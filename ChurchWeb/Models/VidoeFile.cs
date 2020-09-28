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
        [DisplayName("Vidoe Name"),Required]
        public string FileName { get; set; }
        [DisplayName("Content Type")]
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}