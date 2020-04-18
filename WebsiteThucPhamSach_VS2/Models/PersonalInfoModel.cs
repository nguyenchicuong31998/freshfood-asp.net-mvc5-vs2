using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class PersonalInfoModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string display_name { get; set; }
        public Nullable<bool> gender { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn ngày sinh")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> date_of_birth { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string phone_number { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string address { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<bool> status { get; set; }
    }
}