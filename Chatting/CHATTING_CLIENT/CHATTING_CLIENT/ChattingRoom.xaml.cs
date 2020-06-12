using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace CHATTING_CLIENT {
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public partial class ChattingRoom : Window {
        TcpClient clientSocket = new TcpClient();
        NetworkStream stream = default(NetworkStream);
        string message = string.Empty;

        public ChattingRoom(string ID) {
            InitializeComponent();
            StartChatting();
        }

        private void StartChatting() {
            clientSocket.Connect(IP.IPAddress, IP.Port);
            stream = clientSocket.GetStream();

            byte[] buffer = Encoding.Unicode.GetBytes(IP.IPAddress + "$");
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();

            Thread t_handler = new Thread(GetMessage);
            t_handler.IsBackground = true;
            t_handler.Start();

            SendDataText.Focus();
        }

        // 기본적인 수신메소드
        private void GetMessage() {
            while (true) {
                stream = clientSocket.GetStream();
                int BUFFERSIZE = clientSocket.ReceiveBufferSize;
                byte[] buffer = new byte[BUFFERSIZE];
                int bytes = stream.Read(buffer, 0, buffer.Length);

                string message = Encoding.Unicode.GetString(buffer, 0, bytes);
                DisPlayText(message, true);

            }
        }

        // user에 따라 글자색이 변경된다. user가 자신이면 검정색 다른사람이면 파랑색
        private void SetColor(string message, bool user) {
            BrushConverter bc = new BrushConverter();
            TextRange tr = new TextRange(ContentOfTalk.Document.ContentEnd, ContentOfTalk.Document.ContentEnd);
            tr.Text = message;
            tr.ApplyPropertyValue(TextElement.BackgroundProperty, bc.ConvertToString(user == true ? Colors.Yellow : Colors.White));


            ContentOfTalk.AppendText(Environment.NewLine);
            ContentOfTalk.ScrollToEnd();

        }

        // user와 UI쓰레드(flag) 접근권한에따라 분배 
        private void SetWordColor(string message, bool user, bool flag) {
            if (user == true && flag == true) {
                SetColor(message, user);
            } else if (user == true && flag == false) {
                ContentOfTalk.Dispatcher.BeginInvoke(new Action(delegate {
                    SetColor(message, user);
                }));
            } else if (user == false && flag == true) {
                SetColor(message, user);
            } else {
                ContentOfTalk.Dispatcher.BeginInvoke(new Action(delegate {
                    SetColor(message, user);
                }));
            }
        }
        // 메시지를 출력하기 위해 UI쓰레드 or Work쓰레드따라 SetWordColor함수 호출
        private void DisPlayText(string message, bool flag) {
            if (flag == true) {
                if (ContentOfTalk.Dispatcher.CheckAccess()) SetWordColor(message, true, true);
                else SetWordColor(message, true, false);
            } else {
                if (ContentOfTalk.Dispatcher.CheckAccess()) SetWordColor(message, false, true);
                else SetWordColor(message, false, false);
            }
        }

        // 채팅 화면을 움직이게 만들어주는 함수
        private void ScreenMove(object sender, MouseButtonEventArgs e) {
            this.DragMove();
        }

        // 닫기 버튼을 눌렀을때 채팅창을 종료
        private void Close_Window(object sender, MouseButtonEventArgs e) {
            if (clientSocket != null) {
                string exitMessage = IP.IPAddress + "님이 나갔습니다.";
                byte[] buffer = Encoding.Unicode.GetBytes(exitMessage + "$");
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();

                DisPlayText(SendDataText.Text, false);
                SendDataText.Text = "";
                SendDataText.Focus();
            }
            clientSocket.Close();
            this.Close();
        }

        // 최대화 버튼을 눌렀을때 채팅방을 최대화로 만들어준다.
        // 이미 최대화일때 다시 보통크키로 만든다.
        private void Maximize_Window(object sender, MouseButtonEventArgs e) {
            this.WindowState = (this.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
            ContentOfTalk.Width =
            ContentOfTalk.Height = 800;
        }

        // 최소화 버튼을 눌렀을때 채팅방을 최소화로 만들어준다.
        private void Minimize_Window(object sender, MouseButtonEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        // 기본적인 전송메소드
        private void Send() {
            if (SendDataText.Text != string.Empty) {
                byte[] buffer = Encoding.Unicode.GetBytes(SendDataText.Text + "$");
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();

                DisPlayText(SendDataText.Text, false);
                SendDataText.Text = "";
                SendDataText.Focus();
            }
        }

        // 전송버튼 눌렀을경우 실행
        private void Button_Click(object sender, RoutedEventArgs e) {
            Send();
        }

        // SendDataText의 키보드 다운 이벤트를 분배한다. 
        private void SendDataText_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) Send();
            if (e.Key == Key.Back && SendDataText.Text == string.Empty) SendButton.IsEnabled = false;
            else SendButton.IsEnabled = SendDataText.Text == string.Empty ? false : true;

            if (SendDataText.Text.Length > 1) {
                SendDataText.Text += ((SendDataText.Text.Length % 40) == 0) ? Environment.NewLine : "";
                SendDataText.Select(SendDataText.Text.Length, 0);
                SendDataText.Focus();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            WindowSystem.originalWidth = this.Width;
            WindowSystem.originalHeight = this.Height;

            if (this.WindowState == WindowState.Maximized)
                ChangeSize(this.ActualWidth, this.ActualHeight);

            this.SizeChanged += new SizeChangedEventHandler(Window_SizeChanged);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            ChangeSize(e.NewSize.Width, e.NewSize.Height);
        }

        private void ChangeSize(double actualWidth, double actualHeight) {
            WindowSystem.scale.ScaleX = Width / WindowSystem.originalWidth;
            WindowSystem.scale.ScaleY = Height / WindowSystem.originalHeight;
        }
    }
}