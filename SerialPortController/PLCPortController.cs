using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace SerialPortController
{
    public class PLCPortController
    {

        SerialPort _serialPort;
        private static PLCPortController _instance;
        public static PLCPortController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PLCPortController();
                return _instance;
            }
        }


        public void OpenSerialPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopbits)
        {
            _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopbits);
          //  _serialPort.DataReceived += _serialPort_DataReceived;
            _serialPort.WriteTimeout = 1000;
            _serialPort.ReadTimeout = 1000;
            _serialPort.Open();
        }
        public void CloseSerialPort()
        {
            _serialPort.Close();
          
        }

        public bool IsOpen() {
            try
            {
                return _serialPort.IsOpen;
            }
            catch {
                return false;
            }
            
        }


    }
}
