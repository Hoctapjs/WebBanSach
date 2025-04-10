using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebBanSach.App_Start;
using WebBanSach.Models;

namespace WebBanSach.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(model.Username))
            {
                TempData["RegisterMessage"] = "Vui lòng nhập tên đăng nhập.";
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Email))
            {
                TempData["RegisterMessage"] = "Vui lòng nhập email.";
                return View(model);
            }

            // Kiểm tra định dạng email
            if (!new EmailAddressAttribute().IsValid(model.Email))
            {
                TempData["RegisterMessage"] = "Email không đúng định dạng.";
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                TempData["RegisterMessage"] = "Vui lòng nhập mật khẩu.";
                return View(model);
            }

            // Kiểm tra độ dài mật khẩu (tương tự Data Annotations trước đây)
            if (model.Password.Length < 6)
            {
                TempData["RegisterMessage"] = "Mật khẩu phải từ 6 ký tự trở lên.";
                return View(model);
            }

            // Tạo user mới
            var user = new ApplicationUser { UserName = model.Username, FullName = model.Fullname, Email = model.Email, Role = "User" };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            // Nếu có lỗi từ Identity (như username hoặc email đã tồn tại)
            TempData["RegisterMessage"] = result.Errors.FirstOrDefault() ?? "Đã có lỗi xảy ra khi đăng ký.";
            return View(model);
        }

        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                TempData["LoginMessage"] = "Vui lòng nhập đầy đủ thông tin.";
                return View(model);
            }

            // Kiểm tra tồn tại username    
            var user = await UserManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                TempData["LoginMessage"] = "Tên đăng nhập không tồn tại.";
                return View(model);
            }

            // Kiểm tra mật khẩu
            var isPasswordValid = await UserManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordValid)
            {
                TempData["LoginMessage"] = "Mật khẩu không đúng.";
                return View(model);
            }

            // Đăng nhập
            await SignInAsync(user, isPersistent: false);
            TempData["LoginMessage"] = "Đăng nhập thành công.";

            if (user.Role == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            HttpContext.GetOwinContext().Authentication.SignIn(
                new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = isPersistent },
                identity);
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

       
    }
}