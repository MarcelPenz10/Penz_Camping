using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Penz_Camping.Controllers
{
    public class CampingplatzController : Controller
    {

        // GET: /Campingplatz/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Campingplatz/Infos/
        public ActionResult Infos()
        {
            return View();
        }

        public ActionResult Preise()
        {
            return View();
        }

        public ActionResult Bilder()
        {
            return View();
        }

    }
}