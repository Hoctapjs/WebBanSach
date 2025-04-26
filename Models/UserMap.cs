using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class UserMap
    {
        [Key]
        public string AppUserId { get; set; }  // GUID của AspNetUsers
        public int CSVUserId { get; set; }     // User-ID từ Ratings.csv
    }

}