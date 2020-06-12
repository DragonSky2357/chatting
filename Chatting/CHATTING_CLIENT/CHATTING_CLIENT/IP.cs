using System.Net;
using System.Net.Sockets;

namespace CHATTING_CLIENT {
    class IP {
        // LocalHost IPAddress,Port 설정
        public static string IPAddress {
            get {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                foreach (var item in host.AddressList)
                    if (item.AddressFamily == AddressFamily.InterNetwork) return item.ToString();
                return "127.0.0.1";
            }
        }
        public static int Port {
            get { return 13000; }
        }
        //다른 PC에 연결하려면 아래코드 실행
        //public static string IPAddress = "IPAddress";
        //public static int PORT = 13000;
    }
}
