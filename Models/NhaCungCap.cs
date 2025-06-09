using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class NhaCungCap
    {
        public int Id { get; set; }
        public string TenNhaCungCap { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }

        // Một nhà cung cấp có nhiều phiếu nhập
        public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; }
    }
}