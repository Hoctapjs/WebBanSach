using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;
using WebBanSach.Repositories;
using WebBanSach.Services;
using WebBanSach.Utils;

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

            // Lấy dữ liệu giao dịch từ OrderDetails
            var transactions = _context.Orders
                .Select(o => o.OrderDetails.Select(od => od.BookId).ToList())
                .ToList();

            // Tìm các tập hợp thường xuyên với minSupport = 0.1 (10%)
            var frequentItemsets = Apriori.FindFrequentItemsets(transactions, 0.1);

            // Lấy danh sách BookId liên quan
            var relatedBookIds = Apriori.GetRelatedBookIds(id, frequentItemsets);

            // Lấy danh sách sách liên quan từ database
            var relatedBooks = _context.Books
                .Where(b => relatedBookIds.Contains(b.Id))
                .Take(5)
                .ToList();

            // Nếu không có sách liên quan từ Apriori, gợi ý dựa trên cùng thể loại
            if (!relatedBooks.Any())
            {
                relatedBooks = _context.Books
                    .Where(b => b.CategoryId == book.CategoryId && b.Id != book.Id)
                    .Take(5)
                    .ToList();
            }

            var model = new BookDetailViewModel
            {
                Book = book,
                RelatedBooks = relatedBooks
            };

            return View(model);
        }
    }
}