using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CHATTING_SERVER {
    class Server {
        public static TcpListener server = null;
        public static TcpClient clientSocket = null;
        public static Dictionary<TcpClient, string> clientList = new Dictionary<TcpClient, string>();

        public static void InitSocket() {

            Server.server = new TcpListener(IPAddress.Any, 13000);
            Server.clientSocket = default(TcpClient);
            Server.server.Start();
            Console.WriteLine(">> Server Started");

            while (true) {
                try {
                    Server.clientSocket = Server.server.AcceptTcpClient();
                    Console.WriteLine(">> Accept conncetion from client");

                    NetworkStream stream = Server.clientSocket.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string userName = Encoding.Unicode.GetString(buffer, 0, buffer.Length);
                    userName = userName.Substring(0, userName.IndexOf("$"));

                    Server.clientList.Add(Server.clientSocket, userName);

                    SendMessageAll(Server.clientSocket, userName + "입장하셨습니다.", "", false);
                    SaveLog(userName, userName + "입장하셨습니다.");

                    HandleClient h_client = new HandleClient();
                    h_client.OnReceived += new HandleClient.MessageDisplayHandler(OnReceived);
                    h_client.OnDisconnected += new HandleClient.DisconnectedHandler(h_client_OnDisconnected);
                    h_client.startClient(Server.clientSocket, Server.clientList);

                } catch (SocketException se) {
                    Trace.WriteLine(string.Format("InitSocket - SocketException : {0}", se.Message));
                    break;
                } catch (Exception ex) {
                    Trace.WriteLine(string.Format("InitSocket - Exception : {0}", ex.Message));
                    break;
                }
            }
            Server.clientSocket.Close();
            Server.server.Stop();
        }

        private static void SaveLog(string userName, string message) {
            using (StreamWriter writer = new StreamWriter($"{DateTime.Today.ToString("yyyyMMdd")}"+"log.txt", true)) {
                writer.WriteLine("DateTime : {0} User : {1} Message : {2}", DateTime.Now.ToString(), userName, message);
            }
        }
        private static void h_client_OnDisconnected(TcpClient clientSocket) {
            if (clientList.ContainsKey(clientSocket))
                Server.clientList.Remove(clientSocket);
        }

        private static void OnReceived(TcpClient clientSocket, string message, string userName) {
            string displayMessage = "From client : " + userName + " : " + message;
            Console.WriteLine(displayMessage);
            SendMessageAll(clientSocket, message, userName, true);
            SaveLog(userName, message);
        }

        private static void SendMessageAll(TcpClient clientSocket, string message, string userName, bool flag) {
            foreach (var item in Server.clientList) {
                if (item.Key != clientSocket) {
                    Trace.WriteLine(string.Format("tcpclient : {0} userName : {1}", item.Key, item.Value));

                    TcpClient client = item.Key as TcpClient;
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = null;

                    if (flag)
                        buffer = Encoding.Unicode.GetBytes(userName + '\n' + message);
                    else
                        buffer = Encoding.Unicode.GetBytes(message);

                    stream.Write(buffer, 0, buffer.Length);
                    stream.Flush();
                }
            }
        }
    }
}
