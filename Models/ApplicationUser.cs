using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace WebBanSach.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Đây là nơi bạn có thể mở rộng thêm thông tin người dùng
        [Required]
        public string FullName { get; set; }

        public string Address { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Role { get; set; }
    }
}