using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            if((this.ID.Text!=string.Empty) && (this.Password.Password!=string.Empty)) {
                string strConn = String.Format("Data Source={0}", AppDomain.CurrentDomain.BaseDirectory + @"\DB\ChattingDB.db");

                using (SQLiteConnection conn = new SQLiteConnection(strConn)) {
                    conn.Open();

                    //string sql = "INSERT INTO User(USERID,USERPASSWORD) VALUES('test','1234')";
                    //SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    //cmd.ExecuteNonQuery();
                    string userID = this.ID.Text;
                    string userPassword = this.Password.Password;

                    string sql = string.Format("SELECT * FROM User WHERE UserID='{0}'", userID);

                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    SQLiteDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows) {
                        while (rdr.Read()) {
                            var id = rdr["UserID"];
                            var pw = rdr["UserPassword"];
                        }
                        MessageBox.Show("Sucess", "asd", MessageBoxButton.OK);
                        Window chattingWindow = new MainWindow();
                        chattingWindow.Show();
                        this.Close();
                    }

                    

                    //cmd.CommandText = "DELETE FROM User WHERE Id=2";
                    //cmd.ExecuteNonQuery();

                    conn.Close();
                }
               
            }
            
        }

        private void ScreenMove(object sender, MouseButtonEventArgs e) {
            this.DragMove();
        }

        private void IDFind_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {

        }

        private void PasswordFind_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {

        }

        private void MembeRegister_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            new MemberRegister().Show();
        }

        private void LoginLink_Click(object sender, RoutedEventArgs e) {
            var linkButton = sender as Button;
            string linkUrl = loginLinkUrl[linkButton.Name];

            System.Diagnostics.Process.Start(linkUrl);
        }
    }
}
