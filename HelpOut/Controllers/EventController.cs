using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpOut.DAL;
using HelpOut.Models;

namespace HelpOut.Controllers
{
    public class EventController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Event

        public ActionResult Index(string sortOrder, string searchString)
        {
            var events = from e in db.Events
                         select new EventDTO()
                         {
                             EventID = e.EventID,
                             Name = e.Name,
                             DateTime = e.DateTime,
                             Location = e.Location,
                             OrganizationName = e.Organization.FirstName
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

            var @event = (from e in db.Events
                         where e.EventID == id
                         select new EventDetailDTO()
                         {
                             EventID = e.EventID,
                             Name = e.Name,
                             DateTime = e.DateTime,
                             Location = e.Location,
                             Description = e.Description,
                             OrganizationName = e.Organization.FirstName
                         }).First();

            if (@event == null)
            {
                return HttpNotFound();
            }

            return View(@event);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            ViewBag.OrganizationID = new SelectList(db.Users, "id", "Email");
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,Name,DateTime,Location,Description,OrganizationID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrganizationID = new SelectList(db.Users, "UserID", "Email", @event.OrganizationID);
            return View(@event);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrganizationID = new SelectList(db.Users, "UserID", "Email", @event.OrganizationID);
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
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrganizationID = new SelectList(db.Users, "UserID", "Email", @event.OrganizationID);
            return View(@event);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
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
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
