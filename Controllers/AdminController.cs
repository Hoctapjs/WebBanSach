using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBanSach.App_Start;
using WebBanSach.Models;
using WebBanSach.Services;


namespace WebBanSach.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {


        private readonly ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager)
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

        public ActionResult Books(int page = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                if (user != null && user.Role == "Admin")
                {
                    int pageSize = 100;
                    var totalBooks = db.Books.Count();
                    var totalPages = (int)Math.Ceiling((double)totalBooks / pageSize);
                    var books = db.Books.Include("Category").OrderBy(b => b.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    ViewBag.CurrentPage = page;
                    ViewBag.TotalPages = totalPages;
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

        public ActionResult PhieuNhaps()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (user != null && user.Role == "Admin")
                {
                    var list = db.PhieuNhaps.Include("NhaCungCap").ToList();
                    return View(list);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ChiTietPhieuNhap(int id)
        {
            var phieu = db.PhieuNhaps
                .Include("ChiTietPhieuNhaps.Book")
                .Include("NhaCungCap")
                .FirstOrDefault(p => p.Id == id);

            if (phieu == null)
            {
                return HttpNotFound();
            }

            return View(phieu);
        }


        //===============================================Quản lý user
        //Thêm
        public ActionResult Create_user()
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_user(ApplicationUser user)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        UserManager.Create(user);
                        return RedirectToAction("Users", "Admin");
                    }

                }
            }
            return RedirectToAction("Index", "Home");
        }

        //Sửa
        public ActionResult Edit_user(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    if (id == null) return HttpNotFound();
                    var user = UserManager.FindById(id);
                    if (user == null) return HttpNotFound();
                    return View(user);

                }

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_user(ApplicationUser user)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        var existingUser = UserManager.FindById(user.Id);
                        if (existingUser != null)
                        {
                            existingUser.UserName = user.UserName;
                            existingUser.FullName = user.FullName;
                            existingUser.Email = user.Email;
                            existingUser.Role = user.Role;
                            UserManager.Update(existingUser);
                            return RedirectToAction("Users", "Admin");
                        }
                    }
                }

            }
            return RedirectToAction("Index", "Home");


        }



        //Xóa
        public ActionResult Delete_user(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    if (id == null) return HttpNotFound();
                    var user = db.Users.FirstOrDefault(x => x.Id == id);
                    return View(user);

                }

            }
            return RedirectToAction("Index", "Home");

        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete_user_confirmed(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    var user = db.Users.Find(id);
                    if (user != null)
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Users", "Admin");

                }

            }
            return RedirectToAction("Index", "Home");

        }



        //===============================================Quản lý sách

        //Thêm sách
        public ActionResult Create_book()
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_book(Book book, HttpPostedFileBase imageFile)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        // Xử lý upload ảnh nếu có file được chọn
                        if (imageFile != null && imageFile.ContentLength > 0)
                        {
                            // Đảm bảo file là ảnh (kiểm tra phần mở rộng)
                            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                            var extension = Path.GetExtension(imageFile.FileName).ToLower();
                            if (!allowedExtensions.Contains(extension))
                            {
                                ModelState.AddModelError("imageFile", "Vui lòng chọn file ảnh có định dạng .jpg, .jpeg, .png hoặc .gif.");
                                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", book.CategoryId);
                                return View(book);
                            }

                            // Đường dẫn thư mục lưu ảnh trong dự án: Asset/images
                            var uploadDir = Server.MapPath("~/Asset/images");
                            if (!Directory.Exists(uploadDir))
                            {
                                Directory.CreateDirectory(uploadDir); // Tạo thư mục nếu chưa tồn tại
                            }

                            // Tạo tên file duy nhất để tránh trùng lặp
                            var fileName = Guid.NewGuid().ToString() + extension;
                            var path = Path.Combine(uploadDir, fileName);

                            // Lưu file vào thư mục Asset/images
                            imageFile.SaveAs(path);

                            // Cập nhật đường dẫn ImageUrl
                            book.ImageUrl = "/Asset/images/" + fileName;
                        }
                        else
                        {
                            // Nếu không có file ảnh, có thể đặt ImageUrl mặc định hoặc để trống
                            book.ImageUrl = null; // Hoặc một giá trị mặc định
                        }

                        // Thêm sách vào cơ sở dữ liệu
                        db.Books.Add(book);
                        db.SaveChanges();

                        return RedirectToAction("Books", "Admin");
                    }

                    // Nếu ModelState không hợp lệ, trả về view với dữ liệu hiện tại
                    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", book.CategoryId);
                    return View(book);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        //Sửa sách
        public ActionResult Edit_book(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {

                    var book = db.Books.FirstOrDefault(x => x.Id == id);

                    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", book.CategoryId);
                    return View(book);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_book(Book book, HttpPostedFileBase imageFile)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        var existingBook = db.Books.Find(book.Id);
                        if (existingBook != null)
                        {
                            // Cập nhật các trường thông tin sách
                            existingBook.Title = book.Title;
                            existingBook.Description = book.Description;
                            existingBook.Author = book.Author;
                            existingBook.Price = book.Price;
                            existingBook.CategoryId = book.CategoryId;

                            // Xử lý upload ảnh nếu có file được chọn
                            if (imageFile != null && imageFile.ContentLength > 0)
                            {
                                // Đảm bảo file là ảnh (kiểm tra phần mở rộng)
                                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                                var extension = Path.GetExtension(imageFile.FileName).ToLower();
                                if (!allowedExtensions.Contains(extension))
                                {
                                    ModelState.AddModelError("imageFile", "Vui lòng chọn file ảnh có định dạng .jpg, .jpeg, .png hoặc .gif.");
                                    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", book.CategoryId);
                                    return View(book);
                                }

                                // Đường dẫn thư mục lưu ảnh trong dự án: Asset/images
                                var uploadDir = Server.MapPath("~/Asset/images");
                                if (!Directory.Exists(uploadDir))
                                {
                                    Directory.CreateDirectory(uploadDir); // Tạo thư mục nếu chưa tồn tại
                                }

                                // Tạo tên file duy nhất để tránh trùng lặp
                                var fileName = Guid.NewGuid().ToString() + extension;
                                var path = Path.Combine(uploadDir, fileName);

                                // Lưu file vào thư mục Asset/images
                                imageFile.SaveAs(path);

                                // Xóa ảnh cũ nếu có
                                if (!string.IsNullOrEmpty(existingBook.ImageUrl))
                                {
                                    var oldImagePath = Server.MapPath(existingBook.ImageUrl);
                                    if (System.IO.File.Exists(oldImagePath))
                                    {
                                        System.IO.File.Delete(oldImagePath);
                                    }
                                }

                                // Cập nhật đường dẫn ImageUrl
                                existingBook.ImageUrl = "/Asset/images/" + fileName;
                            }

                            // Lưu thay đổi vào cơ sở dữ liệu
                            db.SaveChanges();

                            return RedirectToAction("Books", "Admin");
                        }
                        else
                        {

                            return RedirectToAction("Books", "Admin");
                        }
                    }

                    // Nếu ModelState không hợp lệ, trả về view với dữ liệu hiện tại
                    ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", book.CategoryId);
                    return View(book);
                }
            }
            TempData["ErrorMessage"] = "Bạn không có quyền thực hiện hành động này.";
            return RedirectToAction("Index", "Home");
        }
        //Xóa sách

        public ActionResult Delete_book(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurren = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurren != null && usercurren.Role == "Admin")
                {
                    var book = db.Books.Find(id);
                    if (book == null) return HttpNotFound();
                    return View(book);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("Delete_book")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_book_confirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurren = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurren != null && usercurren.Role == "Admin")
                {
                    // Kiểm tra xem sách có đang được sử dụng trong đơn hàng không
                    var orderdetails = db.OrderDetails.Any(o => o.BookId == id);
                    if (orderdetails)
                    {
                        TempData["ErrorMessage"] = "Không thể xóa sách vì sách đang được sử dụng trong đơn hàng.";
                        return RedirectToAction("Delete_book", "Admin");
                    }

                    // Nếu không có đơn hàng liên quan, tiến hành xóa sách
                    var existingBook = db.Books.Find(id);
                    if (existingBook != null)
                    {
                        try
                        {
                            db.Books.Remove(existingBook);
                            db.SaveChanges();

                        }
                        catch (Exception ex)
                        {
                            TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa sách: " + ex.Message;
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy sách để xóa.";
                    }
                    return RedirectToAction("Books", "Admin");
                }
            }
            TempData["ErrorMessage"] = "Bạn không có quyền thực hiện hành động này.";
            return RedirectToAction("Index", "Home");
        }

        //===============================================Quản lý đơn hàng
        //Thêm
        public ActionResult Create_order()
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    return View();

                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_order(Order order)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        db.Orders.Add(order);
                        db.SaveChanges();
                        return RedirectToAction("Orders", "Admin");
                    }

                }
            }
            return RedirectToAction("Index", "Home");
        }

        //Sửa

        public ActionResult Edit_order(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    var order = db.Orders.FirstOrDefault(x => x.Id == id);
                    if (order != null)
                    {
                        return View(order);
                    }
                    return HttpNotFound();


                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_order(Order order)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        var existingorder = db.Orders.Find(order.Id);
                        if (existingorder != null)
                        {
                            existingorder.UserId = order.UserId;
                            existingorder.OrderDate = order.OrderDate;
                            existingorder.Status = order.Status;

                        }
                        db.SaveChanges();
                        return RedirectToAction("Orders", "Admin");
                    }

                }
            }
            return RedirectToAction("Index", "Home");

        }




        //Xóa
        public ActionResult Delete_order(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    var order = db.Orders.FirstOrDefault(x => x.Id == id);
                    if (order != null)
                    {
                        return View(order);
                    }
                    return HttpNotFound();


                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost, ActionName("Delete_order")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_order_confirned(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usercurrent = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (usercurrent != null && usercurrent.Role == "Admin")
                {
                    var order = db.Orders.FirstOrDefault(x => x.Id == id);
                    if (order != null)
                    {
                        db.Orders.Remove(order);
                        db.SaveChanges();
                        return RedirectToAction("Orders","Admin");
                    }
                    return HttpNotFound();


                }
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult ConfirmOrder(int id)
        {
            var order = db.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null) { return HttpNotFound(); }

            order.Status = "1";
            db.SaveChanges();
            return RedirectToAction("Orders");
        }
        // tính năng ko mở lên chạy

        //public ActionResult SeedCsvUsers()
        //{
        //    var path = Server.MapPath("~/App_Data/Ratings.csv");
        //    var seeder = new CsvUserSeedService();
        //    seeder.SeedUsersFromCsv(path);

        //    return Content("Tạo user và ánh xạ thành công!");
        //}

        //===============================================Quản lý phiếu nhập và nhà cung cấp
        // =========================== Thêm phiếu nhập
        public ActionResult CreatePhieuNhap()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (user != null && user.Role == "Admin")
                {
                    var phieuNhap = new PhieuNhap
                    {
                        NgayNhap = DateTime.Now
                    };
                    ViewBag.NhaCungCapId = new SelectList(db.NhaCungCaps, "Id", "TenNhaCungCap");
                    return View(phieuNhap);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePhieuNhap(PhieuNhap phieuNhap)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (user != null && user.Role == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        db.PhieuNhaps.Add(phieuNhap);
                        db.SaveChanges();
                        return RedirectToAction("AddChiTietPhieuNhap", new { phieuNhapId = phieuNhap.Id });
                    }
                    ViewBag.NhaCungCapId = new SelectList(db.NhaCungCaps, "Id", "TenNhaCungCap", phieuNhap.NhaCungCapId);
                    return View(phieuNhap);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // =========================== Sửa phiếu nhập
        public ActionResult EditPhieuNhap(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (user != null && user.Role == "Admin")
                {
                    var phieuNhap = db.PhieuNhaps.Find(id);
                    if (phieuNhap == null) return HttpNotFound();
                    ViewBag.NhaCungCapId = new SelectList(db.NhaCungCaps, "Id", "TenNhaCungCap", phieuNhap.NhaCungCapId);
                    return View(phieuNhap);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPhieuNhap(PhieuNhap phieuNhap)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (user != null && user.Role == "Admin")
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(phieuNhap).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("PhieuNhaps");
                    }
                    ViewBag.NhaCungCapId = new SelectList(db.NhaCungCaps, "Id", "TenNhaCungCap", phieuNhap.NhaCungCapId);
                    return View(phieuNhap);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // =========================== Xóa phiếu nhập
        public ActionResult DeletePhieuNhap(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (user != null && user.Role == "Admin")
                {
                    var phieuNhap = db.PhieuNhaps.Find(id);
                    if (phieuNhap == null) return HttpNotFound();
                    return View(phieuNhap);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("DeletePhieuNhap")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePhieuNhapConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (user != null && user.Role == "Admin")
                {
                    var phieuNhap = db.PhieuNhaps.Find(id);
                    if (phieuNhap == null) return HttpNotFound();
                    db.PhieuNhaps.Remove(phieuNhap);
                    db.SaveChanges();
                    return RedirectToAction("PhieuNhaps");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // =========================== Thêm chi tiết ngay sau khi tạo phiếu nhập
        public JsonResult SearchBooks(string term)
        {
            var matchedBooks = db.Books
                .Where(b => b.Title.Contains(term))
                .Select(b => new { b.Id, b.Title })
                .Take(20)
                .ToList();

            return Json(matchedBooks, JsonRequestBehavior.AllowGet);
        }

        // GET
        public ActionResult AddChiTietPhieuNhap(int phieuNhapId)
        {
            ViewBag.PhieuNhapId = phieuNhapId;
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddChiTietPhieuNhap(ChiTietPhieuNhap ct)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietPhieuNhaps.Add(ct);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Đã thêm chi tiết cho phiếu nhập.";
                return RedirectToAction("AddChiTietPhieuNhap", new { phieuNhapId = ct.PhieuNhapId });
            }

            ViewBag.PhieuNhapId = ct.PhieuNhapId;
            return View(ct);
        }



        // =========================== Danh sách nhà cung cấp
        public ActionResult NhaCungCaps()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
                if (user != null && user.Role == "Admin")
                {
                    var list = db.NhaCungCaps.ToList();
                    return View(list);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // =========================== Thêm nhà cung cấp
        public ActionResult CreateNhaCungCap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNhaCungCap(NhaCungCap ncc)
        {
            if (ModelState.IsValid)
            {
                db.NhaCungCaps.Add(ncc);
                db.SaveChanges();
                return RedirectToAction("NhaCungCaps");
            }
            return View(ncc);
        }

        // =========================== Sửa nhà cung cấp
        public ActionResult EditNhaCungCap(int id)
        {
            var ncc = db.NhaCungCaps.Find(id);
            if (ncc == null) return HttpNotFound();
            return View(ncc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNhaCungCap(NhaCungCap ncc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ncc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("NhaCungCaps");
            }
            return View(ncc);
        }

        // =========================== Xóa nhà cung cấp
        public ActionResult DeleteNhaCungCap(int id)
        {
            var ncc = db.NhaCungCaps.Find(id);
            if (ncc == null) return HttpNotFound();
            return View(ncc);
        }

        [HttpPost, ActionName("DeleteNhaCungCap")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNhaCungCapConfirmed(int id)
        {
            var ncc = db.NhaCungCaps.Find(id);
            if (ncc == null) return HttpNotFound();
            db.NhaCungCaps.Remove(ncc);
            db.SaveChanges();
            return RedirectToAction("NhaCungCaps");
        }
    }
}