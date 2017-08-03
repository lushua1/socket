using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Web.Script.Serialization; 
using WebSocket4Net;
namespace socket
{
    class psocketWebClient
    {
        WebSocket websocket;

        /// <summary>
        /// webSocketClient 启动服务
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void startUp(string ip, string port) 
        {
            try
            {
                websocket = new WebSocket("ws://" + ip + ":" + port);
                websocket.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// webSocketClient 发送函数
        /// </summary>
        /// <param name="msg"></param>
        public void sendMsg(string msg)
        {
            websocket.Send(msg);
        }



    }
}
