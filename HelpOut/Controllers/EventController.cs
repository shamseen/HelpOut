using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpOut.Models;
using Microsoft.AspNet.Identity;

namespace HelpOut.Controllers
{
    public class EventController : Controller
    {
        private ApplicationDbContext db2 = new ApplicationDbContext();

        // GET: Event

        public ActionResult Index(string sortOrder, string searchString)
        {
            var events = from e in db2.Events
                         select new EventDTO()
                         {
                             EventID = e.EventID,
                             Name = e.Name,
                             DateTime = e.DateTime,
                             Location = e.Location,
                             OrganizationName = e.Organization.FullName
                         };

            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.Name.ToUpper().Contains(searchString.ToUpper())
                                       || e.Location.ToUpper().Contains(searchString.ToUpper())
                                       || e.OrganizationName.ToUpper().Contains(searchString.ToUpper()));
            }


            switch (sortOrder)
            {
                case "Date":
                    events = events.OrderBy(e => e.DateTime);
                    break;
                case "date_desc":
                    events = events.OrderByDescending(e => e.DateTime);
                    break;
                default:
                    events = events.OrderBy(e => e.DateTime);
                    break;
            }
           return View(events.ToList());
        }

        // GET: Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var @event = (from e in db2.Events
                         where e.EventID == id
                         select new EventDetailDTO()
                         {
                             EventID = e.EventID,
                             Name = e.Name,
                             DateTime = e.DateTime,
                             Location = e.Location,
                             Description = e.Description,
                             OrganizationName = e.Organization.FullName
                         }).First();

            if (@event == null)
            {
                return HttpNotFound();
            }

            ViewBag.rsvpText = "RSVP!";
            return View(@event);
        }

        
        [HttpPost, ActionName("Details")]
        [Authorize(Roles = "Volunteer")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSignups(int eventID, string volunteerID)
        {
            ApplicationUser volunteer = (from u in db2.Users
                             where u.Id == volunteerID
                             select u).Include("EventsAttending").Single();


            Event @event = (from e in db2.Events
                            where e.EventID == eventID
                            select e).Include("Attendees").Include("Organization").Single();
            //if (volunteer.EventsAttending == null)
            //    ViewBag.rsvpText = "Vol List";

            //else if (@event.Attendees == null)
            //    ViewBag.rsvpText = "Event List";
            volunteer.EventsAttending.Add(@event);
            @event.Attendees.Add(volunteer);

            EventDetailDTO dto = new EventDetailDTO() {
                EventID = @event.EventID,
                Name = @event.Name,
                DateTime = @event.DateTime,
                Location = @event.Location,
                Description = @event.Description,
                OrganizationName = @event.Organization.FullName
            };

            ViewBag.rsvpText = "Attending!";

            return View(dto);

        }

        // GET: Event/Create
        [Authorize (Roles="Organization")]
        public ActionResult Create()
        {
            ViewBag.OrganizationID = new SelectList(db2.Users, "Id", "Email");
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,Name,DateTime,Location,Description")] Event @event)
        {
            
            if (ModelState.IsValid)
            {
                @event.OrganizationID = User.Identity.GetUserId();
                db2.Events.Add(@event);
                db2.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrganizationID = new SelectList(db2.Users, "Id", "Email", @event.OrganizationID);
            return View(@event);
        }

        // GET: Event/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db2.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationID = new SelectList(db2.Users, "Id", "Email", @event.OrganizationID);
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,Name,DateTime,Location,Description,OrganizationID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db2.Entry(@event).State = EntityState.Modified;
                db2.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationID = new SelectList(db2.Users, "Id", "Email", @event.OrganizationID);
            return View(@event);
        }


        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db2.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db2.Events.Find(id);
            db2.Events.Remove(@event);
            db2.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db2.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
