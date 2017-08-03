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
    class psocketTcpClient
    {

        Socket socketClient = null;
        Thread threadClient = null;
        /// <summary>
        /// 连接服务
        /// </summary>
        /// <param name="adress"></param>
        /// <param name="port"></param>
        public void startUp(string adress, string port) 
        {
            try
            {
                socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                int ports = Convert.ToInt32(port);
                IPAddress ip = IPAddress.Parse(adress);
                IPEndPoint ipe = new IPEndPoint(ip, ports);
                socketClient.Connect(ipe);
                threadClient = new Thread(recMsg);
                threadClient.IsBackground = true;
                threadClient.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 接收函数
        /// </summary>
        private void recMsg() 
        {
            string recStr = null;
        
            while (true)
            {
                int firstRcv = 0;
                byte[] buffer = new byte[8 * 1024];
                  
                try
                {
                    if (socketClient != null) firstRcv = socketClient.Receive(buffer);
                    if (firstRcv > 0)
                    { 
                          byte [] buffers = new byte [buffer.Length]; 
                            recStr = Encoding.UTF8.GetString(buffer, 0, firstRcv);
                            buffers = tarrffy(buffer);
                            MessageBox.Show(buffers[0].ToString ());
                        }                    
                    }              
                    catch (Exception ex)
                    {
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
            byte[] buf = new byte[buffer.Length];
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
        ///tcpClient 发送数据 
        /// </summary>
        /// <param name="SendStr"></param>
        public void sendMsg(string SendStr) 
          {
                byte[] buffer = Encoding.UTF8.GetBytes(SendStr);
                socketClient.Send(buffer);             
         }




    }
}
