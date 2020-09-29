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
    public class DonationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Donations

        [Authorize]
        public ActionResult Index()
        {
            var userName = User.Identity.GetUserName();
            if (User.IsInRole("Member"))
            {
                var donations = db.Donations.Include(d => d.DonationType);
                return View(donations.ToList().Where(x=>x.UserName==userName));
            }
            else
            {
                var donations = db.Donations.Include(d => d.DonationType);
                return View(donations.ToList());
            }
           
        }

        public ActionResult Delivered(int? id)
        {
            Donation.ChangeStatus(id, "Delivered");
            return RedirectToAction("Index");
        }


        // GET: Donations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // GET: Donations/Create
        public ActionResult Create()
        {
            ViewBag.DonationTypeId = new SelectList(db.DonationTypes, "DonationTypeId", "TypeName");
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DonationId,DonationTypeId,UserName,DateDonated,DropInDate,Status")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.GetUserName();
                if (!User.IsInRole("Admin"))
                {
                    donation.UserName = userName;
                }
                donation.Status = "Awaiting Delivery";
                donation.DateDonated = DateTime.Now;
                db.Donations.Add(donation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DonationTypeId = new SelectList(db.DonationTypes, "DonationTypeId", "TypeName", donation.DonationTypeId);
            return View(donation);
        }

        // GET: Donations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            ViewBag.DonationTypeId = new SelectList(db.DonationTypes, "DonationTypeId", "TypeName", donation.DonationTypeId);
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonationId,DonationTypeId,UserName,DateDonated,DropInDate,Status")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.GetUserName();
                if (!User.IsInRole("Admin"))
                {
                    donation.UserName = userName;
                }
                donation.Status = donation.Status;
                donation.DateDonated = DateTime.Now;
                db.Entry(donation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DonationTypeId = new SelectList(db.DonationTypes, "DonationTypeId", "TypeName", donation.DonationTypeId);
            return View(donation);
        }

        // GET: Donations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = db.Donations.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donation donation = db.Donations.Find(id);
            db.Donations.Remove(donation);
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
