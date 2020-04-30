using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Penz_Camping.Models;
using Penz_Camping.Models.DB;


namespace Penz_Camping.Controllers
{
    public class BenutzerverwaltungController : Controller
    {
        private IReservierung res;
        private IRegistrierung reg;

        // GET: Benutzerverwaltung
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Reservierungsanfragen()
        {
            List<Reservierungsanfrage> reservierungsanfragen;

            res = new ReservierungDB();
            res.Open();

            reservierungsanfragen = res.GetAllRes();
            res.Close();

            List<Reservierungsanfrage> neueRes = new List<Reservierungsanfrage>();

            foreach (var r in reservierungsanfragen)
            {
                if (!r.Bearbeitet)
                {
                    neueRes.Add(r);
                }
            }

            return View(neueRes);
        }

       public ActionResult AnfrageBestätigen(int knr)
        {
            if (Session["loggedInUser"] == null)
            {
                return RedirectToAction("login", "benutzerverwaltung");
            }

            if (!Convert.ToBoolean(Session["isAdmin"]))
            {
                return RedirectToAction("index", "home");
            }

            res = new ReservierungDB();

            res.Open();
            res.UpdateAnfrageStatus(knr, true);
            res.Close();
            return View();
        }

        public ActionResult AnfrageLöschen(int knr)
        {
            if (Session["loggedInUser"] == null)
            {
                return RedirectToAction("login", "benutzerverwaltung");
            }

            if (!Convert.ToBoolean(Session["isAdmin"]))
            {
                return RedirectToAction("index", "home");
            }

            res = new ReservierungDB();

            res.Open();
            res.AnfrageLöschen(knr);
            res.Close();
            return View();
        }


        [HttpGet]
        public ActionResult Registration()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {

            if (user == null)
            {
                return RedirectToAction("Registration");
            }

            CheckUserData(user);

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                reg = new RegistrationDB();

                reg.Open();

                if (reg.Insert(user))
                {
                    reg.Close();
                    return View("Message", new Message("Registrierung", "Ihre Daten wurde erfolgreich abgespeichert"));
                }
                else
                {
                    reg.Close();
                    return View("Message", new Message("Registrierung", "Ihre Daten konnten nicht abgespeichert werden"));
                }
            }
        }



        public ActionResult Login()
        {
            return View(new UserLogin());      
        }

        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
                     
            Session["isAdmin"] = Rolle.admin;
            

            User userFromDB;
            reg = new RegistrationDB();
            reg.Open();
            userFromDB = reg.Login(user);
            reg.Close();

            if (userFromDB == null)
            {
                ModelState.AddModelError("Username", "Benutzername oder Passwort stimmen nicht überein!");
                return View(user);
            }

       
            else
            {
                Session["loggedInUser"] = userFromDB;

                if (userFromDB.Rolle == Rolle.admin)
                {
                    Session["isAdmin"] = true;
                }
                else
                {
                    Session["isAdmin"] = false;
                }


                return RedirectToAction("index", "home");
            }

        }



        private void CheckUserData(User user)
        {
            if (user == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(user.Nachname.Trim()))
            {
                ModelState.AddModelError("Lastname", "Nachname ist ein Pflichtfeld.");
            }

            if (user.Gender == Gender.notspecified)
            {
                ModelState.AddModelError("Gender", "Bitte wählen Sie das Geschlecht aus.");
            }

            if (string.IsNullOrEmpty(user.Username.Trim()))
            {
                ModelState.AddModelError("Username", "Benutzername ist ein Pflichtfeld.");
            }
            if (user.Password == null)
            {
                ModelState.AddModelError("Password", "Password ist ein Pflichtfeld");
            }

            if(user.Rolle == Rolle.notSpecified)
            {
                ModelState.AddModelError("Rolle", "Bitte wählen Sie ihre Rolle aus");
            }
        }

    }
}




