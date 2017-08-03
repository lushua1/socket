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
    class psocketUdpClient
    {
        private static Socket sock;
        private static IPEndPoint iep1;
        private static byte[] data;

        /// <summary>
        /// udpServerstartup 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="port"></param>
        public void startUp(string msg, string port) 
        {
            try
            {
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                iep1 = new IPEndPoint(IPAddress.Broadcast, int.Parse(port));

                data = Encoding.ASCII.GetBytes(msg);

                sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                Thread threadudpclient = new Thread(broadMsg); 
                threadudpclient.IsBackground = true;
                threadudpclient.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// udpClient 接收数据
        /// </summary>
        private static void broadMsg()  
        {
                sock.SendTo(data, iep1);      
        }
    }
}
