using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchWeb.Models
{
    public class Donation
    {
        [Key]
        public int DonationId { get; set; }
        public int DonationTypeId { get; set; }
        public virtual DonationType DonationType { get; set; }
        [DisplayName("Donator Name")]
        public string UserName { get; set; }
        [DisplayName("Donation Date"),DataType(DataType.DateTime)]
        public DateTime DateDonated { get; set; }
        [DisplayName("Delivery Date"), DataType(DataType.Date)]
        public DateTime DropInDate { get; set; }
        public string Status { get; set; }

        private static readonly ApplicationDbContext db = new ApplicationDbContext();
        public  static void ChangeStatus(int? id, string status)
        {
            var dbrecord = db.Donations.Find(id);
            dbrecord.Status = status;
            db.Entry(dbrecord).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}