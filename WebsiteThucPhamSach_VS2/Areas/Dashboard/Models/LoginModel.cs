using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {

        public int id { get; set; }
        public string full_name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(40)]
        public string email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string password { get; set; }
        public Nullable<bool> gender { get; set; }
        public Nullable<System.DateTime> day_of_birth { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }
        public Nullable<int> role { get; set; }
        public Nullable<bool> status { get; set; }

    }
}