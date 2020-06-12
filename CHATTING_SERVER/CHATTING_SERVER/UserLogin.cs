using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CHATTING_SERVER {
    class UserLogin {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        private string userID;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        private string userPassword;

        public string UserID { get => userID; set => userID = value; }
        public string UserPassword { get => userPassword; set => userPassword = value; }
    }
}
