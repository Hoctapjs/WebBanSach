using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var categories = db.Categories.ToList(); // phần categories
            var featuredBooks = db.Books.Take(4).ToList(); // phần Featured

            var popularBooks = db.Books.OrderByDescending(b => b.Stock).Take(8).ToList(); // phần Popular
            var specialBooks = db.Books.Where(b => b.Price < 40).Take(8).ToList(); // phần Offer

            var model = new HomeViewModel
            {
                FeaturedBooks = featuredBooks,
                PopularBooks = popularBooks,
                SpecialOfferBooks = specialBooks,
                Categories = categories
            };

            return View(model);
        }
    }
}