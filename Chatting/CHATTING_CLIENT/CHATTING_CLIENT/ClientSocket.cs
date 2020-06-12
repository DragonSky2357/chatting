using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CHATTING_CLIENT {
    public class ClientSocket {
        public TcpClient clientSocket = new TcpClient();
        public NetworkStream stream = default(NetworkStream);

        public ClientSocket() {
           // local host IP
           this.clientSocket.Connect(IP.IPAddress, IP.Port);
           this.stream = clientSocket.GetStream();

            // different host IP
            // this.clientSocket.Connect(IP, Port);
            // this.stream = clientSocket.GetStream();
        }

        public void ReConnect() {
            this.clientSocket.Connect(IP.IPAddress, IP.Port);
            this.stream = clientSocket.GetStream();
        }
    }
}
