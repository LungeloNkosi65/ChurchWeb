using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChurchWeb.Models
{
    public class ChurchLogic
    {
        private static readonly ApplicationDbContext db = new ApplicationDbContext();

        public static void LIkeVideo(int? videoFileId)
        {
            var dbRecord = db.VidoeFiles.Find(videoFileId);
            dbRecord.LIkes++;
            db.Entry(dbRecord).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}