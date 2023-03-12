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
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Office.Interop.Excel;
using Action = System.Action;

namespace LionSDKDotDemo
{

    public partial class Demo : Form
    {

        // 常量


        private readonly Tuple<string, string>[] BigBoard = new[] {
                            Tuple.Create("B-V0",        "X"  ),  //0
                            Tuple.Create("B-V2",         "X" ), //1
                            Tuple.Create("B-V4",    "B-V6"  ), //2
                            Tuple.Create("B-V8",    "B-V10" ), //3
                            Tuple.Create("B-V12",   "B-V14" ), //4
                            Tuple.Create("B-V16",   "B-V18" ), //5
                            Tuple.Create("B-V20",   "B-V22" ), //6
                            Tuple.Create("B-V21",   "B-V23" ), // 7 
                            Tuple.Create("B-V17",   "B-V19" ), // 8
                            Tuple.Create("B-V13",   "B-V15" ),// 9
                            Tuple.Create("B-V9",    "B-V11" ), //10 
                            Tuple.Create("B-V5",    "B-V7"  ), //11
                            Tuple.Create("B-V1",    "B-V3"  ), //12
                            Tuple.Create("A-V22",   "A-V20" ), // 13
                            Tuple.Create("A-V18",   "A-V16" ), //14
                            Tuple.Create("A-V14",   "A-V12" ), // 15
                            Tuple.Create("A-V10",   "A-V8"  ), //16
                            Tuple.Create("A-V6",    "A-V4"  ), //17
                            Tuple.Create("A-V2",    "A-V0"  ), //18
                            Tuple.Create("A-V3",    "A-V1"  ), //19
                            Tuple.Create("A-V7",    "A-V5"  ), //20
                            Tuple.Create("A-V11",   "A-V9"  ), //21
                            Tuple.Create("A-V15",   "A-V13" ), //22
                            Tuple.Create("A-V19",   "A-V17" ), //23
                            Tuple.Create("A-V23",   "X" ), //24
                            Tuple.Create("A-V21",   "X" ), //25
                            Tuple.Create("X",   "X" ), //26
                            Tuple.Create("X",   "X" ), //27
                            Tuple.Create("X",   "X" ), //28
                            Tuple.Create("X",   "X" ) //29
        };

        private readonly Tuple<string, string>[] SmallBoard = new[]
        {
                        Tuple.Create("B-V0",    "B-V2"  ),
                        Tuple.Create("B-V4",    "B-V6"  ),
                        Tuple.Create("B-V8",    "B-V14" ),
                        Tuple.Create("B-V16",   "B-V12" ),
                        Tuple.Create("B-V17",   "B-V19" ),
                        Tuple.Create("B-V13",   "B-V15" ),
                        Tuple.Create("B-V5",    "B-V7"  ),
                        Tuple.Create("B-V1",    "B-V3"  ),

        };

        // PLC 寄存器定义

        const int PLC_REG_ACQ = 2010;
        const int PLC_REG_XRAY_ONOFF = 2011;
        const int PLC_REG_BOARD_TYPE = 995;
        const int PLC_REG_BOARD_NUM = 950;
        const int PLC_REG_POSTION = 999;
        const int PLC_REG_OK_L = 1210;
        const int PLC_REG_NG_L = 1211;
        const int PLC_REG_OK_R = 1220;
        const int PLC_REG_NG_R = 1221;

        const string SN_R = "R";
        const string SN_L = "L";

        const int BOARD_TYPE_BIG = 0;
        const int BORAD_TYPE_SMALL = 1;
        /// <summary>``
        /// Sensor 常量`
        /// </summary>
        const UInt32 SensorCheckTimeOut = 5000;
        const UInt32 SensorGetImageTimeOut = 100000;


        double HV_MaxKV = 100.0f; // 80kv
        int HV_MaxCurrent = 1000; //1000mA
        double HV_MaxPower = 15.0; //W

        string ViFilePath = "";
        string NiLabviewExePath = "";

        List<string> rate = new List<string> { "4800", "9600", "19200", "38400", "43000", "56000", "57600", "115200" };
        List<string> databit = new List<string> { "8", "7", "6" };
        List<string> checkbit = new List<string> { "None", "Odd", "Even" };
        List<string> stopbit = new List<string> { "1", "2" };
        List<string> port = SerialPort.GetPortNames().ToList();

        List<string> uvcMode = new List<string> { "AC", "VTC" };
        List<string> uvcBinning = new List<string> { "No", "Binning" };
        List<string> uvcFPGA = new List<string> { "Raw", "FPGA" };

        // 处理图像的线程
        private Thread processImageThread_;
        BlockingQueue<string> ImageQueueBuffer = new BlockingQueue<string>(2);
        // 读取PLC状态的线程`
        private Thread monitorPlcCommandThread;
        // 控制器对象
        private static TcpClient ImageProcessTcpClient = new TcpClient();


