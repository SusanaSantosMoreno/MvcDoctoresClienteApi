using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDoctoresClienteApi.Models {
    public class LoginModel {

        public String userName { get; set; }
        public String password { get; set; }

        public LoginModel () {}

        public LoginModel (string userName, string password) {
            this.userName = userName;
            this.password = password;
        }
    }
}
