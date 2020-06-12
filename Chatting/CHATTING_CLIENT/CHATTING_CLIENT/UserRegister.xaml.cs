using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CHATTING_CLIENT {
    /// <summary>
    /// MemberRegister.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserRegister : Window { 
        string focusTempText = string.Empty;
        bool genderMen = true;

        public UserRegister() {
            InitializeComponent();
        }

        private void ScreenMove(object sender, MouseButtonEventArgs e) {
            this.DragMove();
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

        private void GenderClick(object sender, RoutedEventArgs e) {
            var gender = sender as Button;

            if(gender.Name == "BtnMen") {
                BtnMen.Background = Brushes.GreenYellow;
                BtnWomen.Background = Brushes.White;
                genderMen = true;
            } else {
                BtnMen.Background = Brushes.White;
                BtnWomen.Background = Brushes.GreenYellow;
                genderMen = false;
            }
            
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
            var textBox = sender as TextBox;
            textBox.Text = "";
        }

        private bool EmptyUserInfo() {
            if (string.IsNullOrEmpty(IDText.Text.ToString()) ||
                string.IsNullOrEmpty(PasswordText.Password.ToString()) ||
                string.IsNullOrEmpty(PasswordCheckText.Password.ToString()) ||
                string.IsNullOrEmpty(NameText.Text.ToString()) ||
                string.IsNullOrEmpty(BirthDay.SelectedDate.ToString()) ||
                string.IsNullOrEmpty(EmergencyEmail.Text.ToString()) ||
                string.IsNullOrEmpty(SecondPhoneNumber.Text.ToString()) ||
                string.IsNullOrEmpty(SecondPhoneNumber.Text.ToString()) ||
                string.IsNullOrEmpty(ThirdPhoneNumber.Text.ToString())) {
                MessageBox.Show("비어있는 정보가 있습니다. 확인해 주세요");
                return true;
            }
                

            return false;

        }
        private void BtnSend_Click(object sender, RoutedEventArgs e) {

            if (RadioBtnYes.IsChecked == false) {
                MessageBox.Show("개인 정보 수집 이용 동의하지 않으시면 가입을 하실 수 없습니다. ");
                return;
            }
            if (EmptyUserInfo()) return;

            string strConn = String.Format("Data Source={0}", AppDomain.CurrentDomain.BaseDirectory + @"\DB\ChattingDB.db");

            using (SQLiteConnection conn = new SQLiteConnection(strConn)) {
                conn.Open();
                //string sql = "INSERT INTO User(USERID,USERPASSWORD) VALUES('test','1234')";
                //SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                //cmd.ExecuteNonQuery();
                string userID = IDText.Text.ToString();
                string userPassword = PasswordText.Password.ToString();
                string userName = NameText.Text.ToString();
                string userGender = genderMen == true ? "남" : "여";
                string userBirthDay = BirthDay.SelectedDate.ToString();
                string userEmergencyEmail = EmergencyEmail.Text.ToString();
                string userPhoneNumber = FirstPhoneNumber.ToString()+SecondPhoneNumber.Text.ToString()+ThirdPhoneNumber.Text.ToString();
                string makeTime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

                string sql = string.Format($"INSERT INTO User (UserID,UserPassword,Name,Gender,BirthDay," +
                    $"PhoneNumber,EmergencyEmail,MakeTime,Login) VALUES" +
                    $"(\"{userID}\",\"{userPassword}\",\"{userName}\",\"{userGender}\"," +
                    $"\"{userBirthDay}\",\"{userEmergencyEmail}\",\"{userPhoneNumber}\",\"{makeTime}\",\"{'0'}\")");


                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();

                RegisterInfoSendToGmail();
                //SQLiteDataReader rdr = cmd.ExecuteReader();

                /*
                if (rdr.HasRows) {
                    while (rdr.Read()) {
                        var id = rdr["UserID"];
                        var pw = rdr["UserPassword"];
                    }
                    MessageBox.Show("Sucess", "asd", MessageBoxButton.OK);
                }
                */


                //cmd.CommandText = "DELETE FROM User WHERE Id=2";
                //cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        private void PasswordChangedCheck(object sender, RoutedEventArgs e) {
            if ((PasswordText.Password.Equals(PasswordCheckText.Password)))
                PasswordReCheckUI.Visibility = Visibility.Visible;
            else
                PasswordReCheckUI.Visibility = Visibility.Hidden;
        }

        private void RegisterInfoSendToGmail() {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false; // 시스템에 설정된 인증 정보를 사용하지 않는다.
            client.EnableSsl = true;  // SSL을 사용한다.
            client.DeliveryMethod = SmtpDeliveryMethod.Network; // 이걸 하지 않으면 Gmail에 인증을 받지 못한다.
            client.Credentials = new System.Net.NetworkCredential("qkrdydals327@gmail.com", "free9803@&");
            MailAddress from = new MailAddress("qkrdydals327@gmail.com", "test", System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress("qkrdydals327@naver.com");

            MailMessage message = new MailMessage(from, to);

            message.Body = "This is a test e-mail message sent by an application. ";
            string someArrows = new string(new char[] { '\u2190', '\u2191', '\u2192', '\u2193' });
            message.Body += Environment.NewLine + someArrows;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "test message 2" + someArrows;
            message.SubjectEncoding = System.Text.Encoding.UTF8;

            try {
                // 동기로 메일을 보낸다.
                client.Send(message);

                // Clean up.
                message.Dispose();
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }


        }
    }
    
}
