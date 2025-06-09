using System.Collections.Generic;
using WebBanSach.Models;



namespace WebBanSach.Models
{
    public class ChiTietPhieuNhap
    {
        public int Id { get; set; }

        // FK đến PhieuNhap
        public int PhieuNhapId { get; set; }
        public virtual PhieuNhap PhieuNhap { get; set; }

        // FK đến Book
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int SoLuong { get; set; }
        public decimal DonGiaNhap { get; set; }
    }

}

