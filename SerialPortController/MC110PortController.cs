using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace SerialPortController
{


    /// <summary>
    /// 端口通讯控制中心
    /// </summary>
    public class SerialPortControler_RS232PROTOCOL_MC110
    {
        const byte STX = 0x3C;
        const byte CR = 0x3E;
        const byte SP = 0x20;
        Char StartTag = (Char)(STX);
        Char EndTag = (Char)(CR);
        private static SerialPortControler_RS232PROTOCOL_MC110 _instance;
        private bool _running = false;
        private bool _is_open = false;

        public static SerialPortControler_RS232PROTOCOL_MC110 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SerialPortControler_RS232PROTOCOL_MC110();
                return _instance;
            }
        }

        private SerialPortControler_RS232PROTOCOL_MC110()
        {
            InitilizeHVStatusThread();
        }
        /// <summary>
        /// 初始化高压XRay曝光状态
        /// </summary>
        private void InitilizeHVStatusThread()
        {
            new Thread(new ThreadStart(delegate
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    try
                    {
                        if (_serialPort == null)
                            continue;
                        GetHVStatus();


                    }
                    catch { }
                }
            }))
            { IsBackground = true }.Start();

        }
        #region 字段
        SerialPort _serialPort;
        #endregion



        /// <summary>

        /// <summary>
        /// 打开端口
        /// </summary>
        public void OpenSerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopbits)
        {

            try {
                Console.WriteLine("{0},{1}", portName, baudRate);
                _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopbits);
                _serialPort.DataReceived += _serialPort_DataReceived;
                _serialPort.WriteTimeout = 1000;
                _serialPort.ReadTimeout = 1000;
                _serialPort.Open();
                _is_open = true;
                _running = true;

                if (Connected != null)
                {
                    Connected();
                }
            }
            catch (Exception e) {
                _is_open = false;
                _running = false;
                MessageBox.Show(e.Message);
            }


        }

        public bool IsOpen()
        {
            return _is_open;
        }
        public void CloseControlSystem()
        {
            _running = false;
            _serialPort.Close();
        }

        string recieve_buffer_;
        bool cacheSerialPortMessage(string head_tag, string tail_tag,
                                         string data, ref string message)
        {

            bool completed = false;
            // 异常类：无头且变量为空，已丢失头部，数据不可靠，直接返回

            if ((!data.Contains(head_tag)) && (recieve_buffer_.Length == 0))
            {
                return false;
            }
            // 第一种：有头无尾，先清空原有内容，再附加
            if ((data.Contains(head_tag)) && (!data.Contains(tail_tag)))
            {
                recieve_buffer_ = "";
                recieve_buffer_.Concat(data);
            }
            // 第二种：无头无尾且变量已有内容，数据中段部分，继续附加即可
            if ((!data.Contains(head_tag)) && (!data.Contains(tail_tag)) &&
                (recieve_buffer_.Length == 0))
            {
                recieve_buffer_.Concat(data);
            }
            // 第三种：无头有尾且变量已有内容，已完整读取，附加后输出数据，并清空变量
            if ((!data.Contains(head_tag)) && (data.Contains(tail_tag)) &&
                (recieve_buffer_.Length == 0))
            {
                recieve_buffer_.Concat(data);
                message = recieve_buffer_;
                recieve_buffer_ = "";
                completed = true;
            }
            // 第四种：有头有尾（一段完整的内容），先清空原有内容，再附加，然后输出，最后清空变量
            if ((data.Contains(head_tag)) && (data.Contains(tail_tag)))
            {
                recieve_buffer_ = "";
                recieve_buffer_.Concat(data);
                message = recieve_buffer_;
                recieve_buffer_ = "";
                completed = true;
            }

            return completed;
        }

        void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = sender as SerialPort;

            while (_running && _serialPort.BytesToRead > 0)
            {
                Thread.Sleep(30);
                byte[] buffer = new byte[1024];
                int len = port.Read(buffer, 0, buffer.Length);
                string data = ASCIIEncoding.ASCII.GetString(buffer, 0, len);
                string message = "";
                Console.WriteLine("Serial Port Receive[" + DateTime.Now.ToString("HH:mm:ss.ffff") + "] " + data);

                /*

                bool completed = cacheSerialPortMessage("<", ">", data, ref message);
                if (!completed)
                {
                    Console.WriteLine("消息不完整！");
                    continue;
                }
                */
                ReceivedMessage(data);

            }

        }

        public delegate void DoubleValueChangedDelegate(double arg);
        public delegate void BoolValueChangedDelegate(bool arg);
        public delegate void UIntValueChangedDelegate(uint arg);
        public delegate void EventHandler();
        public delegate void ReportedHandler(string report);
        /// <summary>
        /// 电压改变
        /// </summary>
        public event ReportedHandler VoltageChanged;

        /// <summary>
        /// 温度改变
        /// </summary>
        public event ReportedHandler TemperatureChanged;

        /// <summary>
        /// 电流改变
        /// </summary>
        public event ReportedHandler CurrentChanged;

        /// <summary>
        /// 清空状态错误
        /// </summary>
        public event EventHandler FaultCleared;
        public event EventHandler Connected;
        public event BoolValueChangedDelegate XRayOnChanged;
        public event BoolValueChangedDelegate WatchDogEnableChanged;
        public event BoolValueChangedDelegate WatchDogOnChanged;
        public event ReportedHandler StateReported;

        private bool _isNeedFeeddingDog = false;
        public bool IsWarming = false;


        public string TS_Step = "";
        public string TS_Volt_Step = "";
        public string TS_Pwr_Step = "";
        public string TS_Elapsed_Time = "";

        /// <summary>
        /// 收到命令答复
        /// </summary>
        /// <param name="message"></param>
        private void ReceivedMessage(string message)
        {
            /*
            {

                string ResponseError = message.Split('[').Last().TrimEnd(']'); // --> ERR:27,29
                string[] ResponseErrorCode = ResponseError.Split(new Char[] { ':', ',' });
                string error_message = string.Empty;
                for (int i = 1; i < ResponseErrorCode.Length; i++)
                {
                    string error_code = ResponseErrorCode[i]; //ERR 27 29
                    switch (error_code)
                    {
                        case "0":
                            {
                                error_message = string.Empty;
                            }
                            break;
                        case "1": error_message += "Invalid Command"; break;
                        case "2": error_message += "Exceed Maximum Specified Value"; break;
                        case "3": error_message += "Exceed Voltage Limit"; break;
                        case "4": error_message += "Exceed Current Limit"; break;
                        case "5": error_message += "Exceed Power Limit"; break;
                        case "6": error_message += "Heater Disabled"; break;
                        case "7": error_message += "Power Supply Disabled"; break;
                        case "8": error_message += "Ade Voltage Interlock Disabled"; break;
                        case "9": error_message += "Heater Interlock Disabled"; break;
                        case "10": error_message += "Curve Power Same"; break;
                        case "11": error_message += "Heater Stabilizing"; break;
                        case "12": error_message += "Over Voltage Fault"; break;
                        case "13": error_message += "Under Voltage Fault"; break;
                        case "14": error_message += "Over Current Fault"; break;
                        case "15": error_message += "Under Current Fault"; break;
                        case "16": error_message += "Temperature Fault"; break;
                        case "17": error_message += "Arc Event Fault"; break;
                        case "18": error_message += "Heater Fault"; break;
                        case "19": error_message += "Reserved"; break;
                        case "20": error_message += "Reserved"; break;
                        case "21": error_message += "I2C Fault"; break;
                        case "22": error_message += "Reserved"; break;
                        case "23": error_message += "Input Voltage Fault"; break;
                        case "24": error_message += "Input Current Fault"; break;
                        case "25": error_message += "G1 Fault"; break;
                        case "26": error_message += "Reserved"; break;
                        case "27": error_message += "G3 Fault"; break;
                        case "28": error_message += "Temperature Preheat"; break;
                        case "29": error_message += "Tube Conditioning"; break;
                        case "30": error_message += "Tube Conditioning Profile"; break;
                        case "31": error_message += "Loda Date"; break;
                        default: error_message += "Unknow"; break;
                    }
                }
                if (StateReported != null && !string.IsNullOrEmpty(error_message))
                    StateReported(error_message);
            }

            */

            if (message.StartsWith("<STATUS:"))
            {

                /**
                 Examples (Tube Conditioning ON):
                1. <STATUS:0,0,1,1,1,2,60000,90,1:45,60000, 150[ERR:29]>

                Command String:
                <GSTAT>
                Response String:
                Tube Conditioning Not Active:
                <STATUS:HT-I, AV-I,HT-E,AV-E, Tube Conditioning Active, Anode Voltage, Tube Current[Err:X]> 
                Tube Conditioning Active:
                <STATUS:HT-I, AV-I,HT-E,AV-E, Tube Conditioning Active, TS Volt Step, TS Pwr Step, TS Elapsed Time, 
                Anode 
                Voltage, Tube Current[Err:X]>
                Examples (Tube Conditioning OFF):
                1. Status when the Interlocks are active.
                A. <STATUS:1,1,0,0,0,0,0[ERR:8,9]>
                2. Status when the heater is enabled and is warming up.
                A. <STATUS:0,0,1,0,0,0,0[ERR:11]>
                3. Status when the power supply is in active operation.
                A. <STATUS:0,0,1,1,0,30000,3640[ERR:0]>
                Examples (Tube Conditioning ON):
                1. <STATUS:0,0,1,1,1,2,60000,90,1:45,60000, 150[ERR:29]>

                 */

                string[] msg = message.Split(',');
                string power_enable = msg.ElementAt(3);
                string TubeContionActivate = msg.ElementAt(4);
                string AV = "";
                string TC = "";


                Console.WriteLine("power_enable flag : " + power_enable);
                if (power_enable == "1" && XRayOnChanged != null) {
                        XRayOnChanged(true);
                }
                if (power_enable == "0" && XRayOnChanged != null)
                {
                       XRayOnChanged(false);
                }



                if ("1" == TubeContionActivate)
                {
                    IsWarming = true;
                    //  Console.WriteLine("Tube Conditioning ON");

                    TS_Step = msg.ElementAt(5);
                    TS_Volt_Step = msg.ElementAt(6);
                    TS_Pwr_Step = msg.ElementAt(7);
                    TS_Elapsed_Time = msg.ElementAt(8);
                    AV = msg.ElementAt(9);
                    TC = msg.ElementAt(10).Split('[').ElementAt(0);
                }
                else
                {
                    IsWarming = false;
                    // Console.WriteLine("Tube Conditioning OFF");
                    AV = msg.ElementAt(5);
                    TC = msg.ElementAt(6).Split('[').ElementAt(0);

                }

                // 显示电压和电流
                Console.WriteLine("AV {0},TC {1}", AV, TC);
                if (VoltageChanged != null) {
                    VoltageChanged(AV);
                }

                if (CurrentChanged != null) {
                    CurrentChanged(TC);
                }
                   
 

            }


            if (message.StartsWith("<TMP")) {

                /*
                 Example (Get Temperature Sensor 1, Returned temp is -10C):
                Get Temperature from sensor 1:
                <GTMP:1>
                Response:
                <TMP1:-10[ERR:0]>
                 
                 */

                string[] msg = message.Split(new Char[] {':' , '['});
                string tmp = msg[1];

                if (TemperatureChanged != null) { 
                    TemperatureChanged(tmp);
                }


            }


            return;

        }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="message"></param>
        public void SendCommand(string message)
        {

            List<byte> command = new List<byte>() { STX };
            byte[] cmd = ASCIIEncoding.ASCII.GetBytes(message);
            command.AddRange(cmd);
            command.Add(CR);


            if (_serialPort.IsOpen)
            {
                _serialPort.Write(command.ToArray(), 0, command.Count);
                _serialPort.WriteLine("");
                
                Console.WriteLine("Send to [" + DateTime.Now.ToString("HH:mm:ss.ffff") + "] " + command.ToString());


            }
            else {
                Console.WriteLine("发送命令失败，串口未打开");
            }
            _isNeedFeeddingDog = false;



        }
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="message"></param>
        public void SendCommand(byte[] message)
        {
            List<byte> command = new List<byte>() { STX };
            command.AddRange(message);
            command.Add(CR);

            _serialPort.Write(command.ToArray(), 0, command.Count);
        }
        /// <summary>
        /// 关闭端口
        /// </summary>
        public void ClosePort()
        {
            try
            {
                _serialPort.Close();
                //ConfigHelper.Save(PORTPARAPATH, PortPara);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 设定采集张数
        /// </summary>
        /// <param name="times"></param>
        public void SetGrabFrames(Int32 times)
        {
            //WriteMultiRegister(byte.Parse(PortPara.SetGrabFramesADD), BitConverter.GetBytes(times));
        }
        /// <summary>
        /// 设定曝光时间
        /// </summary>
        /// <param name="times"></param>
        public void SetGrabTime(ushort time)
        {
            //WriteMultiRegister(byte.Parse(PortPara.SetGrabTimeADD), BitConverter.GetBytes(time));
        }
        /// <summary>
        /// 设定帧时间
        /// </summary>
        /// <param name="times"></param>
        public void SetFrameTime(ushort time)
        {
            //WriteMultiRegister(byte.Parse(PortPara.SetFrameTimeADD), BitConverter.GetBytes(time));
        }
        /// <summary>
        /// 开始采集
        /// </summary>
        public void StartGrab()
        {
            //WriteSingleRegister(byte.Parse(PortPara.StartAdd), 0);
        }
        /// <summary>
        /// 结束采集
        /// </summary>
        public void StopGrab()
        {
            //WriteSingleRegister(byte.Parse(PortPara.StartAdd), 1);
        }

        ///// <summary>
        ///// 合成指令
        ///// </summary>
        ///// <param name="addr"></param>
        ///// <param name="val"></param>
        //public void WriteSingleRegister(ushort addr, byte val)
        //{
        //    byte[] buf = { 0x7F, 0x01, 0x02, 0x03, 0x03, 0x00, 0x00, 0x00, 0x00 };

        //    buf[5] = (byte)(addr & 0xff);
        //    buf[6] = (byte)(addr >> 8);
        //    buf[7] = val;

        //    buf[8] = GetCheckSum(buf, 7);

        //    _serialPort.Write(buf, 0, buf.Length);
        //}
        ///// <summary>
        ///// 合成多位数据指令
        ///// </summary>
        ///// <param name="addr"></param>
        ///// <param name="buffer"></param>
        //public void WriteMultiRegister(ushort addr, byte[] buffer)
        //{
        //    int len = buffer.Length;
        //    if (len > 64) return;

        //    byte[] vBuf = new byte[128];
        //    byte[] cmd = { 0x7F, 0x01, 0x02, 0x04, 0x05, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00 };
        //    cmd.CopyTo(vBuf, 0);

        //    vBuf[4] = (byte)(3 + len);
        //    vBuf[5] = (byte)(addr & 0xff);
        //    vBuf[6] = (byte)(addr >> 8);
        //    vBuf[7] = (byte)len;

        //    buffer.CopyTo(vBuf, 8);

        //    vBuf[len + 8] = GetCheckSum(vBuf, len + 7);

        //    _serialPort.Write(vBuf, 0, buffer.Length + 9);
        //}
        /// <summary>
        /// 计算校验位数值
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public byte GetCheckSum(byte[] buffer, int len)
        {
            byte sum = 0;

            for (int i = 0 + 1; i < len + 1; i++)
            {
                sum += buffer[i];
            }

            sum = (byte)(~sum + 1);

            return sum;
        }
        /// <summary>
        /// 高压复位
        /// </summary>
        public void ResetHV()
        {
            SendCommand("CLERR");
        }
        /// <summary>
        /// 预热
        /// </summary>
        public void Preheat(double kv, double p)
        {
            new Thread(new ThreadStart(delegate
            {
                System.Threading.Thread.Sleep(200);
                SetKV(kv);
                System.Threading.Thread.Sleep(200);
                SetPower(p);
                System.Threading.Thread.Sleep(200);
                XRayOn();

            })).Start();

        }
        public void MC110UpdateCMD(double kv, double p)
        {

            Console.WriteLine("更新光源设置参数 电压：{0}， 功率：{1}", kv, p);

            new Thread(new ThreadStart(delegate
            {
                System.Threading.Thread.Sleep(200);
                SetKV(kv);
                System.Threading.Thread.Sleep(200);
                SetPower(p);
                System.Threading.Thread.Sleep(200);
                SendCommand("UPDT");

            })).Start();
        }

        public void XRayOn()
        {
            Console.WriteLine("执行打开光源");
            SendCommand("EP:1");
            
        }
        public void XRayOff()
        {
            Console.WriteLine("执行关闭光源");
            SendCommand("EP:0");
            

        }

        /// <summary>
        /// 设置KV
        /// </summary>
        /// <param name="kv"></param>
        public void SetKV(double kv)
        {
            if (kv < 0)
                kv = 0;
            uint v = ((uint)(kv * 1000));
            Console.WriteLine("设置电压为：{0} v", v);
            SendCommand("SAV:" + v.ToString());
        }
        /// <summary>
        /// 设置电流
        /// </summary>
        /// <param name="ua"></param>
        public void SetCurrent(int ua)
        {
            // MC110光源不支持设置电流
        }
        /// <summary>
        /// 设置功率
        /// </summary>
        /// <param name="p"></param>
        public void SetPower(double p)
        {
            if (p < 0)
                p = 0;
            uint power = ((uint)(p * 10));
            Console.WriteLine("设置功率为：{0}", power);
            SendCommand("SPWR:" + power.ToString());
        }

        /// <summary>
        /// 获取高压状态
        /// </summary>
        public void GetHVStatus()
        {
            /*
             Get Anode Voltage Monitor  <GAVM>   20000-110000    1V 
             Get Tube Current Monitor   <GTCM> 0-3750     0.1uA
             Get Heater Voltage Monitor  <GHVM>     0-10000     1mV 
             Get Heater Current Monitor  <GHCM>     0-1000     1mA 
             Get Grid 1 Voltage Monitor  <GG1V> 0-2500     10mV
             Get Grid 1 Current Monitor  <GG1C> 0-5000     1uA 
             Get Grid 2 Voltage Monitor  <GG2V> 0-1000 1V 
             Get Grid 2 Current Monitor  <GG2C> 0-5000     1uA 
             Get Grid 3 Voltage Monitor  <GG3V> 0-1500 1V Get 
             Grid 3 Current Monitor  <GG3C> 0-5000     1uA 
             Get Date Code <GDC> YYWW 
             Get Firmware Version    <GFV> 
             Get Part Number     <GPN> 
             Get Tube Number     <GTN> 
             Get Operating Time    <GOT> H:M:S 
             Get Serial Number    <GSN> 
             Get Temperature    <GTMP:x     x=1,2> °C 
             Enable Bootloader Mode   <EBLM> Get Error Report     <GRPT> 
             */

            SendCommand("GTMP:1");
            Thread.Sleep(500);
            SendCommand("GTMP:2");
            Thread.Sleep(500);
            SendCommand("GSTAT");

        }

        public void Connect(double kv, double p)
        {
            /*
            1 Send the current date and wait for conditioning to complete – <SDATE:DD,MM,YYYY>
            2 Program the Anode Voltage Level – <SAV:XXXXXX>
            3 Program Power Level - <SPWR:XXXX>
            4 Enable the Heater - <EH:1>
            5 Enable the Power Supply - <EP:1>
            */

            DateTime dt = DateTime.Now;
            string year = dt.Year.ToString();
            string day = dt.Day.ToString();
            string month = dt.Month.ToString();
            string date = day + "," + month + "," + year;

            // date = "31,12,2023";
            Console.WriteLine("SDATE {0}", date);
            SendCommand("SDATE:" + date);
            Thread.Sleep(200);
            SetKV(kv);
            Thread.Sleep(200);
            SetPower(p);
            Thread.Sleep(200);
            SendCommand("EH:1");
            Thread.Sleep(2000);


        }
    }
}