        //当前的设备对象
        private List<LU_DEVICE> listDev = new List<LU_DEVICE>();
        private int delay_ms = 1000;
        private void LoadConfig()
        {


            // UVC
            this.comboBoxModel.SelectedIndex = uvcMode.IndexOf(Config.Instance.ReadString("UVCSetting", "Mode"));
            this.comboBoxFilter.SelectedIndex = uvcFPGA.IndexOf(Config.Instance.ReadString("UVCSetting", "FPGA"));
            this.textBoxActTime.Text = Config.Instance.ReadString("UVCSetting", "ActTime");

            int.TryParse(Config.Instance.ReadString("UVCSetting", "DelayMs"), out delay_ms);


            // HV port
            PortPara HVPortPara = Config.Instance.GetPortPara("HVPortPara");
            this.comboBoxHVPort.SelectedIndex = port.IndexOf(HVPortPara.PortName);
            this.comboBoxHVBaudRate.SelectedIndex = rate.IndexOf(HVPortPara.BaudRate.ToString());
            this.comBoxHVDataBit.SelectedIndex = databit.IndexOf(HVPortPara.DataBits.ToString());
            this.comBoxHVCheckBit.SelectedIndex = checkbit.IndexOf(HVPortPara.Parity.ToString());
            this.comboBoxHVStopBit.SelectedIndex = stopbit.IndexOf(HVPortPara.StopBits.ToString());

            this.textBoxCurrent.Text = Config.Instance.ReadString("HVSettingPara", "Current");
            this.textBoxKV.Text = Config.Instance.ReadString("HVSettingPara", "KV");

            double.TryParse(Config.Instance.ReadString("HVSettingPara", "KV_Max"), out HV_MaxKV);
            int.TryParse(Config.Instance.ReadString("HVSettingPara", "Current_Max"), out HV_MaxCurrent);
            double.TryParse(Config.Instance.ReadString("HVSettingPara", "Power_Max"), out HV_MaxPower);

            Console.WriteLine("KV_Max = {0}, Current_Max = {1}, Power_Max = {2}", HV_MaxKV, HV_MaxCurrent, HV_MaxPower);

            // PLC 参数设置

            this.textBoxIpAddress.Text = Config.Instance.ReadString("PLCPara", "IP");
            this.textBoxPort.Text = Config.Instance.ReadString("PLCPara", "Port");



            NiLabviewExePath = Config.Instance.ReadString("ImageProcessService", "Path");
            ViFilePath = Config.Instance.ReadString("ImageProcessService", "Arguments");



        }

        void SaveConfig()
        {
            Config.Instance.WriteString("HVPortPara", "PortName", this.comboBoxHVPort.Text);
            Config.Instance.WriteString("HVPortPara", "BaudRate", this.comboBoxHVBaudRate.Text);
            Config.Instance.WriteString("HVPortPara", "Parity", this.comBoxHVCheckBit.Text);
            Config.Instance.WriteString("HVPortPara", "DataBits", this.comBoxHVDataBit.Text);
            Config.Instance.WriteString("HVPortPara", "StopBits", this.comboBoxHVStopBit.Text);

            Config.Instance.WriteString("HVSettingPara", "KV", this.textBoxKV.Text);
            Config.Instance.WriteString("HVSettingPara", "Current", this.textBoxCurrent.Text);

            Config.Instance.WriteString("UVCSetting", "Mode", this.comboBoxModel.Text);
            Config.Instance.WriteString("UVCSetting", "FPGA", this.comboBoxFilter.Text);
            Config.Instance.WriteString("UVCSetting", "ActTime", this.textBoxActTime.Text);

            Config.Instance.WriteString("PLCPara", "IP", this.textBoxIpAddress.Text);
            Config.Instance.WriteString("PLCPara", "Port", this.textBoxPort.Text);



        }


        // 拷贝一个文件夹
        public static void CopyDirectory(string sourceDir, string destDir)
        {
            // 如果目标目录不存在，则创建它
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            // 获取源目录下的所有文件和子目录
            string[] files = Directory.GetFiles(sourceDir);
 

            // 拷贝所有文件
            foreach (string file in files)
            {

                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                Console.WriteLine("copy src {0}, dst {1}",file, destFile);
                File.Copy(file, destFile, true);
            }

 
        }

        void checkAndClearDir(string path)
        {

            if (Directory.Exists(path))
            {
                // 清空目录中的所有文件
                DirectoryInfo directory = new DirectoryInfo(path);
                foreach (FileInfo file in directory.GetFiles())
                {

                    try {
                        file.Delete();
                    } catch (Exception e) { 
                        Console.WriteLine($"Error: {e}");
                    }
                    
                }
            }
            else
            {
                // 创建目录
                Directory.CreateDirectory(path);
            }
        }

