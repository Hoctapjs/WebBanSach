using System.Collections.Generic;
using System;
using WebBanSach.Models;



namespace WebBanSach.Models
{
    public class PhieuNhap
    {
        public int Id { get; set; }
        public DateTime NgayNhap { get; set; }

        // FK đến NhaCungCap
        public int NhaCungCapId { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }

        // Một phiếu nhập có nhiều chi tiết
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
    }

}



