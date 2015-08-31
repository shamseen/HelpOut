using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpOut.Models;

using System.Net;

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

            return View(user);

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

            //if (count == 1 && q.First().Password == login.Password)
            //{
            //    User user = (from c in q
            //                 select c).First();

            //    ViewBag.isLoggedIn = true;
            //    ViewBag.Email = user.Email;
            //    ViewBag.FullName = user.FullName;
            //}
            return View();
        }
        //public ActionResult userprofile(string? id)
        //{
        //    if (id = null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var myuser = (from e in db.Users
        //                  where e.Id = id
        //                  select new EventDetailDTO()
        //                  {
        //                      EventID = e.EventID,
        //                      Name = e.Name,
        //                      DateTime = e.DateTime,
        //                      Location = e.Location,
        //                      Description = e.Description,
        //                      OrganizationName = e.Organization.FullName
        //                  }).First();

        //    if (myuser == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(@event);

           
        //}

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