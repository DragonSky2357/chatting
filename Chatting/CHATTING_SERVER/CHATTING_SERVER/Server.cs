using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace CHATTING_SERVER {
    class Server {
        public static TcpListener server = new TcpListener(IPAddress.Any, 13000);
        public static TcpClient clientSocket = default(TcpClient);
        public static Dictionary<TcpClient, string> clientList = new Dictionary<TcpClient, string>();

        public static void Login() {
            TcpListener loginServer = new TcpListener(IPAddress.Any, 14000);
            TcpClient clientSocket = default(TcpClient);

            loginServer.Start();

            while (true) {
                clientSocket = loginServer.AcceptTcpClient();
                Console.WriteLine("@");
            }
            
        }
        public static void InitSocket() {
            Server.server.Start();
            Console.WriteLine(">> Server Started");

            while (true) {
                try {
                    Server.clientSocket = Server.server.AcceptTcpClient();
                    Console.WriteLine(">> Accept conncetion from client");

                    NetworkStream stream = Server.clientSocket.GetStream();
                    byte[] buffer = new byte[8092];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string command = Encoding.Unicode.GetString(buffer, 0, buffer.Length);
                    command = command.Substring(0, command.IndexOf("\0"));

                    if (command.Equals(Command.LoginButtonClick)) {
                        Trace.WriteLine(Command.LoginButtonClick.ToString());

                        Array.Clear(buffer, 0, buffer.Length);

                        UserLogin userLogin = new UserLogin();
                        buffer = new byte[40];

                        while (stream.Read(buffer, 0, buffer.Length) !=0) {
                            if (Encoding.Unicode.GetString(buffer, 0, buffer.Length).Contains("<EndMessage>")) break;
                            // deserializing;
                            userLogin = GetBindAck(buffer);
                        }
                        Trace.WriteLine("{0}", userLogin.UserID);
                        Trace.WriteLine("{0}", userLogin.UserPassword);

                        if ((userLogin.UserID != string.Empty) && (userLogin.UserPassword != string.Empty)) {
                            string strConn = String.Format("Data Source={0}", AppDomain.CurrentDomain.BaseDirectory + @"DB\ChattingDB.db");

                            using (SQLiteConnection conn = new SQLiteConnection(strConn)) {
                                conn.Open();

                                //string sql = "INSERT INTO User(USERID,USERPASSWORD) VALUES('test','1234')";
                                //SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                                //cmd.ExecuteNonQuery();
                                string userID = userLogin.UserID;
                                string userPassword = userLogin.UserID;

                                string sql = string.Format("SELECT * FROM User WHERE UserID='{0}'", userID);

                                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                                SQLiteDataReader rdr = cmd.ExecuteReader();

                                byte[] loginCheck = null;
                                if (rdr.HasRows) {
                                    while (rdr.Read()) {
                                        var id = rdr["UserID"];
                                        var password = rdr["UserPassword"];
                                        var login = rdr["Login"];
                                        if (login.ToString() == "1") {
                                            loginCheck = Encoding.Unicode.GetBytes("ALREADY");
                                            stream.Write(loginCheck, 0, loginCheck.Length);
                                            stream.Close();
                                            break;

                                        } else {
                                            try {
                                                sql = string.Format("UPDATE User SET Login='1' WHERE UserID='{0}'", userID.ToString());
                                                cmd = new SQLiteCommand(sql, conn);
                                                cmd.ExecuteNonQuery();
                                                loginCheck = Encoding.Unicode.GetBytes("TRUE");

                                                stream.Write(loginCheck, 0, loginCheck.Length);
                                                stream.Close();

                                                HandleClient h_client = new HandleClient();
                                                h_client.OnReceived += new HandleClient.MessageDisplayHandler(OnReceived);
                                                h_client.OnDisconnected += new HandleClient.DisconnectedHandler(h_client_OnDisconnected);

                                                // Server Login 쓰레드 분리 작업 차후 예정
                                                TcpClient clientSocket = Server.server.AcceptTcpClient();
                                                Server.clientList.Add(clientSocket, id.ToString());
                                                h_client.startClient(clientSocket, Server.clientList);
                                                break;
                                            }catch(Exception e) {
                                                Trace.WriteLine(e.Message);
                                            }
                                        }
                                    }
                                } else {
                                    loginCheck = Encoding.Unicode.GetBytes("FALSE");
                                    stream.Write(loginCheck, 0, loginCheck.Length);
                                    stream.Close();
                                }
                            }
                        }
                    }

                    /*
                    

                    SendMessageAll(Server.clientSocket, userName + "입장하셨습니다.", "", false);
                    SaveLog(userName, userName + "입장하셨습니다.");
                    */

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

        private static UserLogin GetBindAck(byte[] buffer) {
            UserLogin userLogin = new UserLogin();

            MemoryStream ms = new MemoryStream(buffer, false);
            BinaryReader br = new BinaryReader(ms);

            userLogin.UserID = ExtendedTrim(Encoding.UTF8.GetString(br.ReadBytes(20)));
            userLogin.UserPassword = ExtendedTrim(Encoding.UTF8.GetString(br.ReadBytes(20)));

            br.Close();
            ms.Close();

            return userLogin;
        }

        private static string ExtendedTrim(string source) {
            string dest = source;
            int index = dest.IndexOf('\0');
            if (index > -1) {
                dest = source.Substring(0, index + 1);
            }

            return dest.TrimEnd('\0').Trim();
        }


        private static void SaveLog(string userName, string message) {
            using (StreamWriter writer = new StreamWriter($"{DateTime.Today.ToString("yyyyMMdd")}" + "log.txt", true)) {
                writer.WriteLine("DateTime : {0} User : {1} Message : {2}", DateTime.Now.ToString(), userName, message);
            }
        }
        private static void h_client_OnDisconnected(TcpClient clientSocket) {
            if (clientList.ContainsKey(clientSocket)) {
                clientSocket.Close();

                Server.clientList.Remove(clientSocket);
            }

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
