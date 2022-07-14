using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LionSDK;

using SerialPortController;
using DAL;


namespace LionSDKDotDemo
{


    public partial class Demo : Form
    {

        // 常量

        double HV_MaxKV = 80.0f; // 80kv
        int HV_MaxCurrent = 1000; //1000mA
        UInt32 SensorCheckTime = 5000;
        UInt32 SensorGetImageTime = 100000;

        List<string> rate = new List<string> { "4800", "9600", "19200", "38400", "43000", "56000", "57600", "115200" };
        List<string> databit = new List<string> { "8", "7", "6" };
        List<string> checkbit = new List<string> { "None", "Odd", "Even" };
        List<string> stopbit = new List<string> { "1", "2" };
        List<string> port = SerialPort.GetPortNames().ToList();

        List<string> uvcMode = new List<string> {"AC", "VTC" };
        List<string> uvcBinning = new List<string> { "No", "Binning"};
        List<string> uvcFPGA = new List<string> {"Raw", "FPGA" };
        List<string> uvcXrayType = new List<string> { "VTC-D", "VTC-A"};

        // 读取PLC状态的线程`
        private Thread monitorPlcCommandThread; 
        // 控制器对象
        private static TcpClient tcpClient = new TcpClient();


        //当前的设备对象
        private List<LU_DEVICE> listDev = new List<LU_DEVICE>();

        private void LoadConfig() {


            // UVC
            this.comboBoxModel.SelectedIndex = uvcMode.IndexOf(Config.Instance.ReadString("UVCSetting", "Mode"));
            this.comboBoxFilter.SelectedIndex = uvcFPGA.IndexOf(Config.Instance.ReadString("UVCSetting", "FPGA"));


            this.textBoxCheckTime.Text = Config.Instance.ReadString("UVCSetting", "CheckTimeout");

            // HV port
            PortPara HVPortPara = Config.Instance.GetPortPara("HVPortPara");
            this.comboBoxHVPort.SelectedIndex = port.IndexOf(HVPortPara.PortName.ToString());
            this.comboBoxHVBaudRate.SelectedIndex = rate.IndexOf(HVPortPara.BaudRate.ToString());
            this.comBoxHVDataBit.SelectedIndex = databit.IndexOf(HVPortPara.DataBits.ToString());
            this.comBoxHVCheckBit.SelectedIndex = checkbit.IndexOf(HVPortPara.Parity.ToString());
            this.comboBoxHVStopBit.SelectedIndex = stopbit.IndexOf(HVPortPara.StopBits.ToString());

            this.textBoxCurrent.Text = Config.Instance.ReadString("HVSettingPara", "Current");
            this.textBoxKV.Text = Config.Instance.ReadString("HVSettingPara", "KV");

            double.TryParse(Config.Instance.ReadString("HVSettingPara", "KV_Max"), out HV_MaxKV);
            int.TryParse(Config.Instance.ReadString("HVSettingPara", "Current_Max"), out HV_MaxCurrent);

            Console.WriteLine("KV_Max = {0}, Current_Max = {1}", HV_MaxKV, HV_MaxCurrent);

            // PLC 参数设置

            this.textBoxIpAddress.Text = Config.Instance.ReadString("PLCPara", "IP");
            this.textBoxPort.Text = Config.Instance.ReadString("PLCPara", "Port");


        }

        void SaveConfig() {
            Config.Instance.WriteString("HVPortPara", "PortName", this.comboBoxHVPort.Text);
            Config.Instance.WriteString("HVPortPara", "BaudRate", this.comboBoxHVBaudRate.Text);
            Config.Instance.WriteString("HVPortPara", "Parity", this.comBoxHVCheckBit.Text);
            Config.Instance.WriteString("HVPortPara", "DataBits", this.comBoxHVDataBit.Text);
            Config.Instance.WriteString("HVPortPara", "StopBits", this.comboBoxHVStopBit.Text);

            Config.Instance.WriteString("HVSettingPara", "KV", this.textBoxKV.Text);
            Config.Instance.WriteString("HVSettingPara", "Current", this.textBoxCurrent.Text);

            Config.Instance.WriteString("UVCSetting", "Mode", this.comboBoxModel.Text);
            Config.Instance.WriteString("UVCSetting", "FPGA", this.comboBoxFilter.Text);
            Config.Instance.WriteString("UVCSetting", "CheckTimeout", this.textBoxCheckTime.Text);

            Config.Instance.WriteString("PLCPara", "IP", this.textBoxIpAddress.Text);
            Config.Instance.WriteString("PLCPara", "Port", this.textBoxPort.Text);

        }


