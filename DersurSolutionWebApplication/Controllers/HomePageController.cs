using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DersurSolutionWebApplication.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult ServicePage()
        {
            return View();
        }

        public ActionResult PortfolioPage()
        {
            return View();
        }
        public ActionResult AboutPage()
        {
            return View();
        }
        public ActionResult ContactPage()
        {
            return View();
        }
    }
}