using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanSach.Models;
using WebBanSach.Repositories;

namespace WebBanSach.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _repository.GetAll();
        }

        public Book GetBookById(int id)
        {
            return _repository.GetById(id);
        }
    }
}