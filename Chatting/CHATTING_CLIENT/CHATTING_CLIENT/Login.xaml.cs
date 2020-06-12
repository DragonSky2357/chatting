using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CHATTING_CLIENT {
    /// <summary>
    /// Login.xaml에 대한 상호 작용 논리
    /// </summary>
    
    public partial class Login : Window {
        Dictionary<string, string> loginLinkUrl = new Dictionary<string, string>();

        public Login() {
            InitializeComponent();
            LoginLinkUrlSet();
        }

        private void LoginLinkUrlSet() {
            loginLinkUrl.Add("LinkNaverBtn", "https://www.naver.com/");
            loginLinkUrl.Add("LinkGoogleBtn", "https://www.google.com/");
            loginLinkUrl.Add("LoginFacebookBtn", "https://www.facebook.com/");
        }
        // 닫기 버튼을 눌렀을때 채팅창을 종료
        private void Close_Window(object sender, MouseButtonEventArgs e) {
            this.Close();
        }

        // 최대화 버튼을 눌렀을때 채팅방을 최대화로 만들어준다.
        // 이미 최대화일때 다시 보통크키로 만든다.
        private void Maximize_Window(object sender, MouseButtonEventArgs e) {
            this.WindowState = (this.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
        }

        // 최소화 버튼을 눌렀을때 채팅방을 최소화로 만들어준다.
        private void Minimize_Window(object sender, MouseButtonEventArgs e) {
            WindowState = WindowState.Minimized;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e) {
            ClientSocket cs = new ClientSocket();

            byte[] buffer = Encoding.Unicode.GetBytes(Command.LoginButtonClick);
            cs.stream.Write(buffer, 0, buffer.Length);
            cs.stream.Flush();

            UserLogin userLogin = new UserLogin();
            userLogin.UserID = ID.Text.ToString();
            userLogin.UserPassword = Password.Password.ToString();

            byte[] userBuffer = GetBytes_Bind(userLogin);
            cs.stream.Write(userBuffer, 0, userBuffer.Length);

            byte[] endMessage = Encoding.Unicode.GetBytes("<EndMessage>");
            cs.stream.Write(endMessage,0, endMessage.Length);

            byte[] loginCheck = new byte[1024];
            cs.stream = cs.clientSocket.GetStream();
            int bytes = cs.stream.Read(loginCheck, 0, loginCheck.Length);
            string command = Encoding.Unicode.GetString(loginCheck, 0, loginCheck.Length);
            command = command.Substring(0, command.IndexOf("\0"));
            cs.stream.Close();

            if (command == "TRUE") {
                new ChattingRoom("").Show();
                this.Close();
            } else if (command == "ALREADY") {
                new ChattingRoom("").Show();
                this.Close();
                LoginFailed.Content = "이미 로그인된 아이디 입니다.";
                LoginFailed.Visibility = Visibility.Visible;
            } else if (command == "FALSE") {
                LoginFailed.Content = "계정 또는 비밀번호를 다시 확인해 주세요.";
                LoginFailed.Visibility = Visibility.Visible;
            }

        }
        private byte[] GetBytes_Bind(UserLogin data) {
            byte[] buffer = new byte[UserLogin.SIZE];

            MemoryStream ms = new MemoryStream(buffer,true);
            BinaryWriter bw = new BinaryWriter(ms);

            try {
                byte[] byteID = new byte[20];
                Encoding.UTF8.GetBytes(data.UserID, 0,data.UserID.Length, byteID, 0);
                bw.Write(byteID);

                byte[] bytePassword = new byte[20];
                Encoding.UTF8.GetBytes(data.UserPassword, 0, data.UserPassword.Length,bytePassword, 0);
                bw.Write(bytePassword);
            } catch (Exception e) {
                Trace.WriteLine("{0}", e.Message);
            }

            bw.Close();
            ms.Close();

            return buffer;
        }

        private void ScreenMove(object sender, MouseButtonEventArgs e) {
            this.DragMove();
        }

        private void LoginLink_Click(object sender, RoutedEventArgs e) {
            var linkButton = sender as Button;
            string linkUrl = loginLinkUrl[linkButton.Name];

            System.Diagnostics.Process.Start(linkUrl);
        }

        private void IDTextBox_GotFocus(object sender, RoutedEventArgs e) {
            LabelID.Visibility = Visibility.Hidden;
            ID.Foreground = System.Windows.Media.Brushes.Black;
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e) {
            LabelPassword.Visibility = Visibility.Hidden;
            Password.Foreground = System.Windows.Media.Brushes.Black;
        }

        private void ID_TextChanged(object sender, TextChangedEventArgs e) {
            ChnageLoginButton();

        }
        private void Password_PasswordChanged(object sender, RoutedEventArgs e) {
            ChnageLoginButton();
        }
        private void ChnageLoginButton() {
            if (ID.Text.Length != 0 && Password.Password.Length >= 4) {
                LoginButton.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#594941");
                LoginButton.Foreground = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#FFFFFF");
                LoginButton.IsEnabled = true;
            } else {
                LoginButton.Background = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#F6F6F6");
                LoginButton.Foreground = (System.Windows.Media.Brush)new BrushConverter().ConvertFrom("#808080");
                LoginButton.IsEnabled = false;
            }
        }

        private void ID_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) LoginButton_Click(null, null);
        }

        private void Password_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) LoginButton_Click(null, null);
        }

        private void MembeRegister_Click(object sender, RoutedEventArgs e) {
            new UserRegister().Show();
        }
    }
}
