using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchWeb.Models
{
    public class Members
    {
        [Key]
        public string MemberId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Gender { get; set; }
        [DisplayName("Member Email")]
        public string MemberEmail { get; set; }
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }
        public string Address { get; set; }

    }
}