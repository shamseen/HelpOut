using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelpOut.Models;

using System.Net;

using Microsoft.AspNet.Identity;
using HelpOut.ModelViews;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HelpOut.Controllers
{
    public class HomeController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var events = from e in db.Events
                         orderby e.DateTime descending
                         select new EventDTO()
                         {
                             EventID = e.EventID,
                             Name = e.Name,
                             DateTime = e.DateTime,
                             Address = e.Address,
                             City = e.City,
                             State = e.State,
                             ZipCode = e.ZipCode,
                             Description = e.Description,
                             OrganizationName = e.Organization.FullName
                         };


            ViewBag.numAttendees = (from e in db.Events.Include("Attendees")
                               orderby e.DateTime descending
                               select e.Attendees.Count).ToList();

            //ViewBag.numAttendees = new List<int>();

            //foreach (var list in listOfLists)
            //    ViewBag.numAttendees.Add(list.Count);

            return View(events.ToList());
            
            //ApplicationUser user = new ApplicationUser();
            //string currentUserID = User.Identity.GetUserId();

            //if (Request.IsAuthenticated)
            //{
            //    user = (from u in db.Users
            //            where u.Id == currentUserID
            //            select u).First();
            //}
            
            //return View(user);
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