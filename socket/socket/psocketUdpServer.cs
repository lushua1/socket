using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
namespace socket
{
    class psocketUdpServer
    {
        Socket server = null;
        Thread threadudpServer = null;
        /// <summary>
        /// udpServer 启动服务器
        /// </summary>
        /// <param name="adress"></param>
        /// <param name="port"></param>
        public void startUp(string adress, string port)  
        {
            try
            {
                server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                server.Bind(new IPEndPoint(IPAddress.Parse(adress), int.Parse(port)));
                threadudpServer = new Thread(recMsg); 
                threadudpServer.IsBackground = true;
                threadudpServer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// udpServer 接收数据
        /// </summary>
        private void recMsg()
        {
            while (true)
            {
                EndPoint point = new IPEndPoint(IPAddress.Any, 0);//用来保存发送方的ip和端口号
                byte[] buffer = new byte[1024];
                int length = server.ReceiveFrom(buffer, ref point);//接收数据报
                string message = Encoding.UTF8.GetString(buffer, 0, length);
                MessageBox.Show(message);
            }
        }
        /// <summary>
        /// udpServer 发送数据
        /// </summary>
        public void sendMsg(string msg, string adress, string port) 
        {
            EndPoint point = new IPEndPoint(IPAddress.Parse(adress), int.Parse(port));
            server.SendTo(Encoding.UTF8.GetBytes(msg), point);
        }




    }
}
