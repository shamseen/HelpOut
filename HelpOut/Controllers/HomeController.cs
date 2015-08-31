using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpOut.Models;
using Microsoft.AspNet.Identity;
namespace HelpOut.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ApplicationUser user = new ApplicationUser();
            string currentUserID = User.Identity.GetUserId();

            if (Request.IsAuthenticated)
            {
                user = (from u in db.Users
                        where u.Id == currentUserID
                        select u).First();
            }

            ViewBag.Temp = user.EventsAttending == null;
            return View(user);

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