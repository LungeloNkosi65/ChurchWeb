using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchWeb.Models
{
    public class Anouncement
    {
        [Key]
        public int AnnouncementId { get; set; }
        [DisplayName("Posted By")]
        public string UserName { get; set; }
        [DisplayName("Title"),Required]
        public string Title { get; set; }
        [ Required]
        public string Description { get; set; }
        [DisplayName("Time Stamp"),DataType(DataType.DateTime)]
        public DateTime TimeMade { get; set; }
    }
}