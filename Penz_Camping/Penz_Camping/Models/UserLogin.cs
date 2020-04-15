using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penz_Camping.Models
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordAdmin { get; set; }

        public UserLogin() : this("", "", "") { }

        public UserLogin(string username, string password, string passwordAdmin)
        {
            this.Username = username;
            this.Password = password;
            this.PasswordAdmin = PasswordAdmin;
        }


    }
}