using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHATTING_CLIENT {
    class Member {
        public string ID { get => ID; set => ID = value; }
        public string Password { get => Password; set => Password = value; }
        public string name { get => name; set => name = value; }
        public bool gender { get => gender; set => gender = value; }
        public DateTime birthDay { get => birthDay; set => birthDay = value; }
        public string emergencyEmail { get => emergencyEmail; set => emergencyEmail = value; }
        public string phoneNumber { get => phoneNumber; set => phoneNumber = value; }
    }
}
