using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public string CartId { get; set; } // Có thể là session id hoặc user id
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public virtual Book Book { get; set; }
    }

}