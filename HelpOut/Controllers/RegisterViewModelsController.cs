using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpOut.Models;

namespace HelpOut.Controllers
{
    public class RegisterViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RegisterViewModels
        public ActionResult Index()
        {   


            return View(db.ApplicationUsers.ToList());
        }

        // GET: RegisterViewModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterViewModel registerViewModel = db.ApplicationUsers.Find(id);
            if (registerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(registerViewModel);
        }

        // GET: RegisterViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisterViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Location,Description,Website,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Password,ConfirmPassword")] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(registerViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(registerViewModel);
        }

        // GET: RegisterViewModels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterViewModel registerViewModel = db.ApplicationUsers.Find(id);
            if (registerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(registerViewModel);
        }

        // POST: RegisterViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Location,Description,Website,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Password,ConfirmPassword")] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registerViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registerViewModel);
        }

        // GET: RegisterViewModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegisterViewModel registerViewModel = db.ApplicationUsers.Find(id);
            if (registerViewModel == null)
            {
                return HttpNotFound();
            }
            return View(registerViewModel);
        }

        // POST: RegisterViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            RegisterViewModel registerViewModel = db.ApplicationUsers.Find(id);
            db.Users.Remove(registerViewModel);
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
