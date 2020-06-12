using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CHATTING_SERVER {
    class User {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        private string userID;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        private string userPassword;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        private string name;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        private string gender;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        private string birthday;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
        private string phoneNumber;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        private string emergencyEmail;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        private string makeTime;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        private string login;

        public string UserID { get => userID; }
        public string UserPassword { get => userPassword; }
        public string Name { get => name; }
        public string Gender { get => gender; }
        public string Birthday { get => birthday; }
        public string PhoneNumber { get => phoneNumber; }
        public string EmergencyEmail { get => emergencyEmail; }
        public string MakeTime { get => makeTime; }
        public string Login { get => login; }

        public User(string userID, string userPassword, string name, string gender, string birthday,
            string phoneNumber, string emergencyEmail, string makeTime, string login) {
            this.userID = userID;
            this.userPassword = userPassword;
            this.name = name;
            this.gender = gender;
            this.birthday = birthday;
            this.phoneNumber = phoneNumber;
            this.emergencyEmail = emergencyEmail;
            this.makeTime = makeTime;
            this.login = login;
        }
    }
}