        private void MonitorPLC() {

            while (true){

                try
                {
                    bool ret = PLCHelperModbusTCP.fnGetInstance().ReadSingleCoilRegistor(2010);
                    Console.WriteLine("M2010 " + ret.ToString());

                    if (ret == true)
                    {
                        // 拍照

                        buttonAsynchronous_Click(null, null);
                    }
                    else
                    {

                        // 不拍照
                    }
                }
                catch {
                    MessageBox.Show("PLC M2010读取异常, 请重新连接PLC");
                    break;
                }

             
            }

          

        }

        public Demo()
        {
            InitializeComponent();
            //
            this.treeViewDevice.CheckBoxes = true;
            this.treeViewDevice.FullRowSelect = true;
            this.treeViewDevice.Indent = 20;
            this.treeViewDevice.ItemHeight = 20;
            this.treeViewDevice.LabelEdit = false;
            this.treeViewDevice.Scrollable = true;
            this.treeViewDevice.ShowPlusMinus = true;
            this.treeViewDevice.ShowRootLines = true;

            // 初始化参数
            this.comboBoxModel.DataSource = uvcMode;
            this.comboBoxFilter.DataSource = uvcFPGA;

            // 初始化串口参数
            this.comboBoxHVPort.Items.AddRange(port.ToArray());
            this.comboBoxHVBaudRate.Items.AddRange(rate.ToArray());
            this.comBoxHVDataBit.Items.AddRange(databit.ToArray());
            this.comBoxHVCheckBit.Items.AddRange(checkbit.ToArray());
            this.comboBoxHVStopBit.Items.AddRange(stopbit.ToArray());


            LoadConfig();

            // 设置高压状态
            this.labelHVPortLED.Text = "●";
            // 高压未连接的话，无法设置电压电流
            TriggerHVPortStatus(false);


            this.labelXrayStatus.Text = "●";
            this.labelXrayStatus.ForeColor = Color.Red;
            

            // PLC  状态设置
            this.labelPLCStatus.Text = "●";
            labelPLCStatus.ForeColor = Color.Red;
            buttonConnectPLC.Text = "连接";

            // 曝光时间设置暂时不支持
            this.textBoxCheckTime.Enabled = false;


            buttonClearImage_Click(null, null);

        }

        private void TriggerHVPortStatus(bool open) {
            if (open)
            {
                this.labelHVPortLED.ForeColor = Color.Green;
                this.buttonConnectHVPort.Text = "关闭串口";
            }
            else {
                this.labelHVPortLED.ForeColor = Color.Red;
                this.buttonConnectHVPort.Text = "打开串口";
            }

            this.textBoxCurrent.Enabled = open;
            this.textBoxKV.Enabled = open;

        }

        private uint[] ImageIds = {0,1 };

        private void buttonEnumDev_Click(object sender, EventArgs e)
        {
            this.treeViewDevice.Nodes.Clear();
            listDev.Clear();
            int nCount = LionSDK.LionSDK.GetDeviceCount();
            //加入根文件
            TreeNode root = this.treeViewDevice.Nodes.Add("LIONUVC设备("+nCount+")");
            root.Name = "LionUVC_root";
            for (int d = 0; d < nCount; d++)
            {
                LU_DEVICE luDev = new LU_DEVICE();
                if(LionCom.LU_SUCCESS == LionSDK.LionSDK.GetDevice(d, ref luDev))
                {
                    listDev.Add(luDev);
                    string strText = System.Text.Encoding.ASCII.GetString(luDev.uvcIdentity.Name);
                    TreeNode node = new TreeNode();
                    node.Text = strText;
                    ImageIds[d] = luDev.uvcIdentity.Id;
                    node.Name = Convert.ToString(luDev.uvcIdentity.Id);

                    root.Nodes.Add(node);
                }
            }
            root.ExpandAll();
        }

