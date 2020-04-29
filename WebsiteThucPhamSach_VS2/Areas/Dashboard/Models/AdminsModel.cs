using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Common;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Areas.Dashboard.Models;
using System.Data.Entity;

namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Models
{
    public class AdminsModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
        public string full_name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [StringLength(40, ErrorMessage = "Mật khẩu phải từ 6 đến 32 ký tự", MinimumLength = 6)]
        public string password { get; set; }
        public Nullable<bool> gender { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Vui lòng chọn ngày sinh.")]
        public Nullable<System.DateTime> date_of_birth { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string phone_number { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        public string address { get; set; }
        public Nullable<int> role { get; set; }
        public Nullable<System.DateTime> start_time { get; set; }
        public Nullable<bool> status { get; set; }
        public virtual role role1 { get; set; }

        FreshFoodEntities db = new FreshFoodEntities();

        public List<admin> getAdmins()
        {
  
            return db.admins.ToList();
        }

        public admin getAdminById(int id)
        {
            var admin = db.admins.SingleOrDefault(ad => ad.id == id);
            if(admin != null)
            {
                return admin;
            }
            return null;
        }

        public admin login(string email, string password)
        {
            var admin = db.admins.SingleOrDefault(ad => ad.email == email && ad.password == password && ad.status == true);
            if(admin != null)
            {
                return admin;
            }
            return null;
        }

        public bool create(AdminsModel newAdmin)
        {
            try
            {
                admin admin = new admin();
                admin.full_name = newAdmin.full_name;
                admin.email = newAdmin.email;
                admin.password = newAdmin.password;
                admin.gender = newAdmin.gender;
                admin.date_of_birth = newAdmin.date_of_birth;
                admin.phone_number = newAdmin.phone_number;
                admin.address = newAdmin.address;
                admin.role = int.Parse(newAdmin.role.ToString());
                admin.start_time = DateTime.Now;
                admin.status = true;
                db.admins.Add(admin);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool updateById(int id, AdminsModel updateAdmin)
        {
            try
            {
                var currentAdmin = this.getAdminById(id);
                if (currentAdmin != null)
                {
                    currentAdmin.full_name = updateAdmin.full_name;
                    currentAdmin.email = updateAdmin.email;
                    currentAdmin.password = updateAdmin.password;
                    currentAdmin.gender = updateAdmin.gender;
                    currentAdmin.date_of_birth = updateAdmin.date_of_birth;
                    currentAdmin.phone_number = updateAdmin.phone_number;
                    currentAdmin.address = updateAdmin.address;
                    currentAdmin.role = int.Parse(updateAdmin.role.ToString());
                    db.Entry(currentAdmin).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }catch(Exception e)
            {
                throw e;
            }
        }
    }
}