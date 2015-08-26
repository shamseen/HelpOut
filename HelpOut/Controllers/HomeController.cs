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
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<User> userList = new List<User>();
            ViewBag.isLoggedIn = false;
            LoginViewModel login = new LoginViewModel();
            return View(login);

        }

        [HttpPost]
        public ActionResult Index(LoginViewModel login)
        {
            //ViewBag.Message = "This log in didn't break anything.";

            //ViewBag.isLoggedIn = false;

            //var q = from c in db.Users
            //        where login.Email == c.Email
            //        select c;
            //q = q.Where(u => u.Email.Equals(login.Email));

            //int count = q.Count(u => u.Email == u.Email);

            //if (count == 1 && q. == login.Password)
            //{
            //    User user = (from c in q
            //                select c).First();

            //    ViewBag.isLoggedIn = true;
            //    ViewBag.Email = user.Email;
            //    ViewBag.FullName = user.FullName;
            //}

            return View();
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