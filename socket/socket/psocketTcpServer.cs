using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Windows.Forms;
namespace socket
{
    class psocketTcpServer  
    {
        Thread threadWatch = null;
        Socket socketWatch = null; 
        /// <summary>
        /// 启动tcp服务器监听
        /// </summary>
        /// <param name="adress"></param>
        /// <param name="port"></param>
        public void startUp(string adress, string port) 
        {
            try
            {
                socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                int ports = Convert.ToInt32(port);
                IPAddress ip = IPAddress.Parse(adress);
                IPEndPoint ipe = new IPEndPoint(ip, ports);
                socketWatch.Bind(ipe);
                socketWatch.Listen(5);
                threadWatch = new Thread(WatchConnect);
                threadWatch.IsBackground = true;
                threadWatch.Start();
                //MessageBox.Show("服务器已经启动，开始监听！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 监听
        /// </summary>
        Socket socketConnect = null;//与客户端建立连接的套接字  
        public void WatchConnect()
        {
            while (true)
            {
                try
                {
                    socketConnect = socketWatch.Accept();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    break;
                }
                Thread thread = new Thread(recMsg);
                thread.IsBackground = true;
                thread.Start(socketConnect);
            }
        }

        /// <summary>
        /// tcp 服务器接收函数
        /// </summary>
        string recStr = null;
        private void recMsg(object obj) 
        {
            Socket socketServer = obj as Socket;
            while (true)
            {
                int firstRcv = 0;
                byte[] buffer = new byte[8 * 1024];
                try
                {
                    //获取接受数据的长度，存入内存缓冲区，返回一个字节数组的长度  
                    if (socketServer != null) firstRcv = socketServer.Receive(buffer);
                    if (firstRcv > 0)//大于0，说明有东西传过来  
                    {
                        byte [] ff = new byte [2];
                        recStr = Encoding.UTF8.GetString(buffer, 0, firstRcv);                     //接受数据处理

                       // if (recStr == "hello") MessageBox.Show(recStr);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("系统异常..." + ex.Message);
                    break;
                }
            }
        }

        /// <summary>
        /// 十进制byte转十六进制byte
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private byte[] tarrffy(byte[] buffer)
        {
            string str = null;
            byte [] buf =new  byte[buffer.Length];
            byte tempByte = 0x00;

            for (int i = 0; i < buffer.Length; i++)
            {
                str = Convert.ToString(buffer[i], 16);
                tempByte = Convert.ToByte(str);
                buf[i] = tempByte;
            }
                return buf;
        }

        /// <summary>
        ///tcpServer 发送数据 
        /// </summary>
        /// <param name="SendStr"></param>
        public void sendMsg(string SendStr)  
        {
            byte[] buffer = Encoding.UTF8.GetBytes(SendStr);
            socketConnect.Send(buffer);
        }


        private void tarry(byte[] buffer)
        {
            string str = null;
            str = buffer[0].ToString("X2");

        }



    }
}
