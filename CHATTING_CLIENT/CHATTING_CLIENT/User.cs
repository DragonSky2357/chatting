using System;
using System.Runtime.InteropServices;

namespace CHATTING_CLIENT {
    [Serializable]
    class User {
        private string userID;
        private string userPassword;
        private string name;
        private string gender;
        private string birthday;
        private string phoneNumber;
        private string emergencyEmail;
        private string makeTime;
        private string login;

        public string UserID { get => userID; }
        public string UserPassword { get => userPassword;  }
        public string Name { get => name; }
        public string Gender { get => gender;  }
        public string Birthday { get => birthday;  }
        public string PhoneNumber { get => phoneNumber;  }
        public string EmergencyEmail { get => emergencyEmail;}
        public string MakeTime { get => makeTime;  }
        public string Login { get => login;  }

        public User(string userID,string userPassword,string name,string gender,string birthday,
            string phoneNumber, string emergencyEmail,string makeTime, string login) {
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
