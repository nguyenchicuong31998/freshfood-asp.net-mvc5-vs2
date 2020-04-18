using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện củ.")]
        [StringLength(40, ErrorMessage = "Mật khẩu phải từ 6 đến 32 ký tự.", MinimumLength = 6)]
        public string password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới.")]
        [StringLength(40, ErrorMessage = "Mật khẩu phải từ 6 đến 32 ký tự", MinimumLength = 6)]
        public string new_password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập xác nhận mật khẩu mới.")]
        [StringLength(40, ErrorMessage = "Mật khẩu phải từ 6 đến 32 ký tự.", MinimumLength = 6)]
        [Compare("new_password", ErrorMessage = "Mật khẩu mới và mật khẩu xác nhận không khớp.")]
        public string confirm_new_password { get; set; }
    }
}