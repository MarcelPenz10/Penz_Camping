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
        private IRegistrierung reg;

        // GET: Benutzerverwaltung
        public ActionResult Index()
        {
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
                Session["loggedinUser"] = userFromDB;
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




