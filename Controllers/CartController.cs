using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "Cart";

        ApplicationDbContext db = new ApplicationDbContext();
        //[Authorize]
        public ActionResult Index()
        {
            var cart = Session[CartSession] as List<CartItem> ?? new List<CartItem>();
            return View(cart);
        }

        public ActionResult MuaNgay(int id)
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

        public JsonResult AddToCart(int id)
        {
            var book = db.Books.Find(id);
            if (book == null)
            {
                return Json(new { success = false, message = "Sách không tồn tại." }, JsonRequestBehavior.AllowGet);
            }

            // Thêm sách vào giỏ hàng
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

            // Tính tổng số lượng và tổng giá trị
            int totalQuantity = cart.Sum(c => c.Quantity);
            decimal totalPrice = cart.Sum(c => c.Price * c.Quantity);

            // Giả lập số lượng đã bán (có thể thay bằng dữ liệu thực tế từ database)
            int soldCount = book.Stock; // Ví dụ: "Đã bán 5"

            return Json(new
            {
                success = true,
                message = "Sản phẩm đã được thêm vào giỏ hàng.",
                totalQuantity = totalQuantity,
                totalPrice = totalPrice,
                soldCount = soldCount
            }, JsonRequestBehavior.AllowGet);
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

        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var cart = Session[CartSession] as List<CartItem>;
            var item = cart.FirstOrDefault(x => x.BookId== id);
            item.Quantity = quantity;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Clear()
        {
            Session[CartSession] = new List<CartItem>();
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = Session["Cart"] as List<CartItem>;
            int quantity = cart?.Sum(item => item.Quantity) ?? 0;
            ViewBag.TotalItems = quantity;

            return PartialView("_CartSummary");
        }
    }
}