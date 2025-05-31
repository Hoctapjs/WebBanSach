using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class UserMap
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string AppUserId { get; set; }  // GUID của AspNetUsers
        public string CSVUserId { get; set; }     // User-ID từ Ratings.csv

        public ApplicationUser User { get; set; }

    }

}