using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class HomeViewModel
    {
        public List<Book> FeaturedBooks { get; set; }
        public List<Book> PopularBooks { get; set; }
        public List<Book> SpecialOfferBooks { get; set; }
        public List<Category> Categories { get; set; }
    }
}