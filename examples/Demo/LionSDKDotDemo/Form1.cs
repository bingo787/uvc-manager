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
        const int systemTimeTick = 1000;
        const double HV_MaxKV = 80.0f; // 80kv
        const int HV_MaxCurrent = 1000; //1000mA

        // 控制器对象
        private static TcpClient tcpClient = new TcpClient();

        //定义Timer类
        System.Timers.Timer timer;
        //定义委托
        public delegate void UpdateStatusBarInfo(string value);

        //当前的设备对象
        private List<LU_DEVICE> listDev = new List<LU_DEVICE>();

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

            //初始化参数
            this.comboBoxModel.Items.Clear();
            this.comboBoxModel.Items.Add("AC 出图");
            this.comboBoxModel.Items.Add("VTC 出图");
            this.comboBoxModel.SelectedIndex = 0;

            this.comboBoxBinning.Items.Clear();
            this.comboBoxBinning.Items.Add("No Binning");
            this.comboBoxBinning.Items.Add("Binning");
            this.comboBoxBinning.SelectedIndex = 0;

            this.comboBoxFilter.Items.Clear();
            this.comboBoxFilter.Items.Add("原始图像");
            this.comboBoxFilter.Items.Add("FPGA 进行坏点过滤");
            this.comboBoxFilter.SelectedIndex = 0;

            this.comboBoxRay.Items.Clear();
            this.comboBoxRay.Items.Add("VTC-D");
            this.comboBoxRay.Items.Add("VTC-A");
            this.comboBoxRay.SelectedIndex = 0;

            this.textBoxCheckTime.Text = "5000";
            this.textBoxGetTime.Text = "10000";
            // 初始化串口参数


            List<string> port = SerialPort.GetPortNames().ToList();
            string[] rate = {"4800","9600","19200","38400","43000","56000","57600","115200" };
            string[] databit = { "8", "7", "6" };
            string[] checkbit = { "None","Odd","Even"};
            string[] stopbit = { "1", "2"};

            // HV
            this.comboBoxHVPort.Items.AddRange(port.ToArray());
            this.comboBoxHVPort.SelectedIndex = -1;


            this.comboBoxHVBaudRate.Items.AddRange(rate);
            this.comboBoxHVBaudRate.SelectedIndex = 0;

            this.comBoxHVDataBit.Items.AddRange(databit);
            this.comBoxHVDataBit.SelectedIndex = 0;

            this.comBoxHVCheckBit.Items.AddRange(checkbit);
            this.comBoxHVCheckBit.SelectedIndex = 0;

            this.comboBoxHVStopBit.Items.AddRange(stopbit);
            this.comboBoxHVStopBit.SelectedIndex = 0;

            // 设置高压状态
            this.labelHVPortLED.Text = "●";
            // 高压未连接的话，无法设置电压电流
            TriggerHVPortStatus(false);

            // 设置高压的最大最小值
            this.trackBarCurrent.Maximum = HV_MaxCurrent;
            this.trackBarKV.Maximum = (int)HV_MaxKV*1000;

            this.labelXrayStatus.Text = "●";
            this.labelXrayStatus.ForeColor = Color.Red;
            

      

            /// PLC 
            this.comboBoxPLCPort.Items.AddRange(port.ToArray());
            this.comboBoxPLCPort.SelectedIndex = -1;

            this.comboBoxPLCBaudRate.Items.AddRange(rate);
            this.comboBoxPLCBaudRate.SelectedIndex = 0;

            this.comboBoxPLCDataBit.Items.AddRange(checkbit);
            this.comboBoxPLCDataBit.SelectedIndex = 0;

            this.comboBoxPLCCheckBit.Items.AddRange(checkbit);
            this.comboBoxPLCCheckBit.SelectedIndex = 0;

            this.comboBoxPLCStopBit.Items.AddRange(stopbit);
            this.comboBoxPLCStopBit.SelectedIndex = 0;
            
            // PLC 的串口状态设置
            this.labelPLCPortStatus.Text = "●";
            labelPLCPortStatus.ForeColor = Color.Red;
            buttonConnectPLC.Text = "打开串口";

            // 系统状态监控
            Timer timer = new Timer(new TimerCallback(timeUp), null, Timeout.Infinite, systemTimeTick);

            timer.Change(0,1000);


        }
        private void timeUp(object value) {
            this.Invoke(new UpdateStatusBarInfo(UpdateSystemTime), "");
            
        }

        private void UpdateSystemTime(string obj) {
            labelSystemTime.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToLongDateString();


            
            //try
            //{
            //    buttonGetDevState_Click(null, null);
            //}
            //catch {

            //}


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

            this.trackBarCurrent.Enabled = open;
            this.trackBarKV.Enabled = open;
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

                                    string fileName = "D:\\temp\\"  + DateTime.Now.ToFileTime().ToString() +".jpg";
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
                PLCSerialPortController.Instance.CloseSerialPort();
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
                    HVSerialPortControler.Instance.OpenSerialPort();
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

            if (PLCSerialPortController.Instance.IsOpen())
            {

                PLCSerialPortController.Instance.CloseSerialPort();

                labelPLCPortStatus.ForeColor = Color.Red;
                buttonConnectPLC.Text = "打开串口";

            }
            else {
                try
                {
                    PLCSerialPortController.Instance.OpenSerialPort();
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

        private void trackBarKV_Scroll(object sender, EventArgs e)
        {
            textBoxKV.Text = (trackBarKV.Value/1000.0f).ToString("F2");
        }

        private void textBoxKV_TextChanged(object sender, EventArgs e)
        {
            double kv = 0;
            if (double.TryParse(textBoxKV.Text.ToString(), out kv) && kv <= HV_MaxKV)
            {
                trackBarKV.Value = (int)kv*1000;
                HVSerialPortControler.Instance.SetKV(kv);
            }
            else {
                MessageBox.Show("输入的参数非法或者超过上线");
            }
           
        }

        private void trackBarCurrent_Scroll(object sender, EventArgs e)
        {
            textBoxCurrent.Text = trackBarCurrent.Value.ToString();
        }

        private void textBoxCurrent_TextChanged(object sender, EventArgs e)
        {
            int current = 0;
            if (int.TryParse(textBoxCurrent.Text.ToString(), out current) && current <= HV_MaxCurrent)
            {
                trackBarCurrent.Value = current;
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
    }
}
