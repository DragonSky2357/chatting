﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CHATTING_SERVER {
    class HandleClient {
        TcpClient clientSocket = null;
        public Dictionary<TcpClient, string> clientList = null;

        public void startClient(TcpClient clientSocket, Dictionary<TcpClient, string> clientList) {
            this.clientSocket = clientSocket;
            this.clientList = clientList;

            Thread t_handler = new Thread(doChat);
            t_handler.IsBackground = true;
            t_handler.Start();
        }

        public delegate void MessageDisplayHandler(TcpClient clientSocket, string message, string userName);
        public event MessageDisplayHandler OnReceived;

        public delegate void DisconnectedHandler(TcpClient clientSocket);
        public event DisconnectedHandler OnDisconnected;

        private void doChat() {
            NetworkStream stream = null;
            try {
                byte[] buffer = new byte[1024];
                string msg = string.Empty;
                int bytes = 0;

                while (true) {
                    stream = clientSocket.GetStream();
                    bytes = stream.Read(buffer, 0, buffer.Length);
                    msg = Encoding.Unicode.GetString(buffer, 0, bytes);
                    msg = msg.Substring(0, msg.IndexOf("$"));

                    if (OnReceived != null)
                        OnReceived(clientSocket, msg, clientList[clientSocket].ToString());
                }
            } catch (SocketException se) {
                Trace.WriteLine(string.Format("doChat - SocketException : {0}", se.Message));

                if (clientSocket != null) {
                    if (OnDisconnected != null)
                        OnDisconnected(clientSocket);

                    if (clientSocket != null)
                        clientSocket.Close();

                    if (stream != null)
                        stream.Close();
                }
            } catch (Exception ex) {
                Trace.WriteLine(string.Format("doChat - Exception : {0}", ex.Message));

                if (clientSocket != null) {
                    if (OnDisconnected != null)
                        OnDisconnected(clientSocket);

                    if(clientSocket!=null)
                        clientSocket.Close();

                    if(stream!=null)
                        stream.Close();
                }
            }
        }
    }
}
