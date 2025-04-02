using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class BookApiDbContext : DbContext
    {
        public BookApiDbContext() : base("BookStoreConnection") { }

        public DbSet<Book> Books { get; set; }
    }
}