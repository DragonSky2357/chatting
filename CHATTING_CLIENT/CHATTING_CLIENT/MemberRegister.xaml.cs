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
using System.Windows.Shapes;

namespace CHATTING_CLIENT {
    /// <summary>
    /// MemberRegister.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MemberRegister : Window { 
        string focusTempText = string.Empty;

        public MemberRegister() {
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

        private void PasswordCheckText_KeyDown(object sender, KeyEventArgs e) {
        }

        private void GenderClick(object sender, MouseButtonEventArgs e) {

        }
    }
}
