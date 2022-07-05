using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SerialPortController
{


    public struct PortPara {

        public string PortName;
        public int BaudRate;
        public int DataBits;
        public int StopBits;
        public System.IO.Ports.Parity Parity;
    }

    public class Config
    {


        private static Config _instance;
        public static Config Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Config();
                return _instance;
            }
        }

        static string INI_FILE = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "config.ini");

        public string ReadString(string section, string key) {
            return IniFile.ReadString(section, key, INI_FILE);
        }
        public void WriteString(string section, string key, string value) {
            IniFile.WriteString(section,key,value,INI_FILE);
        }
        public PortPara GetPortPara(string section) {

            PortPara para;

            para.PortName =  IniFile.ReadString(section, "PortName", INI_FILE);
            para.BaudRate = int.Parse(IniFile.ReadString(section, "BaudRate", INI_FILE));
            para.DataBits = int.Parse(IniFile.ReadString(section, "DataBits", INI_FILE));
            para.StopBits = int.Parse(IniFile.ReadString(section, "StopBits", INI_FILE));

            string parity = IniFile.ReadString(section, "Parity", INI_FILE);
            if (parity == "None")
            {
                para.Parity = Parity.None;
            }
            else if (parity == "Even") {
                para.Parity = Parity.Even;
            }
            else if (parity == "Odd")
            {
                para.Parity = Parity.Odd;
            }
            else  
            {
                para.Parity = Parity.None;
            }

            return para;
        }


    }

}
