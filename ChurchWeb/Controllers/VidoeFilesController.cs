using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ChurchWeb.Models;
using Microsoft.AspNet.Identity;

namespace ChurchWeb.Controllers
{
    public class VidoeFilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VidoeFiles
        public ActionResult Index()
        {
            var userName = User.Identity.GetUserName();
            if (!User.IsInRole("Member"))
            {
                return View(db.VidoeFiles.ToList());
            }
            else
            {
                return View(db.VidoeFiles.ToList().Where(x=>x.UserName==userName));
            }
            
        }

        public ActionResult UploaderView()
        {
            var userName = User.Identity.GetUserName();
            if (!User.IsInRole("Member"))
            {
                return View(db.VidoeFiles.ToList());
            }
            else
            {
                return View(db.VidoeFiles.ToList().Where(x => x.UserName == userName));
            }
        }

        [Authorize]
        public ActionResult LIkeVideo(int? id)
        {
            ChurchLogic.LIkeVideo(id);
            return RedirectToAction("Index");
        }

        // GET: VidoeFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VidoeFile vidoeFile = db.VidoeFiles.Find(id);
            if (vidoeFile == null)
            {
                return HttpNotFound();
            }
            return View(vidoeFile);
        }

        // GET: VidoeFiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VidoeFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VideoId,UserName,FileName,ContentType,Data,LIkes,SmallDescription")] VidoeFile vidoeFile, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.GetUserName();
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }
                vidoeFile.UserName = userName;
                vidoeFile.FileName = Path.GetFileName(postedFile.FileName);
                vidoeFile.ContentType = postedFile.ContentType;
                vidoeFile.Data = bytes;
                vidoeFile.LIkes = 0;
                vidoeFile.TimeULoaded = DateTime.Now;
                db.VidoeFiles.Add(vidoeFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vidoeFile);
        }
        [HttpGet]
        public FileResult DownloadFile(int? fileId)
        {
            var videoFile = db.VidoeFiles.Find(fileId);
            return File(videoFile.Data, videoFile.ContentType, videoFile.FileName);
        }

        // GET: VidoeFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VidoeFile vidoeFile = db.VidoeFiles.Find(id);
            if (vidoeFile == null)
            {
                return HttpNotFound();
            }
            return View(vidoeFile);
        }

        // POST: VidoeFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VideoId,UserName,FileName,ContentType,Data,LIkes,SmallDescription")] VidoeFile vidoeFile, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.GetUserName();
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }
                vidoeFile.UserName = userName;
                vidoeFile.FileName = Path.GetFileName(postedFile.FileName);
                vidoeFile.ContentType = postedFile.ContentType;
                vidoeFile.Data = bytes;
                db.Entry(vidoeFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vidoeFile);
        }

        // GET: VidoeFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VidoeFile vidoeFile = db.VidoeFiles.Find(id);
            if (vidoeFile == null)
            {
                return HttpNotFound();
            }
            return View(vidoeFile);
        }

        // POST: VidoeFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VidoeFile vidoeFile = db.VidoeFiles.Find(id);
            db.VidoeFiles.Remove(vidoeFile);
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
