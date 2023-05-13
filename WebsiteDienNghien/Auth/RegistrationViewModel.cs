using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteDienNghien.Auth
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Bạn cần nhập tên")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập họ và tên lót")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên người dùng")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [MinLength(9, ErrorMessage = "Mật khẩu cần ít nhất 9 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bạn cần xác nhận mật khẩu")]
        [MinLength(9, ErrorMessage = "Mật khẩu cần ít nhất 9 ký tụ")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không đúng")]
        public string ConfirmPassword { get; set; }

    }
}