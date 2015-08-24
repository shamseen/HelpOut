using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpOut.DAL;
using HelpOut.Models;

namespace HelpOut.Controllers
{
    public class HomeController : Controller
    {
        private HelpOutContext db = new HelpOutContext();
        public ActionResult Index()
        {
            List<User> userList = new List<User>();
            ViewBag.isLoggedIn = false;
            LoginViewModel login = new LoginViewModel();
            return View(login);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}