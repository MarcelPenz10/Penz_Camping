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
        public Rolle Rolle { get; set; }

        public UserLogin() : this("", "", Rolle.notSpecified) { }

        public UserLogin(string username, string password, Rolle rolle)
        {
            this.Username = username;
            this.Password = password;
            this.Rolle = rolle;
        }


    }
}