using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class PaymentModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string display_name { get; set; }
        public Nullable<bool> gender { get; set; }
        public Nullable<System.DateTime> date_of_birth { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string phone_number { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string address { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(40)]
        public string email { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn hình thức thanh toán")]
        public string form_payment { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn hình thức giao hàng")]
        public string form_delivery { get; set; }

        public string password { get; set; }
        public Nullable<bool> status { get; set; }
    }
}