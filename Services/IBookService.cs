using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSach.Models;

namespace WebBanSach.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
    }
}
