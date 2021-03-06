﻿using System.Threading;

namespace CHATTING_SERVER {
    class Program {
        static void Main(string[] args) {
            // 서버 시작지점 및 Server쓰레드 생성
            
            Thread serverThread = new Thread(Server.InitSocket);
            serverThread.IsBackground = true;
            serverThread.Start();

            Thread serverThread2 = new Thread(Server.Login);
            serverThread2.IsBackground = true;
            serverThread2.Start();

            serverThread.Join();
            serverThread2.Join();
        }
    }
}
