using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;

namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Models
{
    public class UsersAdModel
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
        public string address { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(40)]
        public string email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(40, ErrorMessage = "Mật khẩu phải từ 6 đến 32 ký tự", MinimumLength = 6)]
        public string password { get; set; }
        public Nullable<bool> status { get; set; }
        FreshFoodEntities db = new FreshFoodEntities();
        public List<user> getUsers()
        {
            return db.users.ToList();
        }

        public user getUserById(int id)
        {
            var user = db.users.SingleOrDefault(u => u.id == id);
            if(user != null)
            {
                return user;
            }
            return null;
        }

        public int totalUser()
        {
            return db.users.Count();
        }
        
        public bool createUser(UsersAdModel newUser)
        {
            try
            {
                user user = new user();
                user.display_name = newUser.display_name;
                user.gender = newUser.gender;
                user.date_of_birth = newUser.date_of_birth;
                user.phone_number = newUser.phone_number;
                user.address = newUser.address;
                user.email = newUser.email; 
                user.password = new Utils().GetMd5Hash(newUser.password);
                user.status = true;
                user.start_time = DateTime.Now;
                db.users.Add(user);
                db.SaveChanges();
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public bool updateUserById(int id, UsersAdModel updateUser)
        {
            try
            {
                var currentUser = this.getUserById(id);
                if(currentUser != null)
                {
                    currentUser.display_name = updateUser.display_name;
                    currentUser.gender = updateUser.gender;
                    currentUser.date_of_birth = updateUser.date_of_birth;
                    currentUser.phone_number = updateUser.phone_number;
                    currentUser.address = updateUser.address;
                    db.Entry(currentUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }catch(Exception e)
            {
                return false;
            }
        }
        public bool checkEmailExisted(string email)
        {
            var isEmail = db.users.FirstOrDefault(user => user.email == email);
            if (isEmail != null)
            {
                return true;
            }
            return false;
        }

        public bool changeStatusById(int id)
        {
            var user = this.getUserById(id);
            if(user != null)
            {
                user.status = !user.status;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool deleteUserById(int id)
        {
            try
            {
                var user = this.getUserById(id);
                if(user != null)
                {
                    db.users.Remove(user);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }catch(Exception e)
            {
                return false;
            }
        }
    }
}