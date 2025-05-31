using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class BookDetailViewModel
    {
        public Book Book { get; set; }
        public List<Book> RelatedBooks { get; set; }
        public List<RatingRow> Ratings { get; set; }
    }
}