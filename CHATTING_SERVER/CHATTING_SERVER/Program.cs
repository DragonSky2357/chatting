using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CHATTING_SERVER {
    class Program {
        public static TcpListener server = null;
        public static TcpClient clientSocket = null;
        public static Dictionary<TcpClient, string> clientList = new Dictionary<TcpClient, string>();

        static void Main(string[] args) {
            Thread serverThread = new Thread(Server.InitSocket);
            serverThread.IsBackground = true;
            serverThread.Start();
            


        }
    }
}
