using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebBanSach.Models;

namespace WebBanSach.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.Include("Category").ToList();
        }

        public Book GetById(int id)
        {
            return _context.Books.Find(id);
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
        }

        public void Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
                _context.Books.Remove(book);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}