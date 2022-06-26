using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

namespace SerialPortController
{
    public class PLCSerialPortController
    {
        #region 字段
        SerialPort _serialPort;

        bool _running;
        #endregion

        public bool IsOpen() {
            return _running;
        }


        #region 属性
        private static PLCSerialPortController _instance;
        public static PLCSerialPortController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PLCSerialPortController();
                return _instance;
            }
        }
        private static string PORTPARAPATH = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "PLC_SerialPortConfig.xml");

        /// <summary>
        /// 数据位
        /// </summary>
        public SerialPortController.Setting.PortPara PortPara
        {
            get
            {
                return ConfigHelper.Get<SerialPortController.Setting.PortPara>(PORTPARAPATH);
            }
        }
        #endregion

        /// <summary>
        /// 打开端口
        /// </summary>
        public void OpenSerialPort()
        {
            _serialPort = new SerialPort(PortPara.PortName, PortPara.BaudRate, PortPara.Parity, PortPara.DataBits, PortPara.StopBits);

            _serialPort.DataReceived += _serialPort_DataReceived;
            _serialPort.WriteTimeout = 1000;
            _serialPort.ReadTimeout = 1000;

            _running = true;
            _serialPort.Open();
        }

        public void CloseSerialPort() {
            _running = false;
            _serialPort.Close();
        }

        void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = sender as SerialPort;

            while (_running && _serialPort.BytesToRead > 0)
            {
                Thread.Sleep(30);
                byte[] buffer = new byte[1024];
                int len = port.Read(buffer, 0, buffer.Length);
                string message = ASCIIEncoding.ASCII.GetString(buffer, 0, len);
#if DEBUG
                // Console.WriteLine("Receive-" + DateTime.Now.ToString("HH:mm:ss.ffff") + "=" + message);
#endif

            }

        }

    }
}
