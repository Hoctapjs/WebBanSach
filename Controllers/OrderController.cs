using Microsoft.AspNet.Identity;
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

            var userASPId = User.Identity.GetUserId();
            var userMap= db.UserMaps.FirstOrDefault(x => x.AppUserId == userASPId);


            // Giả sử người dùng đã đăng nhập và có UserId = 2
            int userId = int.Parse(userMap.CSVUserId); // Trong thực tế, lấy từ session hoặc Identity

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "0",
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

        [Authorize]
        public ActionResult History()
        {
            var userASPId = User.Identity.GetUserId();
            var userMap = db.UserMaps.FirstOrDefault(x => x.AppUserId == userASPId);
            var userCSVId= int.Parse(userMap.CSVUserId);
            var list = db.Orders.Where(x => x.UserId == userCSVId);
            return View(list);
        }

        [HttpPost]
        public JsonResult CancelOrder(int id)
        {
            var order = db.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return Json(new { success = false, message = "Đơn hàng không tồn tại." });
            }

            if (order.Status == "1")
            {
                return Json(new { success = false, message = "Đơn hàng đã được xử lý, không thể hủy." });
            }

            order.Status = "2";
            db.SaveChanges();

            return Json(new { success = true, message = "Đã hủy đơn hàng thành công." });
        }


    }
}