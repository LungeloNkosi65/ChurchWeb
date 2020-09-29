using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchWeb.Models
{
    public class DonationType
    {
        [Key]
        public int DonationTypeId { get; set; }
        [DisplayName("Donation Type"),Required]
        public string TypeName { get; set; }
    }
}