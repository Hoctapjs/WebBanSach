using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CsvHelper.Configuration.Attributes;

namespace WebBanSach.Models
{
    public class RatingRow
    {
        [Name("User-ID")]
        public int UserID { get; set; }

        public string ISBN { get; set; }

        [Name("Book-Rating")]
        public int BookRating { get; set; }
    }

}