        void CSV2XLS(string csvPath, string excelPath)
        {


            // 创建一个 Excel 应用程序对象
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            // 打开 CSV 文件
            Workbook workbook = excel.Workbooks.Open(csvPath,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);

            // 将 CSV 数据复制到新的 Excel 工作簿中
            Worksheet worksheet = workbook.Sheets[1];
            worksheet.Copy(Type.Missing, Type.Missing);

            if (File.Exists(excelPath)) {
                File.Delete(excelPath);
            }

            // 将新的工作簿保存为 Excel 文件
            Workbook newWorkbook = excel.ActiveWorkbook;
            newWorkbook.SaveAs(excelPath, XlFileFormat.xlWorkbookDefault,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing);

            workbook.Close();
            newWorkbook.Close();
            // 关闭 Excel 应用程序
            
            excel.Quit();
        }
        void reportMES()
        {

            bool hasNG = false;

            // 1. 遍历D:\temp目录下的所有图片文件名

            string[] fileNames = Directory.GetFiles(@"D:\temp");


            // 2. 创建mes.csv文件
            string csvFilePath = @"D:\temp\mes.csv";

            // 3. 将文件名信息写入csv
            // Create a new StreamWriter to write to the file
            using (StreamWriter sw = new StreamWriter(csvFilePath))
            {
                // Write the headers
                sw.WriteLine("测试时间,镍片编号,测试结果,原因,文件名");

                // 检测处理后的图：日期_版型_版号_点位_序列号_镍片号.jpg
                // D:\temp\20220716115919_0_7021002580_23_L_B-V10_NG.jpg
                char[] delimiters = new char[] { '_', '\\', '.' };
                foreach (string fileName in fileNames)
                {

                    if (fileName.Contains("X"))
                    {
                        continue;
                    }

                    if (!fileName.Contains("OK") && !fileName.Contains("NG"))
                    {
                        continue;
                    }

                    if (fileName.Contains("NG"))
                    {
                        hasNG = true;
                    }

                    Console.WriteLine(fileName);
                    string[] temp = fileName.Split(delimiters);

                    FileInfo fileInfo = new FileInfo(fileName);
                    string mesFileName = fileInfo.Name;
                    string whyNG = "";
                    // 测试时间,镍片编号,测试结果,	原因,文件名
                    string line = temp[2] + "," + temp[7] + "," + temp[8] + "," + whyNG + "," + mesFileName;
                    // Write some data rows
                    sw.WriteLine(line);
                }
                sw.Close();

            }

            Console.WriteLine("CSV file created and written to successfully.");


            // 4. 检查@"D:\mes\"; 下面是否有目录, 并获取目录名字
           
            string path = @"D:\mes\";
            string targetDirName = "";
            if (Directory.Exists(path))
            {
                string[] subdirs = Directory.GetDirectories(path);
                if (subdirs.Length > 0)
                {
                    foreach (string subdir in subdirs)
                    {

                        targetDirName = subdir;
                        // 只要一个目录!!, 所以break;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("No subdirectories found.");
                    throw new Exception("D:\\mes 下面没有找到子目录!!");
                }
            }
            else
            {
                Console.WriteLine("Directory does not exist.");
                throw new Exception("D:\\mes 目录不存在!!");
            }

            DirectoryInfo targetDir = new DirectoryInfo(targetDirName);


            // 6. 将图片和mes.csv 拷贝到D:/mes/PRODUCT目录下
            string sourceDir = @"D:\temp\";

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(file);
                if (fileName.Contains("X"))
                {
                    continue;
                }
                string destFile = Path.Combine(targetDir.FullName, fileName);
                File.Copy(file, destFile, true);
                Console.WriteLine($"File {file} copyied to {destFile}.");
            }

            // 5. 重命名csv文件名字为产品名
            string targetXlsFileName = Path.Combine(targetDir.FullName, targetDir.Name + ".xls");
            string newCsvFilePath = Path.Combine(targetDir.FullName, "mes.csv");

            Console.WriteLine("CSV转成XLS");
            CSV2XLS(newCsvFilePath, targetXlsFileName);
            File.Delete(newCsvFilePath);

            // 6 将产品本地备份
            string datetime = DateTime.Now.ToString("yyyyMMddhhmmss");
            string copyDirName = Path.Combine(@"D:\rayonbackup", targetDir.Name + "_" + datetime);
            Console.WriteLine("备份到本地");
            CopyDirectory(targetDir.FullName, copyDirName);
            
            // 7. 在D:/mes/PRODUCT 下面创建ready.txt 文件
            string ready = @"D:/mes/ready.txt";

            if (hasNG)
            {
                File.WriteAllText(ready, "NG");
            }
            else
            {
                File.WriteAllText(ready, "OK");
            }


            // 9. 结束
        }

