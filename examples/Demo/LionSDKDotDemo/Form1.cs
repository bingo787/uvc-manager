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
using Timer = System.Threading.Timer;

namespace LionSDKDotDemo
{


    public partial class Demo : Form
    {

        // 常量

        double HV_MaxKV = 80.0f; // 80kv
        int HV_MaxCurrent = 1000; //1000mA

        List<string> rate = new List<string> { "4800", "9600", "19200", "38400", "43000", "56000", "57600", "115200" };
        List<string> databit = new List<string> { "8", "7", "6" };
        List<string> checkbit = new List<string> { "None", "Odd", "Even" };
        List<string> stopbit = new List<string> { "1", "2" };
        List<string> port = SerialPort.GetPortNames().ToList();

        List<string> uvcMode = new List<string> {"AC", "VTC" };
        List<string> uvcBinning = new List<string> { "No", "Binning"};
        List<string> uvcFPGA = new List<string> {"Raw", "FPGA" };
        List<string> uvcXrayType = new List<string> { "VTC-D", "VTC-A"};


        // 控制器对象
        private static TcpClient tcpClient = new TcpClient();


        //当前的设备对象
        private List<LU_DEVICE> listDev = new List<LU_DEVICE>();

        private void LoadConfig() {


            // UVC
            this.comboBoxModel.SelectedIndex = uvcMode.IndexOf(Config.Instance.ReadString("UVCSetting", "Mode"));
            this.comboBoxBinning.SelectedIndex = uvcBinning.IndexOf(Config.Instance.ReadString("UVCSetting", "Binning"));
            this.comboBoxFilter.SelectedIndex = uvcFPGA.IndexOf(Config.Instance.ReadString("UVCSetting", "FPGA"));
            this.comboBoxRay.SelectedIndex = uvcXrayType.IndexOf(Config.Instance.ReadString("UVCSetting", "XrayType"));

            this.textBoxCheckTime.Text = Config.Instance.ReadString("UVCSetting", "CheckTimeout");
            this.textBoxGetTime.Text = Config.Instance.ReadString("UVCSetting", "GetImageTimeout");

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
            int.TryParse(Config.Instance.ReadString("HVSettingPara", "KV_Current"), out HV_MaxCurrent);


            // PLC port
            PortPara PLCPortPara = Config.Instance.GetPortPara("PLCPortPara");
            this.comboBoxPLCPort.SelectedIndex = port.IndexOf(PLCPortPara.PortName.ToString());
            this.comboBoxPLCBaudRate.SelectedIndex = rate.IndexOf(PLCPortPara.BaudRate.ToString());
            this.comboBoxPLCDataBit.SelectedIndex = databit.IndexOf(PLCPortPara.DataBits.ToString());
            this.comboBoxPLCCheckBit.SelectedIndex = checkbit.IndexOf(PLCPortPara.Parity.ToString());
            this.comboBoxPLCStopBit.SelectedIndex = stopbit.IndexOf(PLCPortPara.StopBits.ToString());

        }

