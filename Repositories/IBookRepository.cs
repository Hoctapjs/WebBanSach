using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanSach.Models;

namespace WebBanSach.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetById(int id);
        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
        void Save();
    }
}
