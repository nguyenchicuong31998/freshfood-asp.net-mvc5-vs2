using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class RegisterModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string display_name { get; set; }
        public Nullable<bool> gender { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        public Nullable<System.DateTime> date_of_birth { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(40)]
        public string email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(40, ErrorMessage = "Mật khẩu phải từ 6 đến 32 ký tự", MinimumLength = 6)]
        public string password { get; set; }
        public Nullable<bool> status { get; set; }
    }
}