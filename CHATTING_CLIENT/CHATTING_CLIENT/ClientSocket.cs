using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CHATTING_CLIENT {
    public class ClientSocket {
        public TcpClient clientSocket = new TcpClient();
        public NetworkStream stream = default(NetworkStream);

        public ClientSocket(int port=13000) {
            // local host IP
            try {
                this.clientSocket.Connect(IP.IPAddress, port);
                this.stream = clientSocket.GetStream();
            }
            catch(Exception e) {
                MessageBox.Show(e.Message);
            }

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
