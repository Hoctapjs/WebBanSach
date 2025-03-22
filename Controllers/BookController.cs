using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;
using WebBanSach.Repositories;
using WebBanSach.Services;

namespace WebBanSach.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController()
        {
            var context = new ApplicationDbContext();
            var repo = new BookRepository(context);
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
            return View(book);
        }
    }
}