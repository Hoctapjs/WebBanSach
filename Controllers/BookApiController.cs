using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebBanSach.Models;


namespace WebBanSach.Controllers
{
    [RoutePrefix("api/book")]
    public class BookApiController : ApiController
    {
        private BookApiDbContext db = new BookApiDbContext();

        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllBooks()
        {
            var books = db.Books.ToList();
            return Ok(books);
        }

        // GET: Book
        //[HttpGet]
        //[Route("bestseller")]
        //public IHttpActionResult GetBestsellerBooks()
        //{
        //    var books = db.Books.Where(b => b.IsBestSeller).Select(b => new { b.Id, b.Title, b.ImageUrl, b.Price }).ToList();
        //    return Ok(books); // Web API tự chuyển sang JSON
        //}

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetBookById(int id)
        {
            var book = db.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult SearchBooks(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Vui lòng nhập từ khóa tìm kiếm.");
            }

            var q = query.ToLower();

            var results = db.Books.Where(b => b.Title.ToLower().Contains(query) || b.Author.ToLower().Contains(query)).Select(b => new
            {
                b.Id,
                b.Title,
                b.Author,
                b.Price,
                b.ImageUrl
            }).ToList();

            return Ok(results);
        }

        [HttpGet]
        [Route("category/{categoryId:int}")]
        public IHttpActionResult GetBooksByCategory(int categoryId)
        {
            var books = db.Books.Where(b => b.CategoryId == categoryId).Select(b=> new
            {
                b.Id, b.Title, b.Author, b.Price, b.ImageUrl, b.CategoryId
            }).ToList();

            if (books.Count == 0)
            {
                return NotFound();
            }

            return Ok(books);
        }
    }
}