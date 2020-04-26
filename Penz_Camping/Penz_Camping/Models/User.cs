using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penz_Camping.Models
{
    public enum Gender { male, female, notspecified }
    public enum Rolle { admin, registrierterBenutzer, User, notSpecified}

    public class User
    {
        public int ID { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public Gender Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Rolle Rolle { get; set; }
  

        public User() : this(0, "", "", Gender.notspecified, DateTime.MinValue, "", "", Rolle.notSpecified) { }

        public User(int id, string firstname, string lastname, Gender gender, DateTime? birthdate, string username, string password, Rolle rolle)
        {
            this.ID = id;
            this.Vorname = firstname;
            this.Nachname = lastname;
            this.Gender = gender;
            this.Birthdate = birthdate;
            this.Username = username;
            this.Password = password;
            this.Rolle = rolle;
        }


    }
}