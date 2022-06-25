using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Net;
using System.Net.Sockets;
using System.IO;              //流StreamRea
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace LionSDKDotDemo
{
    class TcpClient
    {
        private Socket clientSocket_;
        private Socket heartBeatSocket_;
        private Thread heartBeatThread_;
        private byte[] receiveBuffer = new byte[32];

        const byte CR = 0x0D;
        const byte LF = 0x0A;

        public void Connect() {

            IPAddress serverIp = IPAddress.Parse("127.0.0.1");

            try
            {
                // 创建心跳通道
                const int heartBeatPort = 12341;
                heartBeatSocket_ = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                heartBeatSocket_.Connect(new IPEndPoint(serverIp, heartBeatPort));

                heartBeatThread_ = new Thread(new ThreadStart(delegate { SendHeartBeat(); }));
                heartBeatThread_.Start();

                // 创建数据通道
                const int dataPort = 12342;
                clientSocket_ = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket_.Connect(new IPEndPoint(serverIp, dataPort));
            }
            catch (Exception ex)
            {

                MessageBox.Show("图像检测服务连接失败！ " +  ex.ToString());


            }


        }

        private void SendHeartBeat() {
            while (true) {

                Thread.Sleep(500);
                byte[] msg = {0xff, CR, LF };
                heartBeatSocket_.Send(msg);
                int len = heartBeatSocket_.Receive(receiveBuffer);

                Console.WriteLine("beat...");

            }
        }

        /// <summary>
        /// 分析图像
        /// </summary>
        /// <returns> 返回0表示OK，返回1表示NG， 返回-1表示错误</returns>
        public int SendAnalyseImageCommand()
        {
            try
            {
                //
                List<byte> command = new List<byte>();
                byte[] cmd = ASCIIEncoding.ASCII.GetBytes("T1,");
                command.AddRange(cmd);
                command.Add(CR);
                command.Add(LF);
                clientSocket_.Send(command.ToArray());


                //"3,OK"
                byte[] recvMsg = new byte[32];
                int len = clientSocket_.Receive(recvMsg);
                String s = Encoding.Default.GetString(recvMsg, 0, len);
                MessageBox.Show(s);
                if (s.Contains("OK"))
                {
                    return 0;
                }
                else if (s.Contains("NG"))
                {
                    return 1;
                }
                else
                {
                    return -2;
                }

            }
            catch {
                MessageBox.Show("图像分析指令发送失败！");
                return -1;
            }
 
        }



    }
}
