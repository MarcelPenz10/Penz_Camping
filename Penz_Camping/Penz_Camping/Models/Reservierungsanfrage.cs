using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Penz_Camping.Models
{
    public enum Paket { billig,normal,teuer}

    public class Reservierungsanfrage
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }    
        public int Kreditkartennummer { get; set; }
        public DateTime? ErsterTagBuchung { get; set; }
        public DateTime? LetzterTagBuchung { get; set; }
        public Paket Paket { get; set; }
        public string Password { get; set; }
        public bool Bearbeitet { get; set; }
        

        public Reservierungsanfrage() : this("", "", 0, DateTime.MinValue, DateTime.MaxValue, Paket.billig,"", false) { }

        public Reservierungsanfrage(string vorname, string nachname, int kreditkartennummer, DateTime? ersterTagBuchung, DateTime? letzterTagBuchung, Paket paket, string password, bool bearbeitet)
        {
            this.Vorname = vorname;
            this.Nachname = nachname;
            this.Kreditkartennummer = kreditkartennummer;
            this.ErsterTagBuchung = ersterTagBuchung;
            this.LetzterTagBuchung = letzterTagBuchung;
            this.Paket = paket;
            this.Password = password;
            this.Bearbeitet = bearbeitet;
        }
    }
}