using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CHATTING_CLIENT {
    [Serializable]
    class UserLogin {
        public static int SIZE = 20 + 20;
        private string userID;
        private string userPassword;

        public string UserID { get => userID; set => userID = value; }
        public string UserPassword { get => userPassword; set => userPassword = value; }
    }
}