        private void treeViewDevice_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                //
                e.Node.Checked = true;
                //
                UInt32 id = Convert.ToUInt32(e.Node.Name);
                //
                for (int d = 0; d < listDev.Count; d++)
                {
                    if (listDev[d].uvcIdentity.Id == id)
                    {
                        this.textBoxDevInfo.Text = "设备名称: " + System.Text.Encoding.ASCII.GetString(listDev[d].uvcIdentity.Name) +
                            "\n设备ID: " + listDev[d].uvcIdentity.Id +
                            "\n设备版本: " + listDev[d].uvcIdentity.MajorNum + "-" + listDev[d].uvcIdentity.MinorNum +
                            "\n设备序列号:" + System.Text.Encoding.ASCII.GetString(listDev[d].uvcIdentity.DevSerial) + "\n";
                        //
                        this.textBoxSerial.Text = System.Text.Encoding.ASCII.GetString(listDev[d].uvcIdentity.DevSerial);
                        break;
                    }
                }
            }
            else
            {
                this.textBoxDevInfo.Text = "设备名称: " +
                            "\n设备ID: " + 
                            "\n设备版本: " + 
                            "\n设备序列号:" +  "\n";
            }
        }

        /// <summary>
        /// 修改序列号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonModifySerial_Click(object sender, EventArgs e)
        {
            //当前选择的设备
            if (this.treeViewDevice.SelectedNode == null || this.treeViewDevice.SelectedNode.Parent == null)
            {
                //请先选择设备
                MessageBox.Show("请先选择设备!");
                return;
            }
            //
            UInt32 id = Convert.ToUInt32(this.treeViewDevice.SelectedNode.Name);
            //
            for (int d = 0; d < listDev.Count; d++)
            {
                if (listDev[d].uvcIdentity.Id == id)
                {
                    LU_DEVICE luDev = listDev[d];

                    string strSerial = this.textBoxSerial.Text;
                    byte[] byteSerial = System.Text.Encoding.ASCII.GetBytes(strSerial);
                    if (LionCom.LU_SUCCESS == LionSDK.LionSDK.SetDeviceSerial(ref luDev, byteSerial))
                    {
                        //设置成功
                        listDev[d] = luDev;
                        MessageBox.Show("序列号设置成功");

                    }
                    else
                    {
                        //设置失败
                        MessageBox.Show("序列号设置失败");
                    }

                    this.textBoxDevInfo.Text = "设备名称: " + System.Text.Encoding.ASCII.GetString(listDev[d].uvcIdentity.Name) +
                            "\n设备ID: " + listDev[d].uvcIdentity.Id +
                            "\n设备版本: " + listDev[d].uvcIdentity.MajorNum + "-" + listDev[d].uvcIdentity.MinorNum +
                            "\n设备序列号:" + System.Text.Encoding.ASCII.GetString(listDev[d].uvcIdentity.DevSerial) + "\n";
                    //
                    this.textBoxSerial.Text = System.Text.Encoding.ASCII.GetString(listDev[d].uvcIdentity.DevSerial);
                    break;
                }
            }
            
        }

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetParameter_Click(object sender, EventArgs e)
        {
            //获取参数，设置参数
            //出图模式
            int nImgModel = this.comboBoxModel.SelectedIndex;
            //Binning模式
            int nBinning = 0; //this.comboBoxBinning.SelectedIndex;
            //图像处理标志
            int nFilter = this.comboBoxFilter.SelectedIndex;
            //X-RAY类型
            int nRay = 0;// this.comboBoxRay.SelectedIndex;
                         //检测图像时间
                        
            UInt32 nCheckTime = SensorCheckTime; 
            //获取图像时间
 
            UInt32 nGetTime = SensorGetImageTime;
            //////
            UInt32 id = Convert.ToUInt32(this.treeViewDevice.SelectedNode.Name);
            //
            for (int d = 0; d < listDev.Count; d++)
            {
                if (listDev[d].uvcIdentity.Id == id)
                {
                    LU_DEVICE luDev = listDev[d];
                    //
                    LU_PARAM param = new LU_PARAM();
                    unsafe
                    {
                        //出图模式
                        param.param = (UInt16)LUDEV_PARAM.LUDEVPARAM_MODE;
                        param.size = sizeof(UInt32);
                        param.data = &nImgModel;
                        //
                        LionSDK.LionSDK.SetDeviceParam(ref luDev, ref param);
                    }
                    //
                    unsafe
                    {
                        //Binning模式
                        param.param = (UInt16)LUDEV_PARAM.LUDEVPARAM_BINNING;
                        param.size = sizeof(UInt32);
                        param.data = &nBinning;
                        //
                        LionSDK.LionSDK.SetDeviceParam(ref luDev, ref param);
                    }
                    unsafe
                    {
                        //图像处理标志
                        param.param = (UInt16)LUDEV_PARAM.LUDEVPARAM_FILTER;
                        param.size = sizeof(UInt32);
                        param.data = &nFilter;
                        //
                        LionSDK.LionSDK.SetDeviceParam(ref luDev, ref param);
                    }
                    unsafe
                    {
                        //X-RAY类型
                        param.param = (UInt16)LUDEV_PARAM.LUDEVPARAM_XRAY;
                        param.size = sizeof(UInt32);
                        param.data = &nRay;
                        //
                        LionSDK.LionSDK.SetDeviceParam(ref luDev, ref param);
                    }
                    unsafe
                    {
                        //检测图像时间
                        param.param = (UInt16)LUDEV_PARAM.LUDEVPARAM_TRIGGERTIME;
                        param.size = sizeof(UInt32);
                        param.data = &nCheckTime;
                        //
                        LionSDK.LionSDK.SetDeviceParam(ref luDev, ref param);
                    }
                    unsafe
                    {
                        //获取图像时间
                        param.param = (UInt16)LUDEV_PARAM.LUDEVPARAM_READIMAGETIME;
                        param.size = sizeof(UInt32);
                        param.data = &nGetTime;
                        //
                        LionSDK.LionSDK.SetDeviceParam(ref luDev, ref param);
                    }


                    //获取设置
                    unsafe
                    {
                        //出图模式
                        nImgModel = 0;
                        param.param = (UInt16)LUDEV_PARAM.LUDEVPARAM_MODE;
                        param.size = sizeof(UInt32);
                        param.data = &nImgModel;
                        //
                        LionSDK.LionSDK.GetDeviceParam(ref luDev, ref param);
                    }
                    //
                    unsafe
                    {
                        //Binning模式
                        nBinning = 0;
                        param.param = (UInt16)LUDEV_PARAM.LUDEVPARAM_BINNING;
                        param.size = sizeof(UInt32);
                        param.data = &nBinning;
                        //
                        LionSDK.LionSDK.GetDeviceParam(ref luDev, ref param);
                    }
                    unsafe
                    {
                        //图像处理标志
                        nFilter = 0;
                        param.param = (UInt16)LUDEV_PARAM.LUDEVPARAM_FILTER;
                        param.size = sizeof(UInt32);
                        param.data = &nFilter;
                        //
                        LionSDK.LionSDK.GetDeviceParam(ref luDev, ref param);
                    }
                    unsafe
                    {
                        //X-RAY类型
                        nRay = 0;
                        param.param = (UInt16)LUDEV_PARAM.LUDEVPARAM_XRAY;
                        param.size = sizeof(UInt32);
                        param.data = &nRay;
                        //
                        LionSDK.LionSDK.GetDeviceParam(ref luDev, ref param);
                    }
                    break;
                }
            }

        }


        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGetDevState_Click(object sender, EventArgs e)
        {
            UInt32 id = Convert.ToUInt32(this.treeViewDevice.SelectedNode.Name);
            //
            for (int d = 0; d < listDev.Count; d++)
            {
                if (listDev[d].uvcIdentity.Id == id)
                {
                    LU_DEVICE luDev = listDev[d];
                    
                    unsafe
                    {
                        //获取设备状态
                        LUDEV_STATE state = LUDEV_STATE.LUDEVSTATE_UNOPNE;

                        if (LionCom.LU_SUCCESS == LionSDK.LionSDK.GetDeviceState(ref luDev, ref state))
                        {
                            //更亲状态信息
                            switch (state)
                            {
                                case LUDEV_STATE.LUDEVSTATE_UNOPNE:				//设备未打开
                                    this.labelStateInfo.Text =  "设备状态: 设备未打开!";
                                    break;
                                case LUDEV_STATE.LUDEVSTATE_OPEN:					//设备打开
                                    this.labelStateInfo.Text = "设备状态: 设备打开!";
                                    break;
                                case LUDEV_STATE.LUDEVSTATE_WAITTRIGGER:					//等待触发
                                    this.labelStateInfo.Text = "设备状态: 等待触发!";
                                    break;
                                case LUDEV_STATE.LUDEVSTATE_TRIGGERIMAGE:					//触发获取图像数据
                                    this.labelStateInfo.Text = "设备状态: 触发获取图像数据!";
                                    break;
                                case LUDEV_STATE.LUDEVSTATE_IMAGESAVE:					//图像保存
                                    this.labelStateInfo.Text = "设备状态: 图像保存!";
                                    break;
                                case LUDEV_STATE.LUDEVSTATE_OVERTIME:					//超时
                                    this.labelStateInfo.Text = "设备状态: 获取图像超时";
                                    break;
                            }
                        }
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// 中断获取图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAbandon_Click(object sender, EventArgs e)
        {
            UInt32 id = Convert.ToUInt32(this.treeViewDevice.SelectedNode.Name);
            //
            for (int d = 0; d < listDev.Count; d++)
            {
                if (listDev[d].uvcIdentity.Id == id)
                {
                    LU_DEVICE luDev = listDev[d];
                    //
                    unsafe
                    {
                        //中断获取图像
                        if (LionCom.LU_SUCCESS == LionSDK.LionSDK.AbandonGetImage(ref luDev))
                        {
                            
                        }
                    }
                    break;
                }
            }
        }

        
        /// <summary>
        /// 异步获取图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAsynchronous_Click(object sender, EventArgs e)
        {
            //if (this.treeViewDevice.SelectedNode == null || this.treeViewDevice.SelectedNode.Parent == null) {
            //    MessageBox.Show("请先选择设备");
            //    return;
            //}

            for (int d = 0; d < listDev.Count; d++)
            {
                {
                    LU_DEVICE luDev = listDev[d];
                    //
                    unsafe
                    {
                        LionCom.LionImageCallback callback = new LionCom.LionImageCallback(AsyncImageCallback);
                        int ret = LionSDK.LionSDK.GetImage(ref luDev, 0, callback);
                        if (LionCom.LU_SUCCESS != ret)
                        {
                            MessageBox.Show("异步采集图像失败 " + ret.ToString());
                        }

                    }
                }
            }
        }

        /// <summary>
        /// 同步获取像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSynchronous_Click(object sender, EventArgs e)
        {

            Console.WriteLine("同步获取图像开始...");
            //if (this.treeViewDevice.SelectedNode == null || this.treeViewDevice.SelectedNode.Parent == null)
            //{
            //    MessageBox.Show("请先选择设备");
            //    return;
            //}
            //同步获取图像

            //
            for (int d = 0; d < listDev.Count; d++)
            {
               // Console.WriteLine("listDev[d].uvcIdentity.Id  {0}", listDev[d].uvcIdentity.Id);
                {
                    LU_DEVICE luDev = listDev[d];
                    unsafe
                    {
                        string strFile="";

                        int nBuf = 0;
                        byte[] data = null;
                        int ret_image = LionSDK.LionSDK.GetImage(ref luDev, 0, ref data, ref nBuf, ref strFile);

                        if (LionCom.LU_SUCCESS == ret_image)
                        {
                            if (!string.IsNullOrEmpty(strFile))
                            {
                                // 显示图像


                                if (luDev.uvcIdentity.Id == ImageIds[0])
                                {
                                    this.pictureBoxImage.Load(strFile);
                                }
                                else if (luDev.uvcIdentity.Id == ImageIds[1])
                                {
                                    this.pictureBoxImage1.Load(strFile);
                                }
                                else
                                {
                                    MessageBox.Show("请重新枚举设备!");
                                    return;
                                }


                                // 处理图像
                                ProcessImage(strFile.Replace("bmp", "jpg"));    
                            }
                            else
                            {
                                MessageBox.Show("图像获取失败! 路径为空");
                            }

                        }
                        else {
                            MessageBox.Show("图像获取失败!  " +  ret_image.ToString());
                        }

                    }
                    //break;
                }
            }
        }


        private void ProcessImage(string imageFile) {


            string copyFileName = "D:\\temp\\";

            copyFileName += DateTime.Now.ToString("yyyyMMddhhmmss");
            copyFileName += "_";

            // 如果PLC已经连接，则读取地址，命名图片
            //  202121222020_999.jpg
            if (PLCHelperModbusTCP.fnGetInstance().IsConnected)
            {

                Int16 pos = PLCHelperModbusTCP.fnGetInstance().ReadSingleDataRegInt16Cmd(999);
                Console.WriteLine("D999 " + pos.ToString());
                copyFileName += pos.ToString();
            }
            else
            {
                copyFileName += "x";

            }
            copyFileName += ".jpg";


            File.Copy(imageFile, copyFileName, true);

            Console.WriteLine("文件已拷贝到 " + copyFileName);

            // 如果图片处理服务已经连接 就分析图像
            if (tcpClient.IsConnected())
            {

                // 分析图像
                TcpClient.ANALYSIS_RESULT ret = tcpClient.ProcessImage(copyFileName);
                if (ret == TcpClient.ANALYSIS_RESULT.OK)
                {
                    string image = copyFileName.Replace(".jpg", "_OK.jpg");
                    this.pictureBoxImage.Load(image);
                }
                else if (ret == TcpClient.ANALYSIS_RESULT.NG)
                {
                    string image = copyFileName.Replace(".jpg", "_NG.jpg");
                    this.pictureBoxImage.Load(image);
                }
                else
                {
                    MessageBox.Show("发送分析图像指令失败！错误代码：" + ret.ToString());
                }
            }

        }


        private int AsyncImageCallback(LU_DEVICE device, byte[] pImgData, int nDataBuf, string pFile)
        {
            if (!string.IsNullOrEmpty(pFile)) {

                // 显示

                if (device.uvcIdentity.Id == ImageIds[0])
                {
                    this.pictureBoxImage.Load(pFile);
                }
                else if (device.uvcIdentity.Id == ImageIds[1])
                {
                    this.pictureBoxImage1.Load(pFile);
                }
                else {
                    MessageBox.Show("请重新枚举设备!");
                    return 0 ;
                }
                
                // 处理
                ProcessImage(pFile.Replace("bmp", "jpg"));
            }

            return 0;
        }

        private void treeViewDevice_AfterCheck(object sender, TreeViewEventArgs e)
        {
            ////通过鼠标或者键盘触发事件，防止修改节点的Checked状态时候再次进入
            ////if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)
            //{
            //    //if (e.Node.Checked)
            //    {
            //        foreach (TreeNode node in this.treeViewDevice.Nodes)
            //        {
            //            if (node.Name != e.Node.Name)
            //            {
            //                node.Checked = false;
            //            }
            //        }
            //    }
            //}
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            try
            {
                tcpClient.Disconnect();
                HVSerialPortControler.Instance.XRayOff();
                HVSerialPortControler.Instance.CloseControlSystem();

            }
            catch {

            }

            Application.Exit();
        }


        /// <summary>
        /// 连接视觉检测服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void buttonConnectServer_Click(object sender, EventArgs e)
        {
            if (tcpClient.IsConnected())
            {
                tcpClient.Disconnect();
                buttonConnectServer.Text = "连接检测服务";
                buttonConnectServer.ForeColor = Color.Red;
            }
            else {
                tcpClient.Connect();
                buttonConnectServer.Text = "断开检测服务";
                buttonConnectServer.ForeColor = Color.Green;
            }
           
        }

        private void buttonAnalyse_Click(object sender, EventArgs e)
        {
            // 分析图像
            TcpClient.ANALYSIS_RESULT ret = tcpClient.ProcessImage("D://temp//001.jpg");
            if ((int)ret >= 0)
            {
                this.pictureBoxImage.Load("D://temp//001_OK.jpg");
            }
            else
            {
                MessageBox.Show("发送分析图像指令失败！错误代码：" + ret.ToString());
            }

        }



        /// <summary>
        /// 连接高压串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void buttonConnectHVPort_Click(object sender, EventArgs e)
        {

            if (HVSerialPortControler.Instance.IsOpen())
            {
                HVSerialPortControler.Instance.CloseControlSystem();
                TriggerHVPortStatus(false);
            }
            else {
                try
                {
                    HVSerialPortControler.Instance.OpenSerialPort(this.comboBoxHVPort.Text,
                        int.Parse(this.comboBoxHVBaudRate.Text),
                        (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), this.comBoxHVCheckBit.Text),
                        int.Parse(this.comBoxHVDataBit.Text),
                        (System.IO.Ports.StopBits)int.Parse(this.comboBoxHVStopBit.Text));

                    HVSerialPortControler.Instance.Connect();
                    TriggerHVPortStatus(true);
                }
                catch
                {
                    MessageBox.Show("高压串口打开失败");
                    TriggerHVPortStatus(false);
                }

            }

          

        }

        /// <summary>
        /// 连接PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        static bool IsConnectedPLC = false;
        private void buttonConnectPLC_Click(object sender, EventArgs e)
        {

            

            if (!IsConnectedPLC)
            {
                try
                {
                    
                    bool ret = PLCHelperModbusTCP.fnGetInstance().ConnectServer(textBoxIpAddress.Text, textBoxPort.Text);

                    if (!ret)
                    {
                        MessageBox.Show("PLC连接失败 " + textBoxIpAddress.Text + ":" + textBoxPort.Text);
                        return;
                    }


                    labelPLCStatus.ForeColor = Color.Green;
                    buttonConnectPLC.Text = "已连接";
                    IsConnectedPLC = true;

                   monitorPlcCommandThread = new Thread(new ThreadStart(delegate {
                         MonitorPLC();
                    }));

                    monitorPlcCommandThread.Start();


                }
                catch
                {
                    labelPLCStatus.ForeColor = Color.Red;
                    buttonConnectPLC.Text = "连接";
                    MessageBox.Show("PLC连接失败 " + textBoxIpAddress.Text + ":" + textBoxPort.Text);
                }

            }
        }


        private void textBoxKV_TextChanged(object sender, EventArgs e)
        {

            if (!HVSerialPortControler.Instance.IsOpen())
            {
                return;
            }
            double kv = 0;

            if (!double.TryParse(textBoxKV.Text.ToString(), out kv)) {

                MessageBox.Show("输入的参数非法");
                return;
            }
            if (kv > HV_MaxKV)
            {
                MessageBox.Show("输入的参数超过最大值 " + HV_MaxKV.ToString());
                return;
            }


            HVSerialPortControler.Instance.SetKV(kv);

        }



        private void textBoxCurrent_TextChanged(object sender, EventArgs e)
        {
            int current = 0;

            if (!HVSerialPortControler.Instance.IsOpen()) {
                return;
            }

            if (!int.TryParse(textBoxKV.Text.ToString(), out current))
            {

                MessageBox.Show("输入的参数非法");
                return;
            }
            if (current > HV_MaxCurrent)
            {
                MessageBox.Show("输入的参数超过最大值 " + HV_MaxCurrent.ToString());
                return;
            }


            HVSerialPortControler.Instance.SetCurrent(current);
        }

        private void buttonXrayOnOff_Click(object sender, EventArgs e)
        {

            if (!HVSerialPortControler.Instance.IsOpen()) {

                MessageBox.Show("请先打开高压串口！");
                return;
            }

            if (labelXrayStatus.ForeColor == Color.Red)
            {
                // 打开光源
                HVSerialPortControler.Instance.XRayOn();
                labelXrayStatus.ForeColor = Color.Green;
                buttonXrayOnOff.Text = "关闭光源";

            }
            else {
                // 关闭

                HVSerialPortControler.Instance.XRayOff();
                labelXrayStatus.ForeColor = Color.Red;
                buttonXrayOnOff.Text = "打开光源";
            }
        }


     

        private void Demo_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 弹出提示框
            DialogResult result = MessageBox.Show("确定要关闭窗体吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                // 关闭窗体
                e.Cancel = false;
                SaveConfig();

                try
                {
                    tcpClient.Disconnect();
                    HVSerialPortControler.Instance.XRayOff();
                    HVSerialPortControler.Instance.CloseControlSystem();
                    PLCHelperModbusTCP.fnGetInstance().DisConnectServer();

                }
                catch
                {

                }

            }
            else
            {
                // 不关闭窗体
                e.Cancel = true;
            }
        }

        private void buttonM1601_Click(object sender, EventArgs e)
        {

            /// 往M1610 写值可以触发 M2010
            PLCHelperModbusTCP.fnGetInstance().WriteSingleMReg(1601, true);
            bool ret =  PLCHelperModbusTCP.fnGetInstance().ReadSingleCoilRegistor(2010);
            Console.WriteLine("M2010 " + ret.ToString());


        }

        private void buttonClearImage_Click(object sender, EventArgs e)
        {

            /// 清空图像可以加载一张提前准备好的图像。
            pictureBoxImage.Load("no_image.png");
            pictureBoxImage.Invalidate();
            pictureBoxImage1.Load("no_image.png");
            pictureBoxImage1.Invalidate();

        }
    }
}
