using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Penz_Camping.Controllers
{
    public class ReservierungController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Anfrage()
        {
            return View();
        }

    }
}