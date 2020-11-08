using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchWeb.Models
{
    public class CashDonation
    {
        [Key]
        public int CashDonationId { get; set; }
        public int DonationTypeId { get; set; }
        public virtual DonationType DonationType { get; set; }
        [DisplayName("Captured By")]
        public string CaptureEmail { get; set; }
        [DisplayName("Date Captured")]
        public DateTime DateCaptured { get; set; }
        [DisplayName("Amount"),DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}