        void SaveConfig() {
            Config.Instance.WriteString("HVPortPara", "PortName", this.comboBoxHVPort.Text);
            Config.Instance.WriteString("HVPortPara", "BaudRate", this.comboBoxHVBaudRate.Text);
            Config.Instance.WriteString("HVPortPara", "Parity", this.comBoxHVCheckBit.Text);
            Config.Instance.WriteString("HVPortPara", "DataBits", this.comBoxHVDataBit.Text);
            Config.Instance.WriteString("HVPortPara", "StopBits", this.comboBoxHVStopBit.Text);

            Config.Instance.WriteString("HVPortPara", "KV", this.textBoxKV.Text);
            Config.Instance.WriteString("HVPortPara", "Current", this.textBoxCurrent.Text);

            Config.Instance.WriteString("PLCPortPara", "PortName", this.comboBoxPLCPort.Text);
            Config.Instance.WriteString("PLCPortPara", "BaudRate", this.comboBoxPLCBaudRate.Text);
            Config.Instance.WriteString("PLCPortPara", "Parity", this.comboBoxPLCCheckBit.Text);
            Config.Instance.WriteString("PLCPortPara", "DataBits", this.comboBoxPLCDataBit.Text);
            Config.Instance.WriteString("PLCPortPara", "StopBits", this.comboBoxPLCStopBit.Text);

            Config.Instance.WriteString("UVCSetting", "Mode", this.comboBoxModel.Text);
            Config.Instance.WriteString("UVCSetting", "Binning", this.comboBoxBinning.Text);
            Config.Instance.WriteString("UVCSetting", "FPGA", this.comboBoxFilter.Text);
            Config.Instance.WriteString("UVCSetting", "XrayType", this.comboBoxRay.Text);
            Config.Instance.WriteString("UVCSetting", "CheckTimeout", this.textBoxCheckTime.Text);
            Config.Instance.WriteString("UVCSetting", "GetImageTimeout", this.textBoxGetTime.Text);

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
            this.comboBoxBinning.DataSource = uvcBinning;
            this.comboBoxFilter.DataSource = uvcFPGA;
            this.comboBoxRay.DataSource = uvcXrayType;

            // 初始化串口参数
            this.comboBoxHVPort.Items.AddRange(port.ToArray());
            this.comboBoxHVBaudRate.Items.AddRange(rate.ToArray());
            this.comBoxHVDataBit.Items.AddRange(databit.ToArray());
            this.comBoxHVCheckBit.Items.AddRange(checkbit.ToArray());
            this.comboBoxHVStopBit.Items.AddRange(stopbit.ToArray());

            this.comboBoxPLCPort.Items.AddRange(port.ToArray());
            this.comboBoxPLCBaudRate.Items.AddRange(rate.ToArray());
            this.comboBoxPLCDataBit.Items.AddRange(databit.ToArray());
            this.comboBoxPLCCheckBit.Items.AddRange(checkbit.ToArray());
            this.comboBoxPLCStopBit.Items.AddRange(stopbit.ToArray());


            LoadConfig();

            // 设置高压状态
            this.labelHVPortLED.Text = "●";
            // 高压未连接的话，无法设置电压电流
            TriggerHVPortStatus(false);


            this.labelXrayStatus.Text = "●";
            this.labelXrayStatus.ForeColor = Color.Red;
            

     
            
            // PLC 的串口状态设置
            this.labelPLCPortStatus.Text = "●";
            labelPLCPortStatus.ForeColor = Color.Red;
            buttonConnectPLC.Text = "打开串口";



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
            int nBinning = this.comboBoxBinning.SelectedIndex;
            //图像处理标志
            int nFilter = this.comboBoxFilter.SelectedIndex;
            //X-RAY类型
            int nRay = this.comboBoxRay.SelectedIndex;
            //检测图像时间
            string strCheckTime = this.textBoxGetTime.Text;
            UInt32 nCheckTime = Convert.ToUInt32(strCheckTime);
            //获取图像时间
            string strGetTime = this.textBoxGetTime.Text;
            UInt32 nGetTime = Convert.ToUInt32(strGetTime);
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
            if (this.treeViewDevice.SelectedNode == null || this.treeViewDevice.SelectedNode.Parent == null)
                return;
            //同步获取图像
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
                        LionCom.LionImageCallback callback = new LionCom.LionImageCallback(AsyncImageCallback);
                        if (LionCom.LU_SUCCESS == LionSDK.LionSDK.GetImage(ref luDev, 0, callback))
                        {
                            
                        }

                    }
                    break;
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
            if (this.treeViewDevice.SelectedNode == null || this.treeViewDevice.SelectedNode.Parent == null)
                return;
            //同步获取图像
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
                        string strFile="";
                        int nBuf = 0;
                        byte[] data = null;
                        if (LionCom.LU_SUCCESS == LionSDK.LionSDK.GetImage(ref luDev, 0, ref data, ref nBuf, ref strFile))
                        {
                            if (!string.IsNullOrEmpty(strFile))
                            {
                                this.pictureBoxImage.Load(strFile);

                                if (tcpClient.IsConnected()) {
                                    // 拷贝图像, 图像检测服务只支持jpg格式。
                                    strFile.Replace("bmp", "jpg");

                                    string fileName = "D:\\temp\\"  + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";
                                    File.Copy(strFile,  fileName, true);

                                    // 分析图像
                                    TcpClient.ANALYSIS_RESULT ret = tcpClient.ProcessImage(fileName);
                                    if (ret == TcpClient.ANALYSIS_RESULT.OK)
                                    {
                                        string image = fileName.Replace(".jpg", "_OK.jpg");
                                        this.pictureBoxImage.Load(image);
                                    }
                                    else if (ret == TcpClient.ANALYSIS_RESULT.NG) {
                                        string image = fileName.Replace(".jpg", "_NG.jpg");
                                        this.pictureBoxImage.Load(image);
                                    }
                                    else
                                    {
                                        MessageBox.Show("发送分析图像指令失败！错误代码：" + ret.ToString());
                                    }
                                }


                               
                            }
                            else {
                                MessageBox.Show("图像获取失败! 路径为空");
                            }

                        }

                    }
                    break;
                }
            }
        }



        private int AsyncImageCallback(LU_DEVICE device, byte[] pImgData, int nDataBuf, string pFile)
        {
            if(!string.IsNullOrEmpty(pFile))
                this.pictureBoxImage.Load(pFile);
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
                PLCPortController.Instance.CloseSerialPort();

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
        /// 连接PLC串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConnectPLC_Click(object sender, EventArgs e)
        {

            if (PLCPortController.Instance.IsOpen())
            {
                PLCPortController.Instance.CloseSerialPort();
                labelPLCPortStatus.ForeColor = Color.Red;
                buttonConnectPLC.Text = "打开串口";

            }
            else
            {
                try
                {
                    PLCPortController.Instance.OpenSerialPort(this.comboBoxPLCPort.Text,
                       int.Parse(this.comboBoxPLCBaudRate.Text),
                       (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), this.comboBoxPLCCheckBit.Text),
                       int.Parse(this.comboBoxPLCDataBit.Text),
                       (System.IO.Ports.StopBits)int.Parse(this.comboBoxPLCStopBit.Text));


                    labelPLCPortStatus.ForeColor = Color.Green;
                    buttonConnectPLC.Text = "关闭串口";
                }
                catch
                {
                    labelPLCPortStatus.ForeColor = Color.Red;
                    buttonConnectPLC.Text = "打开串口";
                    MessageBox.Show("PLC串口打开失败");
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
            if (double.TryParse(textBoxKV.Text.ToString(), out kv) && kv <= HV_MaxKV)
            {
                HVSerialPortControler.Instance.SetKV(kv);
            }
            else {
                MessageBox.Show("输入的参数非法或者超过上线");
            }
           
        }



        private void textBoxCurrent_TextChanged(object sender, EventArgs e)
        {
            int current = 0;

            if (!HVSerialPortControler.Instance.IsOpen()) {
                return;
            }

            if (int.TryParse(textBoxCurrent.Text.ToString(), out current) && current <= HV_MaxCurrent)
            {
                HVSerialPortControler.Instance.SetCurrent(current);
            }
            else
            {
                MessageBox.Show("输入的参数非法或者超过上线");
            }
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
                    PLCPortController.Instance.CloseSerialPort();

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
    }
}
