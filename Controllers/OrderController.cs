using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private const string CartSession = "Cart";

        // Hiển thị form xác nhận
        [Authorize]
        public ActionResult Checkout()
        {
            var cart = Session[CartSession] as List<CartItem>;
            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            return View(cart);
        }

        // Xử lý sau khi người dùng xác nhận
        [HttpPost]
        public ActionResult CheckoutConfirm()
        {
            var cart = Session[CartSession] as List<CartItem>;
            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Cart");

            // Giả sử người dùng đã đăng nhập và có UserId = 2
            int userId = 2; // Trong thực tế, lấy từ session hoặc Identity

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var item in cart)
            {
                var detail = new OrderDetail
                {
                    BookId = item.BookId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                };
                order.OrderDetails.Add(detail);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            // Xoá giỏ hàng sau khi đặt hàng
            Session[CartSession] = null;

            return RedirectToAction("ThankYou");
        }

        [Authorize]
        public ActionResult ThankYou()
        {
            return View();
        }
    }
}