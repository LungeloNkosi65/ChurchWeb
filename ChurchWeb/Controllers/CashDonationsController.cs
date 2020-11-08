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
    public class CashDonationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CashDonations
        public ActionResult Index()
        {
            if (!User.IsInRole("Admin"))
            {
                var userName = User.Identity.GetUserName();
                var cashDonations = db.CashDonations.Include(c => c.DonationType);
                return View(cashDonations.ToList().Where(x=>x.CaptureEmail==userName));
            }
            else
            {
                var cashDonations = db.CashDonations.Include(c => c.DonationType);
                return View(cashDonations.ToList());
            }
          
        }

        // GET: CashDonations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashDonation cashDonation = db.CashDonations.Find(id);
            if (cashDonation == null)
            {
                return HttpNotFound();
            }
            return View(cashDonation);
        }

        // GET: CashDonations/Create
        public ActionResult Create()
        {
            ViewBag.DonationTypeId = new SelectList(db.DonationTypes, "DonationTypeId", "TypeName");
            return View();
        }

        // POST: CashDonations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CashDonationId,DonationTypeId,CaptureEmail,DateCaptured,Amount")] CashDonation cashDonation)
        {
            if (ModelState.IsValid)
            {
                if (cashDonation.Amount > 0)
                {
                    var userName = User.Identity.GetUserName();
                    cashDonation.DateCaptured = DateTime.Now;
                    cashDonation.CaptureEmail = userName;
                    db.CashDonations.Add(cashDonation);
                    db.SaveChanges();
                    return RedirectToAction("OnceOff","Payment",new { amount = (double)cashDonation.Amount });
                }
                else
                {
                    ModelState.AddModelError("", "Invalid amount, please enter valid amount");
                    ViewBag.DonationTypeId = new SelectList(db.DonationTypes, "DonationTypeId", "TypeName", cashDonation.DonationTypeId);
                    return View(cashDonation);
                }
          
            }

            ViewBag.DonationTypeId = new SelectList(db.DonationTypes, "DonationTypeId", "TypeName", cashDonation.DonationTypeId);
            return View(cashDonation);
        }

        // GET: CashDonations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashDonation cashDonation = db.CashDonations.Find(id);
            if (cashDonation == null)
            {
                return HttpNotFound();
            }
            ViewBag.DonationTypeId = new SelectList(db.DonationTypes, "DonationTypeId", "TypeName", cashDonation.DonationTypeId);
            return View(cashDonation);
        }

        // POST: CashDonations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CashDonationId,DonationTypeId,CaptureEmail,DateCaptured,Amount")] CashDonation cashDonation)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.GetUserName();
                cashDonation.DateCaptured = DateTime.Now;
                cashDonation.CaptureEmail = userName;
                db.Entry(cashDonation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DonationTypeId = new SelectList(db.DonationTypes, "DonationTypeId", "TypeName", cashDonation.DonationTypeId);
            return View(cashDonation);
        }

        // GET: CashDonations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashDonation cashDonation = db.CashDonations.Find(id);
            if (cashDonation == null)
            {
                return HttpNotFound();
            }
            return View(cashDonation);
        }

        // POST: CashDonations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashDonation cashDonation = db.CashDonations.Find(id);
            db.CashDonations.Remove(cashDonation);
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
