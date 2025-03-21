using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext() : base("BookStoreConnection") { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}