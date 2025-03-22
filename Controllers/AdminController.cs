using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                if (user != null && user.Role == "Admin")
                {
                    // Lấy số lượng sách
                    ViewBag.TotalBooks = db.Books.Count();

                    // Lấy số lượng người dùng (trừ admin nếu cần)
                    ViewBag.TotalUsers = db.Users.Count();

                    // Lấy số lượng đơn hàng
                    ViewBag.TotalOrders = db.Orders.Count();

                    return View(); // Cho truy cập
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Books()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                if (user != null && user.Role == "Admin")
                {
                    var books = db.Books.ToList();
                    return View(books);
                }
            }
            return RedirectToAction("Index", "Home");

            
        }

        public ActionResult Users()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                if (user != null && user.Role == "Admin")
                {
                    var users = db.Users.ToList();
                    return View(users);
                }
            }
            return RedirectToAction("Index", "Home");

            
        }

        public ActionResult Orders()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                if (user != null && user.Role == "Admin")
                {
                    var orders = db.Orders.ToList();
                    return View(orders);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}