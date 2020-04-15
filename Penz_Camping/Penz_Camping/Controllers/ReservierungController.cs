using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Penz_Camping.Models;
using Penz_Camping.Models.DB;

namespace Penz_Camping.Controllers
{
    public class ReservierungController : Controller
    {
        private IReservierung res;
        
       
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Anfrage()
        {
            return View(new Reservierungsanfrage());
        }

        [HttpPost]
        public ActionResult Anfrage(Reservierungsanfrage reservierungsanfrage)
        {

            if (reservierungsanfrage == null)
            {
                return RedirectToAction("Reservierung");
            }

            CheckData(reservierungsanfrage);

            if (!ModelState.IsValid)
            {
                return View(reservierungsanfrage);
            }
            else
            {
                res = new ReservierungDB();

                res.Open();

                if (res.Insert(reservierungsanfrage))
                {
                    res.Close();
                    return View("Message", new Message("Anfrage", "Ihre Anfrage war erfolgreich"));
                }
                else
                {
                    res.Close();
                    return View("Message", new Message("Anfrage", "Ihre Anfrage war nicht erfolgreich"));
                }
            }
        }

        





        private void CheckData(Reservierungsanfrage reservierungsanfrage)
        {
            if (reservierungsanfrage == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(reservierungsanfrage.Nachname.Trim()))
            {
                ModelState.AddModelError("Nachname", "Nachname ist ein Pflichtfeld.");
            }

            if (reservierungsanfrage.Kreditkartennummer == 0)
            {
                ModelState.AddModelError("Kreditkartennummer", "Kreditkartennummer ist ein Pflichtfeld.");
            }

            if (reservierungsanfrage.ErsterTagBuchung == null)
            {
                ModelState.AddModelError("ErsterTagBuchung", "Erster Tag Buchung ist ein Pflichtfeld");
            }

            if (reservierungsanfrage.LetzterTagBuchung == null)
            {
                ModelState.AddModelError("LetzterTagBuchung", "Letzter Tag Buchung ist ein Pflichtfeld");
            }

            if(reservierungsanfrage.Password == null)
            {
                ModelState.AddModelError("Password", "Password ist ein Pflichtfeld");
            }
        }
    }
}