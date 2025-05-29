using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.App_Start;
using WebBanSach.Models;
using WebBanSach.Repositories;
using WebBanSach.Services;
using WebBanSach.Utils;
using System.Data.Entity;


namespace WebBanSach.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ApplicationDbContext _context;
 

        public BookController()
        {
            _context = new ApplicationDbContext();
            var repo = new BookRepository(_context);
            _bookService = new BookService(repo);

        }

        public ActionResult Index()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }

        public ActionResult Detail(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            // Lấy danh sách giao dịch
            var transactions = _context.Orders
             .Where(o => o.OrderDetails.Any()) // bỏ đơn trống
             .Select(o => o.OrderDetails.Select(od => od.BookId).Distinct().ToList())
             .ToList();


            // Tìm tập phổ biến
            var frequentItemsets = Apriori.FindFrequentItemsets(transactions, 0.2);



            // 🔍 Log tập tập các giao dịch con
            System.Diagnostics.Debug.WriteLine("======= TRANSACTIONS =======");
            foreach (var t in transactions)
            {
                System.Diagnostics.Debug.WriteLine($"Giao dịch: [{string.Join(", ", t)}]");
            }


            // 🔍 Log tập phổ biến
            foreach (var itemset in frequentItemsets)
            {
                System.Diagnostics.Debug.WriteLine($"Tập phổ biến: [{string.Join(", ", itemset)}]");
            }

            // Lấy sách liên quan
            var relatedBookIds = Apriori.GetRelatedBookIds(id, frequentItemsets);

            // 🔍 Log ID sách liên quan
            System.Diagnostics.Debug.WriteLine($"Sách liên quan đến {id}: {string.Join(", ", relatedBookIds)}");

            var relatedBooks = _context.Books
                .Where(b => relatedBookIds.Contains(b.Id))
                .Take(5)
                .ToList();

            // 🔍 Log thông tin sách gợi ý
            foreach (var bookItem in relatedBooks)
            {
                System.Diagnostics.Debug.WriteLine($"Gợi ý sách: {bookItem.Id} - {bookItem.Title}");
            }

            var ratingBooks = _context.Ratings.Take(10).ToList();

            var model = new BookDetailViewModel
            {
                Book = book,
                RelatedBooks = relatedBooks,
                Ratings = ratingBooks
            };

            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.usermap = _context.UserMaps.Include(u => u.User).ToList();
            ViewBag.ratinglist = _context.Ratings.Where(x => x.ISBN == book.ISBN).ToList();

            return View(model);
        }


        [Authorize]
        [HttpPost]
        public ActionResult CommentBook(RatingRow rating)
        {
            if(ModelState.IsValid)
            {
                var ratingBook = new RatingRow
                {
                    BookRating = rating.BookRating,
                    UserID = rating.UserID,
                    ISBN = rating.ISBN,
                    DateCreated=DateTime.Now,
                    Comment= rating.Comment,
                };

                var usermap = new UserMap
                {
                    AppUserId = User.Identity.GetUserId(), 
                    CSVUserId = rating.UserID,
                };

                _context.UserMaps.Add(usermap);
                _context.Ratings.Add(ratingBook);
                _context.SaveChanges();

                var book = _context.Books.FirstOrDefault(x => x.ISBN == rating.ISBN);
                

                return RedirectToAction("Detail", "Book", new { id = book.Id});

            }
            return RedirectToAction("Index", "Home");
        }
    }
}