using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;

namespace DAL
{
    public class PLCHelperModbusTCP
    {

        
        private volatile static PLCHelperModbusTCP instance = null;
        private static Object inatanceLock = new Object();
        public static PLCHelperModbusTCP fnGetInstance()
        {
            if (instance == null)
            {
                lock (inatanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PLCHelperModbusTCP();
                    }
                }
            }
            return instance;
        }
        //public IPAddress ip = null;
        //public IPEndPoint point = null;
        //public string plcIPAddress = "192.168.1.5";
        public static Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
  
        int delaytime = 20;
       
        private byte[] ConvertBytes(string sourceStr)
        {
            string[] tmpSrt = sourceStr.Trim().Split(' ');

            byte[] destinationByte = new byte[tmpSrt.Count()];
            for (int i = 0; i < tmpSrt.Count(); i++)
            {
                destinationByte[i] = Convert.ToByte(Convert.ToInt32(tmpSrt[i], 16));
            }
            return destinationByte;
        }

        public void DisConnectServer() {
            socketClient.Disconnect(false);
        }
            
        public bool ConnectServer(string IPstr, string port)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(IPstr);
                IPEndPoint point = new IPEndPoint(ip, Convert.ToInt32(port));
                socketClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 1000);
                socketClient.Connect(point);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("PLC连接失败：" + ex.Message);
            }
        }





        #region 【1】Modbus 指令06,16读取寄存器（D,SD,R,T,C）

        /// <summary>
        /// 写入单个16位数据寄存器
        /// </summary>
        /// <param name="regAddr">数据寄存器地址：100为D100</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] SendWriteSingleDataRegInt16Cmd(int regAddr, short data)
        {
            string send = "01 01 00 00 00 06 FF 06 ";
            string hexRegAddr = Convert.ToString(regAddr, 16).PadLeft(4, '0');
            string hexData = Convert.ToString(data, 16).PadLeft(4, '0');
            send += hexRegAddr.Substring(0, 2) + " " + hexRegAddr.Substring(2, 2) + " " + hexData.Substring(0, 2) + " " + hexData.Substring(2, 2);
            byte[] sendMessage = ConvertBytes(send);
            return sendMessage;           
        }

        /// <summary>
        /// 写入单个16位数据寄存器
        /// </summary>
        /// <param name="regAddr">数据寄存器地址：100为D100</param>
        /// <param name="data">写入的数值</param>
        /// <param name="socketClient">socket</param>
        public void WriteSingleDataRegInt16Cmd(int regAddr, int data,PLCWordRegStartAddr regType= PLCWordRegStartAddr.D0D8511)
        {
            
            string send = "01 01 00 00 00 06 FF 06 ";
            string hexRegAddr = Convert.ToString(regAddr + (int)regType, 16).PadLeft(4, '0');
            string hexData = Convert.ToString(data, 16).PadLeft(4, '0');
            send += hexRegAddr.Substring(0, 2) + " " + hexRegAddr.Substring(2, 2) + " " + hexData.Substring(0, 2) + " " + hexData.Substring(2, 2);
            byte[] sendMessage = ConvertBytes(send);
            socketClient.Send(sendMessage);
            byte[] buffer = new byte[128];
            int r = socketClient.Receive(buffer);
        }

        /// <summary>
        /// 连续写入16位数据寄存器
        /// </summary>
        /// <param name="startAddr"></param>
        /// <param name="dataArray"></param>
        /// <param name="regCount"></param>
        /// <returns>返回值为发送的指令</returns>
        public byte[] SendWriteMultiDataRegInt16Cmd(int startAddr, short[] dataArray, int regCount)
        {
            if (dataArray.Length != regCount)
            {
                MessageBox.Show("数据数组长度与写入寄存器个数不相等");
                return null;
            }
            string send = "01 01 00 00 00 {0} FF 10 ";
            int byteCount = 7 + regCount * 2;
            string hexbyteCount = Convert.ToString(byteCount, 16).PadLeft(2, '0');
            string hexRegCount = Convert.ToString(regCount, 16).PadLeft(4, '0');
            string hexDataCount = Convert.ToString(regCount * 2, 16).PadLeft(2, '0');
            send = String.Format(send, hexbyteCount);
            string hexStartRegAddr = Convert.ToString(startAddr, 16).PadLeft(4, '0');
            send += hexStartRegAddr.Substring(0, 2) + " " + hexStartRegAddr.Substring(2, 2) + " " + hexRegCount.ToString().Substring(0, 2) + " " + hexRegCount.ToString().Substring(2, 2) + " " + hexDataCount;
            string hexData = "";
            for (int i = 0; i < dataArray.Length; i++)
            {
                string sdata = Convert.ToString(dataArray[i], 16).PadLeft(4, '0');
                hexData += " " + sdata.Substring(0, 2) + " " + sdata.Substring(2, 2);
            }
            send += hexData;
            byte[] sendMessage = ConvertBytes(send);
            return sendMessage;
        }

        /// <summary>
        /// 连续写入16位数据寄存器
        /// </summary>
        /// <param name="startAddr"></param>
        /// <param name="dataArray"></param>
        /// <param name="regCount"></param>
        public void WriteMultiDataRegInt16Cmd(int startAddr, short[] dataArray, int regCount, PLCWordRegStartAddr regType = PLCWordRegStartAddr.D0D8511)
        {
            

            if (dataArray.Length != regCount)
            {
                MessageBox.Show("数据数组长度与写入寄存器个数不相等");
                return ;
            }
            string send = "01 01 00 00 00 {0} FF 10 ";
            int byteCount = 7 + regCount * 2;
            string hexbyteCount = Convert.ToString(byteCount, 16).PadLeft(2, '0');
            string hexRegCount = Convert.ToString(regCount, 16).PadLeft(4, '0');
            string hexDataCount = Convert.ToString(regCount * 2, 16).PadLeft(2, '0');
            send = String.Format(send, hexbyteCount);
            string hexStartRegAddr = Convert.ToString(startAddr + (int)regType, 16).PadLeft(4, '0');
            send += hexStartRegAddr.Substring(0, 2) + " " + hexStartRegAddr.Substring(2, 2) + " " + hexRegCount.ToString().Substring(0, 2) + " " + hexRegCount.ToString().Substring(2, 2) + " " + hexDataCount;
            string hexData = "";
            for (int i = 0; i < dataArray.Length; i++)
            {
                string sdata = Convert.ToString(dataArray[i], 16).PadLeft(4, '0');
                hexData += " " + sdata.Substring(0, 2) + " " + sdata.Substring(2, 2);
            }
            send += hexData;
            byte[] sendMessage = ConvertBytes(send);
            socketClient.Send(sendMessage);
            byte[] buffer = new byte[128];
            int r = socketClient.Receive(buffer);
        }
        
        /// <summary>
        /// 写入单个32位数据寄存器
        /// </summary>
        /// <param name="regAddr"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] SendWriteSingleDataRegInt32Cmd(int regAddr, int data)
        {
            string send = "01 01 00 00 00 0B FF 10 ";
            string hexRegAddr = Convert.ToString(regAddr, 16).PadLeft(4, '0');
            string hexData = Convert.ToString(data, 16).PadLeft(8, '0');
            send += hexRegAddr.Substring(0, 2) + " " + hexRegAddr.Substring(2, 2) + " " + "00" + " " + "02" + " " + "04" + " " + hexData.Substring(4, 2) + " " + hexData.Substring(6, 2) + " " + hexData.Substring(0, 2) + " " + hexData.Substring(2, 2);
            byte[] sendMessage = ConvertBytes(send);
            return sendMessage;
        }

        public void WriteSingleDataRegInt32Cmd(int regAddr, int data,PLCWordRegStartAddr regType = PLCWordRegStartAddr.D0D8511)
        {
           
            string send = "01 01 00 00 00 0B FF 10 ";
            string hexRegAddr = Convert.ToString(regAddr + (int)regType, 16).PadLeft(4, '0');
            string hexData = Convert.ToString(data, 16).PadLeft(8, '0');
            send += hexRegAddr.Substring(0, 2) + " " + hexRegAddr.Substring(2, 2) + " " + "00" + " " + "02" + " " + "04" + " " + hexData.Substring(4, 2) + " " + hexData.Substring(6, 2) + " " + hexData.Substring(0, 2) + " " + hexData.Substring(2, 2);
            byte[] sendMessage = ConvertBytes(send);
            socketClient.Send(sendMessage);
            byte[] buffer = new byte[128];
            int r = socketClient.Receive(buffer);
        }

        /// <summary>
        /// 连续写入32位数据寄存器
        /// </summary>
        /// <param name="startAddr"></param>
        /// <param name="dataArray"></param>
        /// <param name="regCount"></param>
        /// <returns></returns>
        public byte[] SendWriteMultiDataRegInt32Cmd(int startAddr, int[] dataArray, int regCount = 2)
        {
            if (dataArray.Length != regCount)
            {
                MessageBox.Show("数据数组长度与写入寄存器个数不相等");
                return null;
            }
            string send = "01 01 00 00 00 {0} FF 10 ";
            int byteCount = 7 + regCount * 4;
            string hexbyteCount = Convert.ToString(byteCount, 16).PadLeft(2, '0');
            string hexRegCount = Convert.ToString(regCount * 2, 16).PadLeft(4, '0');
            string hexDataCount = Convert.ToString(regCount * 4, 16).PadLeft(2, '0');
            send = String.Format(send, hexbyteCount);
            string hexStartRegAddr = Convert.ToString(startAddr, 16).PadLeft(4, '0');
            send += hexStartRegAddr.Substring(0, 2) + " " + hexStartRegAddr.Substring(2, 2) + " " + hexRegCount.ToString().Substring(0, 2) + " " + hexRegCount.ToString().Substring(2, 2) + " " + hexDataCount;
            string hexData = "";
            for (int i = 0; i < dataArray.Length; i++)
            {
                string sdata = Convert.ToString(dataArray[i], 16).PadLeft(8, '0');
                hexData += " " + sdata.Substring(4, 2) + " " + sdata.Substring(6, 2) + " " + sdata.Substring(0, 2) + " " + sdata.Substring(2, 2);
            }
            send += hexData;
            byte[] sendMessage = ConvertBytes(send);
            return sendMessage;
        }

        public void WriteMultiDataRegInt32Cmd(int startAddr, int[] dataArray, int regCount, PLCWordRegStartAddr regType = PLCWordRegStartAddr.D0D8511)
        {
            
            if (dataArray.Length != regCount)
            {
                MessageBox.Show("数据数组长度与写入寄存器个数不相等");
                return;
            }
            string send = "01 01 00 00 00 {0} FF 10 ";
            int byteCount = 7 + regCount * 4;
            string hexbyteCount = Convert.ToString(byteCount, 16).PadLeft(2, '0');
            string hexRegCount = Convert.ToString(regCount * 2, 16).PadLeft(4, '0');
            string hexDataCount = Convert.ToString(regCount * 4, 16).PadLeft(2, '0');
            send = String.Format(send, hexbyteCount);
            string hexStartRegAddr = Convert.ToString(startAddr+(int)regType, 16).PadLeft(4, '0');
            send += hexStartRegAddr.Substring(0, 2) + " " + hexStartRegAddr.Substring(2, 2) + " " + hexRegCount.ToString().Substring(0, 2) + " " + hexRegCount.ToString().Substring(2, 2) + " " + hexDataCount;
            string hexData = "";
            for (int i = 0; i < dataArray.Length; i++)
            {
                string sdata = Convert.ToString(dataArray[i], 16).PadLeft(8, '0');
                hexData += " " + sdata.Substring(4, 2) + " " + sdata.Substring(6, 2) + " " + sdata.Substring(0, 2) + " " + sdata.Substring(2, 2);
            }
            send += hexData;
            byte[] sendMessage = ConvertBytes(send);
            socketClient.Send(sendMessage);
            byte[] buffer = new byte[128];
            int r = socketClient.Receive(buffer);
        }

        #endregion

        #region 【2】Modbus 指令03读取寄存器（D,SD,R,T,C）

        /// <summary>
        /// 生成读取单个16位寄存器指令
        /// </summary>
        /// <param name="regAddr"></param>
        /// <returns></returns>
        public byte[] SnedReadDataRegCmd(int regAddr, int regCount = 1)
        {
            string send = "01 01 00 00 00 06 FF 03 ";
            string hexRegAddr = Convert.ToString(regAddr, 16).PadLeft(4, '0');
            string hexRegCount = Convert.ToString(regCount, 16).PadLeft(4, '0');
            send += hexRegAddr.Substring(0, 2) + " " + hexRegAddr.Substring(2, 2) + " " + hexRegCount.Substring(0, 2) + " " + hexRegCount.Substring(2, 2);
            byte[] sendMessage = ConvertBytes(send);
            return sendMessage;
        }

        /// <summary>
        /// 读取单个16位数据寄存器
        /// </summary>
        /// <param name="regAddr"></param>
        /// <param name="socketClient"></param>
        /// <returns></returns>
        public Int16 ReadSingleDataRegInt16Cmd(int regAddr,PLCWordRegStartAddr regType= PLCWordRegStartAddr.D0D8511)
        {
            byte[] buffer = new byte[16];
            Int16 regValue = 0;
            socketClient.Send(SnedReadDataRegCmd(regAddr+(int)regType));           
            int r = socketClient.Receive(buffer);
            if (r == 11)
            {
                string hi = Convert.ToString((int)buffer[9], 16).PadLeft(2, '0');
                string lo = Convert.ToString((int)buffer[10], 16).PadLeft(2, '0');
                string hex = hi + lo;
                regValue = Convert.ToInt16(hex, 16);
            }  
            return regValue;
        }

        /// <summary>
        /// 读取单个32位数据寄存器
        /// </summary>
        /// <param name="regAddr"></param>
        /// <param name="socketClient"></param>
        /// <returns></returns>
        //public int ReadSingleDataRegInt32Cmd(int regAddr, Socket socketClient, PLCWordRegStartAddr regType = PLCWordRegStartAddr.D0D8511)
        //{
        //    byte[] buffer = new byte[16];
        //    int regValue = 0;
        //    socketClient.Send(SnedReadDataRegCmd(regAddr+(int)regType, 2));
        //    int r = socketClient.Receive(buffer);
        //    if (r > 11)
        //    {
        //        string hiLow = Convert.ToString((int)buffer[9], 16).PadLeft(2, '0');
        //        string loLow = Convert.ToString((int)buffer[10], 16).PadLeft(2, '0');
        //        string hiHigh= Convert.ToString((int)buffer[11], 16).PadLeft(2, '0');
        //        string loHigh = Convert.ToString((int)buffer[12], 16).PadLeft(2, '0');
        //        string hex = hiHigh+loHigh+hiLow + loLow;
        //        regValue = Convert.ToInt32(hex, 16);
        //    }
        //    return regValue;
        //}

        public int ReadSingleDataRegInt32(int regAddr,  PLCWordRegStartAddr regType = PLCWordRegStartAddr.D0D8511)
        {
            byte[] buffer = new byte[16];
            int regValue = 0;
            socketClient.Send(SnedReadDataRegCmd(regAddr + (int)regType, 2));
            int r = socketClient.Receive(buffer);
            if (r > 11)
            {
                string hiLow = Convert.ToString((int)buffer[9], 16).PadLeft(2, '0');
                string loLow = Convert.ToString((int)buffer[10], 16).PadLeft(2, '0');
                string hiHigh = Convert.ToString((int)buffer[11], 16).PadLeft(2, '0');
                string loHigh = Convert.ToString((int)buffer[12], 16).PadLeft(2, '0');
                string hex = hiHigh + loHigh + hiLow + loLow;
                regValue = Convert.ToInt32(hex, 16);
            }
            return regValue;
        }

        /// <summary>
        /// 读取多个16位数据寄存器
        /// </summary>
        /// <param name="regAddr"></param>
        /// <param name="regCount"></param>
        /// <param name="socketClient"></param>
        /// <returns></returns>
        public Int16[] ReadMultiDataRegInt16(int regAddr,int regCount,  PLCWordRegStartAddr regType = PLCWordRegStartAddr.D0D8511)
        {
            byte[] buffer = new byte[512];
            Int16[] regValue = null;            
            socketClient.Send(SnedReadDataRegCmd(regAddr+ (int)regType, regCount));
            int r = socketClient.Receive(buffer);
            if (r > 10)
            {
                regValue = AnalysisGetMultiDataRegInt16Buffer(buffer);
            }

            return regValue;
        }

        /// <summary>
        /// 从socket返回值中解析多个16位寄存器的值
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public Int16[] AnalysisGetMultiDataRegInt16Buffer(byte[] buffer)
        {
            int returnCount = (int)buffer[8];
            Int16[]  regValue = new Int16[returnCount / 2];
            for (int i = 0; i < returnCount / 2; i++)
            {
                string hi = Convert.ToString((int)buffer[9 + i * 2], 16).PadLeft(2, '0');
                string lo = Convert.ToString((int)buffer[10 + i * 2], 16).PadLeft(2, '0');
                string hex = hi + lo;
                regValue[i] = Convert.ToInt16(hex, 16);
            }
            return regValue;
        }

        /// <summary>
        /// 读取多个32位数据寄存器
        /// </summary>
        /// <param name="regAddr"></param>
        /// <param name="regCount"></param>
        /// <param name="socketClient"></param>
        /// <returns></returns>
        public Double[] ReadMultiDataRegInt32(int regAddr, int regCount,  PLCWordRegStartAddr regType = PLCWordRegStartAddr.D0D8511)
        {
            byte[] buffer = new byte[512];
            Double[] regValue = null;
            socketClient.Send(SnedReadDataRegCmd(regAddr+(int)regType, regCount*2));
            int r = socketClient.Receive(buffer);
            if (r > 10)
            {
                regValue = AnalysisGetMultiDataRegInt32Buffer(buffer);
            }

            return regValue;
        }


     
        /// <summary>
        /// 从socket返回值中解析多个32位寄存器的值
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public double[] AnalysisGetMultiDataRegInt32Buffer(byte[] buffer)
        {
            int returnCount = (int)buffer[8];
            Double[] regValue = new double[returnCount / 4];
            for (int i = 0; i < returnCount / 4; i++)
            {
                string hiLow = Convert.ToString((int)buffer[9 + i * 4], 16).PadLeft(2, '0');
                string loLow = Convert.ToString((int)buffer[10 + i * 4], 16).PadLeft(2, '0');
                string hiHigh = Convert.ToString((int)buffer[11 + i * 4], 16).PadLeft(2, '0');
                string loHigh = Convert.ToString((int)buffer[12 + i * 4], 16).PadLeft(2, '0');
                string hex = hiHigh + loHigh + hiLow + loLow;
                regValue[i] = Convert.ToInt32(hex, 16);
            }
            return regValue;
        }

        #endregion

        #region【3】Modbus 指令01写入线圈（M,SM,S,T,C,X,Y）

        /// <summary>
        /// 解析发送写入线圈ModbusTCP协议指令
        /// </summary>
        /// <param name="regAddr">地址</param>
        /// <param name="onoffFlag"></param>
        /// <returns></returns>
        public byte[] SnedWriteSingleMRegCmd(int regAddr, bool onoffFlag)
        {
            string send = "01 01 00 00 00 06 FF 05 ";
            string hexRegAddr = Convert.ToString(regAddr, 16).PadLeft(4, '0');
            if(onoffFlag)
                send += hexRegAddr.Substring(0, 2) + " " + hexRegAddr.Substring(2, 2) + " " + "00" + " " + "01";
            else
                send += hexRegAddr.Substring(0, 2) + " " + hexRegAddr.Substring(2, 2) + " " + "00" + " " + "00";
            byte[] sendMessage = ConvertBytes(send);
            return sendMessage;
        }

        /// <summary>
        /// 写入线圈值
        /// </summary>
        /// <param name="regAddr"></param>
        /// <param name="onoffFlag"></param>
        /// <param name="socketClient"></param>
        /// <param name="regType"></param>
        public void WriteSingleMReg(int regAddr, bool onoffFlag, PLCBitRegStartAddr regType = PLCBitRegStartAddr.M0M7679)
        {
            
            socketClient.Send(SnedWriteSingleMRegCmd(regAddr + (int)regType, onoffFlag));
            byte[] buffer = new byte[128];
            int r = socketClient.Receive(buffer);
        }
        

        #endregion

        #region【4】Modbus 指令01读取线圈（M,SM,S,T,C,X,Y）
                
        /// <summary>
        /// 解析发送读取线圈ModbusTCP协议指令
        /// </summary>
        /// <param name="regAddr">起始地址</param>
        /// <param name="regCount">读取个数</param>
        /// <returns></returns>
        public byte[] SnedReadCoilRegCmd(int regAddr,int regCount=1)
        {
            string send = "01 01 00 00 00 06 FF 01 ";
            string hexRegAddr = Convert.ToString(regAddr, 16).PadLeft(4, '0');
            string hexRegCount = Convert.ToString(regCount, 16).PadLeft(4, '0');
            send += hexRegAddr.Substring(0, 2) + " " + hexRegAddr.Substring(2, 2) + " " + hexRegCount.Substring(0, 2) + " " + hexRegCount.Substring(2, 2);
            byte[] sendMessage = ConvertBytes(send);
            return sendMessage;
        }

        /// <summary>
        /// 解析接收读取单个线圈ModbusTCP协议指令
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool AnalysisGetCoilRegBuffer(byte[] buffer)
        {
            bool state = false ;
            if (buffer == null || buffer.Length == 0)
                state = false;
            else
            {
                if (buffer.Length == 10)
                {
                    if (buffer[9] == 1)
                        state = true;
                    else
                        state = false;

                }
                else
                    state = false;
            }
            return state;
        }

        /// <summary>
        /// 解析接收读取多个线圈ModbusTCP协议指令
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public bool[] AnalysisGetMultiCoilRegBuffer(byte[] buffer)
        {
            bool[] state = null;
            if (buffer == null || buffer.Length == 0)
            { }
            else
            {
                if (buffer.Length >= 10)
                {
                    int count = Convert.ToInt32(buffer[8]);
                    if (count >= 1)
                    {                       
                        state = new bool[count * 8];
                        for (int i = 0; i < count; i++)
                        {                          
                            byte[] byteState = System.Text.Encoding.ASCII.GetBytes(Convert.ToString(buffer[9+i], 2).PadLeft(8, '0'));
                            Array.Reverse(byteState);
                            for (int j = 0; j < byteState.Length; j++)
                            {
                                if (byteState[j] == 49)
                                    state[i*8 + j] = true;
                                else
                                    state[i*8+ j] = false;
                            }
                        }                        
                    }
                }
                
            }
            return state;
        }
       
        /// <summary>
        /// 获取单个线圈状态 
        /// </summary>
        /// <param name="regAddr">开始地址</param>
        /// <param name="socketClient">socket</param>
        /// <param name="regType">线圈类型对应的偏移地址</param>
        /// <returns></returns>
        public bool ReadSingleCoilRegistor(int regAddr,  PLCBitRegStartAddr regType=PLCBitRegStartAddr.M0M7679)
        {
            byte[] buffer = new byte[10];
            socketClient.Send(SnedReadCoilRegCmd(regAddr + (int)regType));
            int r = socketClient.Receive(buffer);
            return AnalysisGetCoilRegBuffer(buffer);
        }

        /// <summary>
        /// 获取多个线圈状态
        /// </summary>
        /// <param name="regAddr">开始地址</param>
        /// <param name="socketClient"></param>
        /// <param name="regCount">读取数量</param>
        /// <param name="regType">线圈类型对应的偏移地址</param>
        /// <returns></returns>
        public bool[] ReadMultiCoilRegistor(int regAddr, int regCount,  PLCBitRegStartAddr regType)
        {
            byte[] buffer = new byte[30];
            socketClient.Send(SnedReadCoilRegCmd(regAddr + (int)regType, regCount));
            int r = socketClient.Receive(buffer);
            return AnalysisGetMultiCoilRegBuffer(buffer);
        }

        #endregion

        #region【5】浮点数操作

        public byte[] SnedReadFloat(int regAddr, int regCount = 2)
        {
            string send = "01 01 00 00 00 06 FF 03 ";
            string hexRegAddr = Convert.ToString(regAddr, 16).PadLeft(4, '0');
            string hexRegCount = Convert.ToString(regCount, 16).PadLeft(4, '0');
            send += hexRegAddr.Substring(0, 2) + " " + hexRegAddr.Substring(2, 2) + " " + hexRegCount.Substring(0, 2) + " " + hexRegCount.Substring(2, 2);
            byte[] sendMessage = ConvertBytes(send);
            return sendMessage;
        }

        public float[] AnalysisGetMultiDataRegFloatBuffer(byte[] buffer)
        {
            int returnCount = (int)buffer[8];
            float[] regValue = new float[returnCount / 4];
            for (int i = 0; i < returnCount / 4; i++)
            {
                string hiLow = Convert.ToString((int)buffer[9 + i * 4], 16).PadLeft(2, '0');
                string loLow = Convert.ToString((int)buffer[10 + i * 4], 16).PadLeft(2, '0');
                string hiHigh = Convert.ToString((int)buffer[11 + i * 4], 16).PadLeft(2, '0');
                string loHigh = Convert.ToString((int)buffer[12 + i * 4], 16).PadLeft(2, '0');
                string hex = hiHigh + loHigh + hiLow + loLow;
                UInt32 x = Convert.ToUInt32(hex, 16);//字符串转16进制32位无符号整数


                regValue[i] = BitConverter.ToSingle(BitConverter.GetBytes(x), 0);//IEEE754 字节转换float
            }
            return regValue;
        }

        public float[] ReadMultiDataRegFloat(int regAddr, int regCount,   PLCWordRegStartAddr regType = PLCWordRegStartAddr.D0D8511)
        {
            byte[] buffer = new byte[512];
            float[] regValue = null;
            socketClient.Send(SnedReadFloat(regAddr+(int)regType, regCount * 2));
            int r = socketClient.Receive(buffer);
            if (r > 10)
            {
                regValue = AnalysisGetMultiDataRegFloatBuffer(buffer);
            }

            return regValue;
        }

        public byte[] SnedWriteFloat(int startAddr, float[] dataArray, int regCount = 2)
        {
            if (dataArray.Length != regCount)
            {
                MessageBox.Show("数据数组长度与写入寄存器个数不相等");
                return null;
            }
            string send = "01 01 00 00 00 {0} FF 10 ";
            int byteCount = 7 + regCount * 4;
            string hexbyteCount = Convert.ToString(byteCount, 16).PadLeft(2, '0');
            string hexRegCount = Convert.ToString(regCount * 2, 16).PadLeft(4, '0');
            string hexDataCount = Convert.ToString(regCount * 4, 16).PadLeft(2, '0');
            send = String.Format(send, hexbyteCount);
            string hexStartRegAddr = Convert.ToString(startAddr, 16).PadLeft(4, '0');
            send += hexStartRegAddr.Substring(0, 2) + " " + hexStartRegAddr.Substring(2, 2) + " " + hexRegCount.ToString().Substring(0, 2) + " " + hexRegCount.ToString().Substring(2, 2) + " " + hexDataCount;
            string hexData = "";
            for (int i = 0; i < dataArray.Length; i++)
            {
                var b = BitConverter.GetBytes(dataArray[i]);
                string sdata = BitConverter.ToString(b.Reverse().ToArray()).Replace("-", "").PadLeft(8, '0');
                hexData += " " + sdata.Substring(4, 2) + " " + sdata.Substring(6, 2) + " " + sdata.Substring(0, 2) + " " + sdata.Substring(2, 2);
            }
            send += hexData;
            byte[] sendMessage = ConvertBytes(send);
            return sendMessage;
        }

        public void WriteMultiDataRegFloatCmd(int startAddr, float[] dataArray, int regCount, PLCWordRegStartAddr regType= PLCWordRegStartAddr.D0D8511)
        {
            if (dataArray.Length != regCount)
            {
                MessageBox.Show("数据数组长度与写入寄存器个数不相等");
                return;
            }
            string send = "01 01 00 00 00 {0} FF 10 ";
            int byteCount = 7 + regCount * 4;
            string hexbyteCount = Convert.ToString(byteCount, 16).PadLeft(2, '0');
            string hexRegCount = Convert.ToString(regCount * 2, 16).PadLeft(4, '0');
            string hexDataCount = Convert.ToString(regCount * 4, 16).PadLeft(2, '0');
            send = String.Format(send, hexbyteCount);
            string hexStartRegAddr = Convert.ToString(startAddr + (int)regType, 16).PadLeft(4, '0');
            send += hexStartRegAddr.Substring(0, 2) + " " + hexStartRegAddr.Substring(2, 2) + " " + hexRegCount.ToString().Substring(0, 2) + " " + hexRegCount.ToString().Substring(2, 2) + " " + hexDataCount;
            string hexData = "";
            for (int i = 0; i < dataArray.Length; i++)
            {
                var b = BitConverter.GetBytes(dataArray[i]);
                string sdata = BitConverter.ToString(b.Reverse().ToArray()).Replace("-", "").PadLeft(8, '0');                
                hexData += " " + sdata.Substring(4, 2) + " " + sdata.Substring(6, 2) + " " + sdata.Substring(0, 2) + " " + sdata.Substring(2, 2);
            }
            send += hexData;
            byte[] sendMessage = ConvertBytes(send);
            socketClient.Send(sendMessage);
            byte[] buffer = new byte[128];
            int r = socketClient.Receive(buffer);
        }

        #endregion

       
    }

    #region 数据类型定义类型

    public enum PLCIOSta//IO端口状态
    {
        ON = 1,
        OFF = 0,
    }

    /// <summary>
    /// 线圈 、位元件、变量 位元件地址 定义
    /// </summary>
    public enum PLCBitRegStartAddr
    {
        XReg = 63488,
        YReg = 64512,
        M0M7679 = 0,
        M8000M8511 = 8000, //0x1F400-0x213F (8000-8511)
        SM0SM1023 = 9216,
        S0S4095 = 57344,
        T0T511 = 61440,
        C0C255 = 62464,
    }

    /// <summary>
    /// 寄存器 、字元件、变量 字元件地址 定义
    /// </summary>
    public enum PLCWordRegStartAddr
    {
        D0D8511 = 0,
        SD0SD1023 = 9216,
        R = 12288,       
        T0T511 = 61440,
        C0C199 = 62464,
        C200C255 = 63232,
    }

   

    #endregion
}
