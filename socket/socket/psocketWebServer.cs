using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using SuperSocket.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketEngine;
using SuperSocket.SocketBase.Logging;
using SuperWebSocket;
using log4net;

namespace socket
{
    class psocketWebServer
    {
        WebSocketServer appServer = new WebSocketServer();
        WebSocketSession serion = new WebSocketSession();
        ServerConfig serverConfig = new ServerConfig 
        {
            Port = 2015,//set the listening port
            MaxConnectionNumber = 10000
        };

        /// <summary>
        /// webServer 启动服务
        /// </summary>
        public void startUp()
        {
            if (!appServer.Setup(serverConfig)) //Setup the appServer
            {
                System.Windows.Forms.MessageBox.Show("开启服务器失败");
                return;
            }

            if (!appServer.Start())//Try to start the appServer
            {
                System.Windows.Forms.MessageBox.Show("开启服务器失败");
                return;
            }
            //注册事件
            appServer.NewSessionConnected += appServer_NewSessionConnected;//客户端连接
            appServer.NewMessageReceived += recMsg;//客户端接收消息 
            appServer.SessionClosed += appServer_SessionClosed;//客户端关闭
        }
        /// <summary>
        /// webServer 连接状态
        /// </summary>
        /// <param name="session"></param>
        void appServer_NewSessionConnected(WebSocketSession session)
        {
            session.Send("连接成功");
        }

        /// <summary>
        /// webServer 接收数据
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
        void recMsg(WebSocketSession session, string value)
        {
            session.Send("服务端收到了客户端发来的消息");
            MessageBox.Show(value.ToString());
        }

        /// <summary>
        /// webServer 关闭服务
        /// </summary>
        /// <param name="session"></param>
        /// <param name="value"></param>
         void appServer_SessionClosed(WebSocketSession session, SuperSocket.SocketBase.CloseReason value)
        {
            session.Close();
        }

    }
}
