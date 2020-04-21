using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CHATTING_SERVER {
    class Program {
        const int PORT = 13000;
        private static TcpListener server = null;
        private static TcpClient clientSocket = null;
        static int count = 0;
        public static Dictionary<TcpClient, string> clientList = new Dictionary<TcpClient, string>();

       static void Main(string[] args) {
            Thread t = new Thread(InitSocket);
            t.IsBackground = true;
            t.Start();
            t.Join();
        }

        private static void InitSocket() {
            server = new TcpListener(IPAddress.Any, PORT);
            clientSocket = default(TcpClient);
            server.Start();

            Console.WriteLine("Chatting Server Start : {0}", DateTime.Now);
            while (true) {
                try {
                    count++;
                    clientSocket = server.AcceptTcpClient();
                    Console.WriteLine("Accept connection from client");

                    NetworkStream stream = clientSocket.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string userName = Encoding.UTF8.GetString(buffer, 0, bytes);
                    userName = userName.Substring(0, userName.IndexOf("$"));

                    clientList.Add(clientSocket, userName);

                    SendMessageAll(userName + "Joined ", "", false);

                    HandleClient h_client = new HandleClient();
                    h_client.OnReceived += new HandleClient.MessageDisplayHandler(OnReceived);
                    h_client.OnDisconnected += new HandleClient.DisconnectedHandler(h_client_OnDisconnected);
                    h_client.StartClinet(clientSocket, clientList);

                } catch (SocketException se) {
                    Trace.WriteLine(string.Format("InitSocket - SocketException : {0}", se.Message));
                    break;
                } catch (Exception ex) {
                    Trace.WriteLine(string.Format("InitSocket - Exception : {0}", ex.Message));
                    break;
                }
                clientSocket.Close();
                server.Stop();
            }
        }

        static void h_client_OnDisconnected(TcpClient clientSocket) {
            if (clientList.ContainsKey(clientSocket))
                clientList.Remove(clientSocket);
        }

        private static void OnReceived(string message, string user_name) {
            string displayMessage = "From client : " + user_name + " : " + message;
            SendMessageAll(message, user_name, true);
        }

        private static void SendMessageAll(string message, string userName, bool flag) {
            foreach (var item in clientList) {
                Trace.WriteLine(string.Format("Tcpclient : {0} UserName : {1}", item.Key, item.Value));

                TcpClient client = item.Key as TcpClient;
                NetworkStream stream = client.GetStream();
                byte[] buffer = null;

                if (flag)
                    buffer = Encoding.Unicode.GetBytes(userName + " says : " + message);
                else
                    buffer = Encoding.Unicode.GetBytes(message);

                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
        }
    }
}
