using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChurchWeb.Models
{
    public class VidoeFile
    {
        [Key]
        public int VideoId { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [DisplayName("Vidoe Name")]
        public string FileName { get; set; }
        [DisplayName("Content Type")]
        public string ContentType { get; set; }
        [DisplayName("Select File")]
        public byte[] Data { get; set; }
        public int LIkes { get; set; }
        [DisplayName("Small Description")]
        public string SmallDescription { get; set; }
        [DisplayName("Time Uploaded")]
        public DateTime TimeULoaded { get; set; }
    }
}