        int reg_postion = 0;
        int reg_board_type = 0;
        int reg_board_id = 0;
        int last_reg_postion = -1;
        bool reported_mes = false;
        bool IsXrayOn = false;
        private void MonitorPLC()
        {

            while (PLCHelperModbusTCP.fnGetInstance().IsConnected)
            {

                try
                {

                    Thread.Sleep(500);

                    // 读取光源控制指令
                    bool reg_xray_onoff = PLCHelperModbusTCP.fnGetInstance().ReadSingleCoilRegistor(PLC_REG_XRAY_ONOFF);

                    // 读取采集图像指令
                    bool reg_acq_onoff = PLCHelperModbusTCP.fnGetInstance().ReadSingleCoilRegistor(PLC_REG_ACQ);


                    // 读取运动装置的位置
                    reg_postion = PLCHelperModbusTCP.fnGetInstance().ReadSingleDataRegInt16Cmd(PLC_REG_POSTION);

                    // 读取隔离板型号
                    reg_board_type = PLCHelperModbusTCP.fnGetInstance().ReadSingleDataRegInt16Cmd(PLC_REG_BOARD_TYPE);

                    // 读取隔离板编号
                    reg_board_id = PLCHelperModbusTCP.fnGetInstance().ReadSingleDataRegInt16Cmd(PLC_REG_BOARD_NUM);



                    Console.WriteLine("xray_onoff = {0}, reg_xray_onoff = {1}, reg_acq_onoff = {2}," +
                        " reg_postion = {3}, last_postion = {4} reg_board_type = {5}, reg_board_id = {6}", 
                        IsXrayOn, reg_xray_onoff, reg_acq_onoff, reg_postion,last_reg_postion, reg_board_type, reg_board_id);


                    if (reg_xray_onoff && !IsXrayOn)
                    {
                        // 1, 0
                        // 如果是 打开光源 的命令，且光源未打开，则打开光源
                        Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.ffff") + " 打开光源 ");
                        XrayOnOff(true);
                        Console.WriteLine("延时 {0} ms", delay_ms);
                        Thread.Sleep(delay_ms);
                        //开始测试
                        // 清空temp目录
                        buttonClearImage_Click(null, null);
                        checkAndClearDir(@"D:\temp");
                        reported_mes = false;


                    }
                    else if (!reg_xray_onoff && IsXrayOn)
                    {
                        // 0, 1
                        // 如果是 关闭光源 的命令，且光源已打开，则 关闭光源
                        Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.ffff") + " 关闭光源 ");
                        XrayOnOff(false);


                    }

                    if (reg_xray_onoff && reg_acq_onoff && (last_reg_postion != reg_postion))
                    {
                        last_reg_postion = reg_postion;

                        this.BeginInvoke(new Action(() =>
                        {
                            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.ffff") + " 开始异步采图 ");
                            buttonAsynchronous_Click(null, null);
                        }));

                    }

                    if ((reg_postion ==26) &&(last_reg_postion !=-1) && (IsXrayOn == false) &&  (reported_mes == false)) {

                        // 上报mes
                        try
                        {
                            reportMES();

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                        reported_mes = true;

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.ffff") + " 已断开PLC " + ex.ToString());
                }


            }



        }
        Process imageProcessServices;
        void runImageProcessServices()
        {

            try
            {
                imageProcessServices = new Process
                {
                    StartInfo =
                    {
                        FileName = NiLabviewExePath,
                        Arguments = ViFilePath,
                        UseShellExecute = false,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                imageProcessServices.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("图像处理服务打开失败！ " + ViFilePath + ex.ToString());
            }


        }


        void OpenXraySerialPortAndInit() {
            try
            {
                SerialPortControler_RS232PROTOCOL_MC110.Instance.OpenSerialPort(this.comboBoxHVPort.Text,
                    int.Parse(this.comboBoxHVBaudRate.Text),
                    (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), this.comBoxHVCheckBit.Text),
                    int.Parse(this.comBoxHVDataBit.Text),
                    (System.IO.Ports.StopBits)int.Parse(this.comboBoxHVStopBit.Text));


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

                double power;
                if (!double.TryParse(textBoxCurrent.Text.ToString(), out power))
                {

                    MessageBox.Show("输入的参数非法");
                    return;
                }
                if (power > HV_MaxPower)
                {
                    MessageBox.Show("输入的参数超过最大值 " + HV_MaxPower.ToString());
                    return;
                }

                Console.WriteLine("KV {0}, Power {1}", kv, power);


                SerialPortControler_RS232PROTOCOL_MC110.Instance.Connect(kv, power);
            }
            catch
            {
                MessageBox.Show("高压串口打开失败, 请检查串口 " + this.comboBoxHVPort.Text);
                return;
            }

        }

        public void OneKeyStart()
        {


            try
            {
                last_reg_postion = -1;


                // 1. 检查设备

                int nCount = LionSDK.LionSDK.GetDeviceCount();
                if (nCount != 2)
                {
                    MessageBox.Show("传感器数量: " + nCount.ToString() + ", 不是2个，请检查图像传感器是否已插好？");
                    return;
                }
                else
                {

                    buttonEnumDev_Click(null, null);
                    // 2. 设置Sensor参数
                    buttonSetParameter_Click(null, null);
                }

                // 3. 打开高压串口
                OpenXraySerialPortAndInit();

                // 5. 连接图像处理服务

                ImageProcessTcpClient.Connect();
                if (!ImageProcessTcpClient.IsConnected())
                {
                    return;
                }



                // 连接PLC
                bool ret = PLCHelperModbusTCP.fnGetInstance().ConnectServer(textBoxIpAddress.Text, textBoxPort.Text);
                if (!ret)
                {
                    return;
                }
                toolStripStatusLabel_PLCConn.Text = "PLC-已连接";

                // 创建PLC监控线程
                monitorPlcCommandThread = new Thread(new ThreadStart(delegate
                {
                    MonitorPLC();
                }));

                monitorPlcCommandThread.Start();

            }
            catch
            {
                MessageBox.Show("系统初始化失败！请手动修正参数，重新启动");
                return;
            }

            UPDATE_PROGRESS(STEP.Idle);

            buttonStart.BackColor = Color.Green;
            buttonStart.Text = "正在运行";

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


            // 高压初始化

            this.pictureBoxXrayOnOff.Image = Properties.Resources.fushe_black;
            this.pictureBoxXrayOnOff.Invalidate();
            InitilizeControlSystem();


            // 0. 加载配置

            LoadConfig();

            //  runImageProcessServices();

            // 清空显示
            buttonClearImage_Click(null, null);

            // 6. 创建图像处理线程
            processImageThread_ = new Thread(new ThreadStart(delegate
            {
                ProcessImage();
            }));

            processImageThread_.Start();

        }


        private void buttonEnumDev_Click(object sender, EventArgs e)
        {
            this.treeViewDevice.Nodes.Clear();
            listDev.Clear();
            int nCount = LionSDK.LionSDK.GetDeviceCount();
            //加入根文件
            TreeNode root = this.treeViewDevice.Nodes.Add("LIONUVC设备(" + nCount + ")");
            root.Name = "LionUVC_root";
            for (int d = 0; d < nCount; d++)
            {
                LU_DEVICE luDev = new LU_DEVICE();
                if (LionCom.LU_SUCCESS == LionSDK.LionSDK.GetDevice(d, ref luDev))
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
            Console.WriteLine("buttonEnumDev_Click listDev.Count {0}", listDev.Count);


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
                            "\n设备序列号:" + "\n";
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


            UInt32 nGetTime = SensorGetImageTimeOut;
            UInt32 nCheckTime = SensorCheckTimeOut;
            //获取图像时间

            string strActTime = this.textBoxActTime.Text;
            UInt32 nActTime = Convert.ToUInt32(strActTime);


            //////
            //  UInt32 id = Convert.ToUInt32(this.treeViewDevice.SelectedNode.Name);
            //

            for (int d = 0; d < listDev.Count; d++)
            {
                //   if (listDev[d].uvcIdentity.Id == id)
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
                    unsafe
                    {
                        //ACT 1.6版本 曝光时间
                        param.param = (UInt16)LUDEV_PARAM.LUDEVPARAM_ACT;
                        param.size = sizeof(UInt32);
                        param.data = &nActTime;
                        //
                        int ret = LionSDK.LionSDK.SetDeviceParam(ref luDev, ref param);

                        Console.WriteLine(" LionSDK.LionSDK.SetDeviceParam {0}, return {1} listDev.Count {2}", luDev.uvcIdentity.Id.ToString(), ret, listDev.Count);
                    }
                    /***
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
                                        */
                }
            }

        }


        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private string GetDeviceState(LU_DEVICE luDev)
        {

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

            UPDATE_PROGRESS(STEP.Idle);
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
                        catch
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

            UPDATE_PROGRESS(STEP.Start_Acq);

            for (int d = 0; d < listDev.Count; d++)
            {

                UPDATE_PROGRESS(STEP.Start_Acq);
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


        void DisplayImageByFileName(string file)
        {

            // "D:\temp\202223232121_0_23_L_A-V10_NG.jpg"
            try
            {
                if (file.Contains(SN_R))
                {
                    this.pictureBoxImage.Load(file);
                }
                else if (file.Contains(SN_L))
                {
                    this.pictureBoxImage1.Load(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        void WriteResultToPlcByFileName(string file)
        {
            try
            {

                // 更新状态

                UPDATE_PROGRESS(STEP.Write_Result);


                // 处理结果
                // 图1： OK->1210写True, NG->1211写False
                // 图2： OK->1211写True, NG->1221写False
                // 原始文件名为 luvc_camera_7416.jpg
                /*
                 原图：日期_版型_版号_点位_左右_镍片号.jpg
                20220716115919_0_7021002580_23_L_B-V10.jpg

                检测处理后的图：日期_版型_版号_点位_SN_镍片号_检测结果.jpg
                20220716115919_0_7021002580_23_L_B-V10_NG.jpg

                 */

                string[] list = file.Split('_');
                string sn = list[4];
                string result = list[6].Replace(".jpg", ""); ;

                Console.WriteLine("sn= {0}, result {1}", sn, result);

                if (sn == SN_R && result.Contains("OK"))
                {
                    PLCHelperModbusTCP.fnGetInstance().WriteSingleMReg(PLC_REG_OK_R, true);
                }
                else if (sn == SN_R && result.Contains("NG"))
                {
                    PLCHelperModbusTCP.fnGetInstance().WriteSingleMReg(PLC_REG_NG_R, true);
                }
                else if (sn == SN_L && result.Contains("OK"))
                {
                    PLCHelperModbusTCP.fnGetInstance().WriteSingleMReg(PLC_REG_OK_L, true);
                }
                else if (sn == SN_L && result.Contains("NG"))
                {
                    PLCHelperModbusTCP.fnGetInstance().WriteSingleMReg(PLC_REG_NG_L, true);
                }
                else
                {

                    MessageBox.Show(sn + result + " 图像处理结果不正确， 无法向PLC写入结果");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void ProcessImage()
        {
            while (true)
            {
                try
                {
                    string fileName = "";
                    bool ret = ImageQueueBuffer.TryDequeue(out fileName);
                    Console.WriteLine("pull ImageQueueBuffer.Count {0}, ret {1}", ImageQueueBuffer.Count(), ret);


                    UPDATE_PROGRESS(STEP.Process_Image);

                    if (ImageProcessTcpClient.IsConnected())
                    {
                        string res = ImageProcessTcpClient.ProcessImage(fileName);

                        if (!res.Contains("OK") && !res.Contains("NG"))
                        {

                            MessageBox.Show(res);
                            continue;
                        }
                        fileName = res.Split(',').ElementAt(1);
                    }
                    Console.WriteLine("thread run display fileName {0}", fileName);
                    DisplayImageByFileName(fileName);

                    if (PLCHelperModbusTCP.fnGetInstance().IsConnected)
                    {
                        WriteResultToPlcByFileName(fileName);
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    continue;
                }


            }



        }

        string GetTargetName(string SN)
        {

            string target = "UNKNOW";
            try
            {
                if (reg_board_type == BOARD_TYPE_BIG)
                {
                    // BigBoard
                    if (SN == SN_R)
                    {
                        target = BigBoard[reg_postion].Item1;
                    }
                    else if (SN == SN_L)
                    {
                        target = BigBoard[reg_postion].Item2;
                    }
                }
                else if (reg_board_type == BORAD_TYPE_SMALL)
                {
                    // Small Board
                    if (SN == SN_R)
                    {
                        target = SmallBoard[reg_postion].Item1;
                    }
                    else if (SN == SN_L)
                    {
                        target = SmallBoard[reg_postion].Item2;
                    }

                }
                else
                {
                    MessageBox.Show("未知的版型 {0}", reg_board_type.ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return target;
        }

        private void EnqueueImage(string SN, string imageFile)
        {

            // 更新状态

            UPDATE_PROGRESS(STEP.Copy_Image);

            /*


            检测处理后的图：日期_版型_版号_点位_序列号_镍片号.jpg
            20220716115919_0_7021002580_23_L_B-V10_NG.jpg
             
             */


            string target = GetTargetName(SN);


            string datetime = DateTime.Now.ToString("yyyyMMddhhmmss");
            string newImageFileName = datetime + "_"                // 日期
                            + reg_board_type.ToString() + "_"            // 版型
                            + reg_board_id.ToString() + "_"              // 版号
                            + reg_postion.ToString() + "_"     // 点位
                            + SN + "_"                              // 序列号   
                            + target + ".jpg";                      // 镍片号

            string newFileName = "D:\\temp\\" + newImageFileName;

            File.Move(imageFile, newFileName);
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.ffff") + " Move {0} to {1} ", imageFile, newFileName);

            ImageQueueBuffer.Enqueue(newFileName);
            Console.WriteLine("push ImageQueueBuffer.Count {0}", ImageQueueBuffer.Count());

        }


        public enum STEP//IO端口状态
        {
            /******  流程梳理       
             * 0.等待PLC采集命令
             * 1.采集图像1、2
             * 2.拷贝图像1、2
             * 3.发送处理图像命令1、2
             * 4.接收处理结果1、2
             * 5.给PLC写入处理结果1、2
             * ***/
            Start_Acq = 0, // AsyncGetImage 2次
            Copy_Image,   // Equeue 2次
            Process_Image, // Process 2次
            Write_Result, // Write 2次数
            Idle,
        }

        int progressValue = 0;
        void UPDATE_PROGRESS(STEP step)
        {
            switch (step)
            {
                case STEP.Start_Acq:
                    toolStripStatusLabel_ProgressInfo.Text = "采集图像";
                    progressValue = 20;

                    break;
                case STEP.Copy_Image:
                    toolStripStatusLabel_ProgressInfo.Text = "保存图像";
                    progressValue += 10;

                    break;
                case STEP.Process_Image:
                    toolStripStatusLabel_ProgressInfo.Text = "分析图像";
                    progressValue += 20;
                    break;
                case STEP.Write_Result:
                    toolStripStatusLabel_ProgressInfo.Text = "发送结果";
                    progressValue += 10;
                    break;
                case STEP.Idle:
                    toolStripStatusLabel_ProgressInfo.Text = "等待采集指令";
                    progressValue = 0;
                    break;

            }

            if (progressValue > 100) progressValue = 100;

            toolStripProgressBar.Value = progressValue;


        }
        private int AsyncImageCallback(LU_DEVICE device, byte[] pImgData, int nDataBuf, string pFile)
        {
            string deviceSN = System.Text.Encoding.ASCII.GetString(device.uvcIdentity.Name);
            deviceSN = deviceSN.Split('_').ElementAt(0);

            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.ffff") + " AsyncImageCallback come 设备号: " + deviceSN);


            this.BeginInvoke(new Action(() =>
            {

                if (!string.IsNullOrEmpty(pFile))
                {

                    // 删除不要的图像 bmp , raw

                    try
                    {
                        File.Delete(pFile.Replace("bmp", "raw"));
                        File.Delete(pFile);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }


                    Console.WriteLine("device SN : {0}", deviceSN);
                    // 拷贝图像
                    EnqueueImage(deviceSN, pFile.Replace("bmp", "jpg"));


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

                SerialPortControler_RS232PROTOCOL_MC110.Instance.XRayOff();
                SerialPortControler_RS232PROTOCOL_MC110.Instance.CloseControlSystem();

                ImageProcessTcpClient.Disconnect();
                imageProcessServices.Kill();

            }
            catch
            {

            }

            System.Windows.Forms.Application.Exit();
        }





        /// <summary>
        /// 连接高压串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void buttonConnectHVPort_Click(object sender, EventArgs e)
        {

            if (buttonConnectHVPort.Text == "关闭光源串口")
            {
                SerialPortControler_RS232PROTOCOL_MC110.Instance.CloseControlSystem();
                toolStripStatusLabel_HVConn.Text = "高压-已断开";
                buttonConnectHVPort.Text = "打开光源串口";
            }
            else
            {
                OpenXraySerialPortAndInit();
                
                buttonConnectHVPort.Text = "关闭光源串口";

            }



        }

        /// <summary>
        /// 连接PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void buttonConnectPLC_Click(object sender, EventArgs e)
        {
            if (buttonConnectPLC.Text == "连接PLC")
            {
                try
                {

                    bool ret = PLCHelperModbusTCP.fnGetInstance().ConnectServer(textBoxIpAddress.Text, textBoxPort.Text);

                    if (!ret)
                    {
                        MessageBox.Show("PLC连接失败 " + textBoxIpAddress.Text + ":" + textBoxPort.Text);
                        return;
                    }



                    monitorPlcCommandThread = new Thread(new ThreadStart(delegate
                    {
                        MonitorPLC();
                    }));

                    monitorPlcCommandThread.Start();
                    buttonConnectPLC.Text = "断开PLC";


                }
                catch
                {
                    MessageBox.Show("PLC连接失败 " + textBoxIpAddress.Text + ":" + textBoxPort.Text);
                }

            }
            else
            {

                // 断开PLC　
                PLCHelperModbusTCP.fnGetInstance().DisConnectServer();
                buttonConnectPLC.Text = "连接PLC";

            }
        }


        private void textBoxKV_TextChanged(object sender, EventArgs e)
        {

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


            //  SerialPortControler_RS232PROTOCOL_MC110.Instance.SetKV(kv);

        }



        private void textBoxCurrent_TextChanged(object sender, EventArgs e)
        {

            double power = 0.0f;

            if (!double.TryParse(textBoxCurrent.Text.ToString(), out power))
            {

                MessageBox.Show("输入的参数非法");
                return;
            }
            if (power > HV_MaxPower)
            {
                MessageBox.Show("输入的参数超过最大值 " + HV_MaxPower.ToString());
                return;
            }


            //  SerialPortControler_RS232PROTOCOL_MC110.Instance.SetCurrent(current);
        }


        private void XrayOnOff(bool on)
        {

            if (on)
            {
                // 打开光源


                SerialPortControler_RS232PROTOCOL_MC110.Instance.XRayOn();
                Thread.Sleep(200);
                SerialPortControler_RS232PROTOCOL_MC110.Instance.XRayOn();
                buttonXrayOnOff.Text = "关闭光源";
            }
            else
            {
                SerialPortControler_RS232PROTOCOL_MC110.Instance.XRayOff();
                Thread.Sleep(200);
                SerialPortControler_RS232PROTOCOL_MC110.Instance.XRayOff();
                buttonXrayOnOff.Text = "打开光源";
            }

        }

        private void buttonXrayOnOff_Click(object sender, EventArgs e)
        {

            if (!SerialPortControler_RS232PROTOCOL_MC110.Instance.IsOpen())
            {

                MessageBox.Show("请先打开高压串口！");
                return;
            }

            if (buttonXrayOnOff.Text == "打开光源")
            {
                // 打开光源

                XrayOnOff(true);

            }
            else
            {
                // 关闭
                XrayOnOff(false);
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
                    // 图像处理服务
                    ImageProcessTcpClient.Disconnect();
                    imageProcessServices.Kill();

                    // 光源
                    SerialPortControler_RS232PROTOCOL_MC110.Instance.XRayOff();
                    SerialPortControler_RS232PROTOCOL_MC110.Instance.CloseControlSystem();

                    // PLC
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


        private void buttonClearImage_Click(object sender, EventArgs e)
        {

            if (pictureBoxImage.Image != null) {
                pictureBoxImage.Image.Dispose();
            }

            if (pictureBoxImage1.Image != null) {
                pictureBoxImage1.Image.Dispose();
            }
            
           
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
        void ControlSystem_TemperatureChanged(string arg)
        {

            this.BeginInvoke(new Action(() =>
            {
                toolStripStatusLabel_HV_Temp.Text = arg + "℃";
                //this.Log("高压温度: " + lblHV_Temperature.Content.ToString());
            }));
        }

        /// <summary>
        /// 电流监控
        /// </summary>
        /// <param name="arg"></param>
        void ControlSystem_CurrentChanged(string arg)
        {
            this.BeginInvoke(new Action(() =>
            {
                toolStripStatusLabel_HV_Cur.Text = arg + "(unit:0.1uA)";
            }));
        }
        /// <summary>
        /// 电压监控
        /// </summary>
        /// <param name="arg"></param>
        void ControlSystem_VoltageChanged(string arg)
        {
            this.BeginInvoke(new Action(() =>
            {
                toolStripStatusLabel_HV_KV.Text = arg + "V";
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
                    toolStripStatusLabel_XrayOnOff.ForeColor = Color.Red;
                    pictureBoxXrayOnOff.Image = Properties.Resources.fushe_red;
                    IsXrayOn = true;
                    Console.WriteLine("光源回调 打开， IsXrayOn = {0}", IsXrayOn);

                }
                else
                {
                    toolStripStatusLabel_XrayOnOff.Text = "Xray OFF";
                    toolStripStatusLabel_XrayOnOff.ForeColor = Color.Black;
                    pictureBoxXrayOnOff.Image = Properties.Resources.fushe_black;
                    IsXrayOn = false;
                    Console.WriteLine("光源回调 关闭， IsXrayOn = {0}", IsXrayOn);

                }

                pictureBoxXrayOnOff.Invalidate();
            }));

        }
        void ControlSystem_Connected()
        {
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
                toolStripStatusLabel_HVError.ForeColor = Color.Red;
            }));
        }
        /// <summary>
        /// 初始化串口控制器
        /// </summary>
        private void InitilizeControlSystem()
        {
            try
            {

                SerialPortControler_RS232PROTOCOL_MC110.Instance.XRayOnChanged += ControlSystem_XRayOnChanged;
                SerialPortControler_RS232PROTOCOL_MC110.Instance.VoltageChanged += ControlSystem_VoltageChanged;
                SerialPortControler_RS232PROTOCOL_MC110.Instance.CurrentChanged += ControlSystem_CurrentChanged;
                SerialPortControler_RS232PROTOCOL_MC110.Instance.TemperatureChanged += ControlSystem_TemperatureChanged;
                SerialPortControler_RS232PROTOCOL_MC110.Instance.StateReported += ControlSystem_StateReported;
                SerialPortControler_RS232PROTOCOL_MC110.Instance.Connected += ControlSystem_Connected;

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




        private void textBoxActTime_TextChanged(object sender, EventArgs e)
        {
            int actTime = 100;
            if (!int.TryParse(textBoxActTime.Text, out actTime))
            {
                MessageBox.Show("无效的参数，格式不正确，必须是整数");
                return;
            }
            if (actTime > SensorGetImageTimeOut)
            {
                MessageBox.Show("无效的参数，ACT时间最大为 " + SensorGetImageTimeOut.ToString() + " ms");
                return;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            OneKeyStart();
        }
    }
}
