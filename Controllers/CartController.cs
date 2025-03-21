using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "Cart";

        private BookStoreDbContext db = new BookStoreDbContext();

        public ActionResult Index()
        {
            var cart = Session[CartSession] as List<CartItem> ?? new List<CartItem>();
            return View(cart);
        }

        public ActionResult AddToCart(int id)
        {
            var book = db.Books.Find(id);
            if (book == null) return HttpNotFound();

            var cart = Session[CartSession] as List<CartItem> ?? new List<CartItem>();

            var item = cart.FirstOrDefault(c => c.BookId == id);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    BookId = book.Id,
                    Title = book.Title,
                    Price = book.Price,
                    Quantity = 1,
                    ImageUrl = book.ImageUrl
                });
            }

            Session[CartSession] = cart;
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            var cart = Session[CartSession] as List<CartItem>;
            if (cart != null)
            {
                var item = cart.FirstOrDefault(c => c.BookId == id);
                if (item != null) cart.Remove(item);
                Session[CartSession] = cart;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Clear()
        {
            Session[CartSession] = new List<CartItem>();
            return RedirectToAction("Index");
        }
    }
}