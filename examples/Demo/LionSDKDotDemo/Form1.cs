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

            if (Config.Instance.ReadString("ModeSelect", "Auto") == "True")
            {
                this.radioButtonAutoMode.Checked = true;
            }
            else {
                this.radioButtonManualMode.Checked = true;
            }
            

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

            Config.Instance.WriteString("ModeSelect", "Auto", this.radioButtonAutoMode.Checked.ToString());
            Config.Instance.WriteString("ModeSelect", "Manual", this.radioButtonManualMode.Checked.ToString());

        }

        int  IsSensorBusy = 0;
        private void MonitorPLC() {

            while (IsConnectedPLC){

                Thread.Sleep(100);
                try
                {
                  
                    bool ret = PLCHelperModbusTCP.fnGetInstance().ReadSingleCoilRegistor(2010);
                  //  Console.WriteLine("M2010 " + ret.ToString() + " Busy Count " + IsSensorBusy.ToString());

                    if (ret == true && IsSensorBusy == 0)
                    {
                        // 拍照
                        IsSensorBusy = 2;

                        this.BeginInvoke(new Action(() =>
                        {
                            Console.WriteLine("开始异步采图 ");
                            buttonAsynchronous_Click(null, null);
                        }));
                        
                    }
                    else
                    {

                        // 不拍照
                    }
                }
                catch (Exception ex){
                   // MessageBox.Show("PLC M2010读取异常, 请重新连接PLC");
                    Console.WriteLine(ex.ToString());
                    //break;
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

            // 高压初始化
            this.labelHVPortLED.Text = "●";
            TriggerHVPortStatus(false);
            this.pictureBoxXrayOnOff.Image = Properties.Resources.fushe_black;
            this.pictureBoxXrayOnOff.Invalidate();

            InitilizeControlSystem();

            // PLC初始化
            this.labelPLCStatus.Text = "●";
            labelPLCStatus.ForeColor = Color.Red;
            buttonConnectPLC.Text = "连接";

            // 曝光时间设置暂时不支持
            this.textBoxCheckTime.Enabled = false;


            // 清空显示
            buttonClearImage_Click(null, null);

            // 启动线程刷新状态栏
            new Thread(()=> { UpdateStatusBar(); }).Start();

        }

        private void UpdateStatusBar() {

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

        private uint[] ImageIds = {0,0};

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

        private string GetDeviceState(LU_DEVICE luDev) {

            string stateText = "";
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
                            stateText = ": 设备未打开!";
                            break;
                        case LUDEV_STATE.LUDEVSTATE_OPEN:					//设备打开
                            stateText = ": 设备打开!";
                            break;
                        case LUDEV_STATE.LUDEVSTATE_WAITTRIGGER:					//等待触发
                            stateText = ": 等待触发!";
                            break;
                        case LUDEV_STATE.LUDEVSTATE_TRIGGERIMAGE:					//触发获取图像数据
                            stateText = ": 触发获取图像数据!";
                            break;
                        case LUDEV_STATE.LUDEVSTATE_IMAGESAVE:					//图像保存
                            stateText = ": 图像保存!";
                            break;
                        case LUDEV_STATE.LUDEVSTATE_OVERTIME:					//超时
                            stateText = ": 获取图像超时";
                            break;
                    }
                }
            }
            Console.WriteLine("");
            return "Sensor " + luDev.uvcIdentity.Id.ToString() + stateText;
        }
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
                                //case LUDEV_STATE.LUDEVSTATE_UNOPNE:				//设备未打开
                                //    this.labelStateInfo.Text =  "设备状态: 设备未打开!";
                                //    break;
                                //case LUDEV_STATE.LUDEVSTATE_OPEN:					//设备打开
                                //    this.labelStateInfo.Text = "设备状态: 设备打开!";
                                //    break;
                                //case LUDEV_STATE.LUDEVSTATE_WAITTRIGGER:					//等待触发
                                //    this.labelStateInfo.Text = "设备状态: 等待触发!";
                                //    break;
                                //case LUDEV_STATE.LUDEVSTATE_TRIGGERIMAGE:					//触发获取图像数据
                                //    this.labelStateInfo.Text = "设备状态: 触发获取图像数据!";
                                //    break;
                                //case LUDEV_STATE.LUDEVSTATE_IMAGESAVE:					//图像保存
                                //    this.labelStateInfo.Text = "设备状态: 图像保存!";
                                //    break;
                                //case LUDEV_STATE.LUDEVSTATE_OVERTIME:					//超时
                                //    this.labelStateInfo.Text = "设备状态: 获取图像超时";
                                //    break;
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

            toolStripProgressBar.Value = 0;
          //  UInt32 id = Convert.ToUInt32(this.treeViewDevice.SelectedNode.Name);
            //
            for (int d = 0; d < listDev.Count; d++)
            {
               // if (listDev[d].uvcIdentity.Id == id)
                {
                    LU_DEVICE luDev = listDev[d];
                    //
                    unsafe
                    {
                        //中断获取图像
                        try
                        {
                            LionSDK.LionSDK.AbandonGetImage(ref luDev);
                        }
                        catch {

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

            /// before acq delete image
            /// luvc_camera_964.jpg luvc_camera_964.bmp luvc_camera_964.raw
            /// 
            buttonClearImage_Click(null,null); ;
            try
            {
                for (int i = 0; i < 2; i++) {

                    string bmp_file = "luvc_camera_" + ImageIds[i].ToString() + ".bmp";
                    string jpg_file = "luvc_camera_" + ImageIds[i].ToString() + ".jpg";
                    string  raw_file = "luvc_camera_" + ImageIds[i].ToString() + ".raw";

                    Console.WriteLine("delete {0}, {1}, {2}", bmp_file, jpg_file, raw_file);
                    File.Delete(bmp_file);
                    File.Delete(jpg_file);
                    File.Delete(raw_file);
                }
               
            }
            catch {

                Console.WriteLine("delete file failed......");

            }



            toolStripProgressBar.Value = 0;
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
                toolStripProgressBar.Value += 10;
            }
        }

        /// <summary>
        /// 同步获取像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSynchronous_Click(object sender, EventArgs e)
        {

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
            // 原始文件名为 luvc_camera_7416.jpg
            // 拷贝文件名： datetime_positon_cameraid.jpg;

            string timestamp = DateTime.Now.ToString("yyyyMMddhhmmss");
            string plcPostion = "32768"; // Int16 最大值

            string newImageFileName = imageFile.Replace("luvc", timestamp);

            // 如果PLC已经连接，则读取地址，命名图片
            if (PLCHelperModbusTCP.fnGetInstance().IsConnected)
            {
                Int16 pos = PLCHelperModbusTCP.fnGetInstance().ReadSingleDataRegInt16Cmd(999);
                Console.WriteLine("D999 " + pos.ToString());
                plcPostion = pos.ToString();
            }

            newImageFileName = newImageFileName.Replace("camera", plcPostion);


            string copyFileName = "D:\\temp\\" + newImageFileName;
            Console.WriteLine("Copy {0} to {1} ", imageFile, copyFileName);
            File.Copy(imageFile, copyFileName, true);



            // 如果图片处理服务已经连接 就分析图像
            if (tcpClient.IsConnected())
            {

                // 分析图像
                TcpClient.ANALYSIS_RESULT ret = tcpClient.ProcessImage(copyFileName);
                if (ret == TcpClient.ANALYSIS_RESULT.OK)
                {
                    string image = copyFileName.Replace(".jpg", "_OK.jpg");
                    this.pictureBoxImage.Load(image);
                    PLCHelperModbusTCP.fnGetInstance().WriteSingleMReg(1210, true);
                    PLCHelperModbusTCP.fnGetInstance().WriteSingleMReg(1220, true);
                    Console.WriteLine("WriteSingleMReg(1210, true) ");
                }
                else if (ret == TcpClient.ANALYSIS_RESULT.NG)
                {
                    string image = copyFileName.Replace(".jpg", "_NG.jpg");
                    this.pictureBoxImage.Load(image);
                    PLCHelperModbusTCP.fnGetInstance().WriteSingleMReg(1211, false);
                    PLCHelperModbusTCP.fnGetInstance().WriteSingleMReg(1221, false);
                    Console.WriteLine("WriteSingleMReg(1210, true) ");
                }
                else
                {
                    MessageBox.Show("发送分析图像指令失败！错误代码：" + ret.ToString());
                }

            }

            // send result to plc 

            PLCHelperModbusTCP.fnGetInstance().WriteSingleMReg(1210, true);
            PLCHelperModbusTCP.fnGetInstance().WriteSingleMReg(1220, true);

        }


        private int AsyncImageCallback(LU_DEVICE device, byte[] pImgData, int nDataBuf, string pFile)
        {

            Console.WriteLine("AsyncImageCallback come " + device.uvcIdentity.Id.ToString());


            this.BeginInvoke(new Action(() =>
            {
                toolStripProgressBar.Value = 50;
                Console.WriteLine("  ImageIds[0] ={0}, ImageIds[1]={1} , pFile ={2}", ImageIds[0], ImageIds[1], pFile);

                if (!string.IsNullOrEmpty(pFile)) {

                    
                if (device.uvcIdentity.Id == ImageIds[0])
                {
                     //   this.pictureBoxImage.Load(pFile);
                    }
                else if (device.uvcIdentity.Id == ImageIds[1])
                {
                    //    this.pictureBoxImage1.Load(pFile);

                    }
                else {
                    MessageBox.Show("请重新枚举设备!");
                }

                // 处理

                    ProcessImage(pFile.Replace("bmp", "jpg"));
                    toolStripProgressBar.Value = 100;
                    IsSensorBusy -= 1;

               
            }
            }));
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

            }
            else {
                tcpClient.Connect();
            }
           
        }


        /// <summary>
        /// 连接高压串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        static bool IsXrayPortConnected = false;
        private void buttonConnectHVPort_Click(object sender, EventArgs e)
        {

            if (IsXrayPortConnected)
            {
                HVSerialPortControler.Instance.CloseControlSystem();
                TriggerHVPortStatus(false);
                IsXrayPortConnected = false;
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
                    IsXrayPortConnected = true;
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

                    monitorPlcCommandThread = new Thread(new ThreadStart(delegate
                    {
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
            else {

                // 断开PLC　
                PLCHelperModbusTCP.fnGetInstance().DisConnectServer();

            IsConnectedPLC　=　false;

                labelPLCStatus.ForeColor = Color.Red;
                buttonConnectPLC.Text = "连接";
            }
        }


        private void textBoxKV_TextChanged(object sender, EventArgs e)
        {

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


          //  HVSerialPortControler.Instance.SetKV(kv);

        }



        private void textBoxCurrent_TextChanged(object sender, EventArgs e)
        {
            int current = 0;



            if (!int.TryParse(textBoxCurrent.Text.ToString(), out current))
            {

                MessageBox.Show("输入的参数非法");
                return;
            }
            if (current > HV_MaxCurrent)
            {
                MessageBox.Show("输入的参数超过最大值 " + HV_MaxCurrent.ToString());
                return;
            }


          //  HVSerialPortControler.Instance.SetCurrent(current);
        }

        private void buttonXrayOnOff_Click(object sender, EventArgs e)
        {

            if (!HVSerialPortControler.Instance.IsOpen()) {

                MessageBox.Show("请先打开高压串口！");
                return;
            }

            if (buttonXrayOnOff.Text == "打开光源")
            {
                // 打开光源
                double kv = 0;

                if (!double.TryParse(textBoxKV.Text.ToString(), out kv))
                {

                    MessageBox.Show("输入的参数非法");
                    return;
                }
                if (kv > HV_MaxKV)
                {
                    MessageBox.Show("输入的参数超过最大值 " + HV_MaxKV.ToString());
                    return;
                }

                int current;
                if (!int.TryParse(textBoxCurrent.Text.ToString(), out current))
                {

                    MessageBox.Show("输入的参数非法");
                    return;
                }
                if (current > HV_MaxCurrent)
                {
                    MessageBox.Show("输入的参数超过最大值 " + HV_MaxCurrent.ToString());
                    return;
                }

                Console.WriteLine("KV {0}, Current {0}", kv, current);
                HVSerialPortControler.Instance.Preheat(kv, current) ;



                buttonXrayOnOff.Text = "关闭光源";
            }
            else {
                // 关闭

                HVSerialPortControler.Instance.XRayOff();
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
            pictureBoxImage.Image = Properties.Resources.no_image;
            pictureBoxImage.Invalidate();
            pictureBoxImage1.Image = Properties.Resources.no_image; 
            pictureBoxImage1.Invalidate();



        }

        /// <summary>
        /// 高压温度监控
        /// </summary>
        /// <param name="arg"></param>
        void ControlSystem_TemperatureChanged(double arg)
        {

            this.BeginInvoke(new Action(() =>
            {
                toolStripStatusLabel_HV_Temp.Text = arg.ToString("f2") + "℃";
                //this.Log("高压温度: " + lblHV_Temperature.Content.ToString());
            }));
        }

        /// <summary>
        /// 电流监控
        /// </summary>
        /// <param name="arg"></param>
        void ControlSystem_CurrentChanged(uint arg)
        {
            this.BeginInvoke(new Action(() =>
            {
                toolStripStatusLabel_HV_Cur.Text = arg + "uA";
            }));
        }
        /// <summary>
        /// 电压监控
        /// </summary>
        /// <param name="arg"></param>
        void ControlSystem_VoltageChanged(double arg)
        {
            this.BeginInvoke(new Action(() =>
            {
                toolStripStatusLabel_HV_KV.Text = arg + "kV";
            }));
        }
        /// <summary>
        /// Xray曝光监控
        /// </summary>
        /// <param name="arg"></param>
        void ControlSystem_XRayOnChanged(bool arg)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (arg)
                {
                    toolStripStatusLabel_XrayOnOff.Text = "XRay ON";
                    pictureBoxXrayOnOff.Image = Properties.Resources.fushe_red;
                }
                else
                {
                    toolStripStatusLabel_XrayOnOff.Text = "Xray OFF";
                    pictureBoxXrayOnOff.Image = Properties.Resources.fushe_black;

                }
                pictureBoxXrayOnOff.Invalidate();
            }));

        }
        void ControlSystem_Connected() {
            toolStripStatusLabel_HVConn.Text = "高压-已连接";
        }

        /// <summary>
        /// 高压出错信息监控
        /// </summary>
        /// <param name="report"></param>
        void ControlSystem_StateReported(string report)
        {
            this.BeginInvoke(new Action(() =>
            {
                toolStripStatusLabel_HVError.Text = report;
            }));
        }
        /// <summary>
        /// 初始化串口控制器
        /// </summary>
        private void InitilizeControlSystem()
        {
            try
            {

                HVSerialPortControler.Instance.XRayOnChanged += ControlSystem_XRayOnChanged;
                HVSerialPortControler.Instance.VoltageChanged += ControlSystem_VoltageChanged;
                HVSerialPortControler.Instance.CurrentChanged += ControlSystem_CurrentChanged;
                HVSerialPortControler.Instance.TemperatureChanged += ControlSystem_TemperatureChanged;
                HVSerialPortControler.Instance.StateReported += ControlSystem_StateReported;
                HVSerialPortControler.Instance.Connected += ControlSystem_Connected;

            }
            catch (Exception)
            {
                MessageBox.Show("高压通讯串口初始化失败，请检查串口设置\nInitialization of high voltage communication serial port failed. Please check the serial port settings.");
                toolStripStatusLabel_HVConn.Text = "高压-未连接";
            }
        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel_Time.Text = System.DateTime.Now.ToString();
        }

        private void timerSensorState_Tick(object sender, EventArgs e)
        {
            try
            {
                this.toolStripStatusLabel_Sensor1.Text = GetDeviceState(listDev[0]);
                this.toolStripStatusLabel_Sensor2.Text = GetDeviceState(listDev[1]);
            }
            catch {

            }



        }

        private void radioButtonLock_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAutoMode.Checked)
            {

                groupBox4.Enabled = false;
                groupBox7.Enabled = false;
                   
                for (int i = 0; i < groupBox5.Controls.Count; i++) {

                    if (groupBox5.Controls[i].Text != "●") {
                        groupBox5.Controls[i].Enabled = false;
                    }
                    
                }
                for (int i = 0; i < groupBox6.Controls.Count; i++)
                {

                    if (groupBox6.Controls[i].Text != "●")
                    {
                        groupBox6.Controls[i].Enabled = false;
                    }

                }
                

            }
            else {
                groupBox4.Enabled = true;
                groupBox7.Enabled = true;

                for (int i = 0; i < groupBox5.Controls.Count; i++)
                {
                        groupBox5.Controls[i].Enabled = true;
                }
                for (int i = 0; i < groupBox6.Controls.Count; i++)
                {
                        groupBox6.Controls[i].Enabled = true;
                }
            }
        }
    }
}
