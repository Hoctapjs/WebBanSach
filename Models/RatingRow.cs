using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CsvHelper.Configuration.Attributes;

namespace WebBanSach.Models
{
    public class RatingRow
    {
        [Key]
        public int Id { get; set; }
        [Name("User-ID")]
        public string UserID { get; set; }

        public string ISBN { get; set; }

        [Name("Book-Rating")]
        public int BookRating { get; set; }

        public string Comment {  get; set; }
        public DateTime DateCreated { get; set; }
    }

}