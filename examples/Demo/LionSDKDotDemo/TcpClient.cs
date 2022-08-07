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
        bool running_ = false;

        public bool IsConnected() {
            return running_;
        }

        public void Disconnect() {
            running_ = false;
            heartBeatSocket_.Disconnect(false);
            clientSocket_.Disconnect(false);
        }
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

                // 创建数据通道`
                const int dataPort = 12342;
                clientSocket_ = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket_.Connect(new IPEndPoint(serverIp, dataPort));

                running_ = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("图像检测服务连接失败！ " +  ex.ToString());

            }


        }
 
        private void SendHeartBeat() {
            while (running_) {
                try
                {
                    Thread.Sleep(500);
                    byte[] msg = { 0xff, CR, LF };
                    heartBeatSocket_.Send(msg);
                    int len = heartBeatSocket_.Receive(receiveBuffer);

                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss.ffff") + " heart beat...");
                }
                catch {
                    MessageBox.Show("图像检测服务或已断开连接，心跳检测异常");
                    break;
                }


            }
        }

        public string ProcessImage(string path)
        {
            try
            {
                //
                List<byte> command = new List<byte>();
                byte[] head = { 0xAA, 0xBB };
                byte[] cmd = ASCIIEncoding.ASCII.GetBytes("PS," + path);
                command.AddRange(head);
                command.AddRange(cmd);
                command.Add(CR);
                command.Add(LF);

                Console.WriteLine("send: " + Encoding.Default.GetString(command.ToArray(), 0, command.ToArray().Length));
                clientSocket_.Send(command.ToArray());

                //"3,OK"
                byte[] recvMsg = new byte[64];
                int len = clientSocket_.Receive(recvMsg);

                string s = Encoding.Default.GetString(recvMsg, 0, len);
                Console.WriteLine("recv: " + s);

                return s;

            }
            catch {
                return "";
            }
 
        }



    }
}
