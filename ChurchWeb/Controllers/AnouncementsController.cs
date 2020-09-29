using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChurchWeb.Models;
using Microsoft.AspNet.Identity;

namespace ChurchWeb.Controllers
{
    public class AnouncementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Anouncements
        public ActionResult Index()
        {
                return View(db.Anouncements.ToList());
        }
        [Authorize]
        public ActionResult AnouncementsV()
        {
            return View(db.Anouncements.ToList());
        }

        // GET: Anouncements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anouncement anouncement = db.Anouncements.Find(id);
            if (anouncement == null)
            {
                return HttpNotFound();
            }
            return View(anouncement);
        }

        // GET: Anouncements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anouncements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AnnouncementId,UserName,Title,Description,TimeMade")] Anouncement anouncement)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.GetUserName();
                if (User.IsInRole("Admin"))
                {
                    anouncement.UserName = "Admin@gmail.com";
                }
                else
                {
                    anouncement.UserName = userName;
                }
                anouncement.TimeMade = DateTime.Now;
                db.Anouncements.Add(anouncement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anouncement);
        }

        // GET: Anouncements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anouncement anouncement = db.Anouncements.Find(id);
            if (anouncement == null)
            {
                return HttpNotFound();
            }
            return View(anouncement);
        }

        // POST: Anouncements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnnouncementId,UserName,Title,Description,TimeMade")] Anouncement anouncement)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.GetUserName();
                if (User.IsInRole("Admin"))
                {
                    anouncement.UserName = "Admin@gmail.com";
                }
                else
                {
                    anouncement.UserName = userName;
                }
                anouncement.TimeMade = DateTime.Now;
                db.Entry(anouncement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anouncement);
        }

        // GET: Anouncements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anouncement anouncement = db.Anouncements.Find(id);
            if (anouncement == null)
            {
                return HttpNotFound();
            }
            return View(anouncement);
        }

        // POST: Anouncements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anouncement anouncement = db.Anouncements.Find(id);
            db.Anouncements.Remove(anouncement);
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
