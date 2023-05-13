using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteDienNghien.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Thiếu tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Thiếu mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}