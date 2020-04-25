using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Diagnostics;

namespace CHATTING_LOGIN {
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
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

            if ((this.ID.Text != string.Empty) && (this.Password.Password != string.Empty)) {
                string strConn = String.Format("Data Source={0}", AppDomain.CurrentDomain.BaseDirectory + @"database\Chatting.db");

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

                    //var a = rdr.HasRows;

                    if (rdr.HasRows) {
                        //userid ,userpw, username, userE-mail, userPhone, userNicname, user birthday
                        while (rdr.Read()) {
                            var id = rdr["UserID"];
                            var pw = rdr["UserPassword"];
                        }
                        string path = @"CHATTING_CLIENT.exe";
                        Process.Start(AppDomain.CurrentDomain.BaseDirectory + path);
                    } else {
                        MessageBox.Show("등록된 아이디가 업습니다.", "오류", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    

                    //cmd.CommandText = "DELETE FROM User WHERE Id=2";
                    //cmd.ExecuteNonQuery();

                    conn.Close();
                }
                

                this.Close();
            }else if((this.ID.Text == string.Empty)) {
                MessageBox.Show("아이디를 입력해주세요", "오류", MessageBoxButton.OK, MessageBoxImage.Information);
            }else if(((this.Password.Password == string.Empty))) {
                MessageBox.Show("비밀번호 입력해주세요", "오류", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
