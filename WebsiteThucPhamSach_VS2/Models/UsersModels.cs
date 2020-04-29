using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;
using System.ComponentModel.DataAnnotations;
using WebsiteThucPhamSach_VS2.Common;
using System.Web.Mvc;
using System.Data.Entity;

namespace WebsiteThucPhamSach_VS2.Models
{
    public partial class UsersModels
    {
        public int id { get; set; }
        public string display_name { get; set; }
        public Nullable<bool> gender { get; set; }
        public Nullable<System.DateTime> date_of_birth { get; set; }
        public string phone_number { get; set; }
        public string address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [StringLength(40)]
        public string email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string password { get; set; }
        public Nullable<bool> status { get; set; }

        FreshFoodEntities db = new FreshFoodEntities();
        Utils utils = new Utils();

        public List<user> getUsers()
        {
            return db.users.ToList();
        } 

        public user getUserById(int id)
        {
            return db.users.SingleOrDefault(user => user.id == id);
        }

        public user getUserByEmail(string email)
        {
            return db.users.SingleOrDefault(user => user.email == email);
        }

        public user login(string email, string password)
        {
            try {
                var hashPassword = utils.GetMd5Hash(password);
                var results = utils.VerifyMd5Hash(password, hashPassword);
                if (results)
                {
                    var userExist = db.users.SingleOrDefault(n => n.email == email && n.password == hashPassword && n.status == true);
                    if (userExist != null)
                    {
                        return userExist;
                    }
                }
                return null;
            }
            catch(Exception e) {
                throw e;
            }
        }

        public bool checkEmailExisted(string email)
        {
            var isEmail = db.users.FirstOrDefault(user => user.email == email);
            if(isEmail != null)
            {
                return true;
            }
            return false;
        }

        public bool checkPassword(string password,string newPassword)
        {
            var hashPassword = utils.GetMd5Hash(newPassword);
            if (password == hashPassword)
            {
                return true;
            }
            return false;
        }

        public user register(user newUser)
        {
            try
            {
                newUser.password = utils.GetMd5Hash(newUser.password);
                newUser.start_time = DateTime.Now;
                newUser.status = true;
                db.users.Add(newUser);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
            return newUser;
        }
        public user updatePassword(user user,string newPassword)
        {
            user.password = utils.GetMd5Hash(newPassword);
            db.users.Where(n => n.id == user.id).ToList().ForEach(a =>
            {
                a.password = user.password;    
            });
            db.SaveChanges();
            return user;
        }
        public user updateUser(int userId, user changeUser)
        {

            var currentUser = this.getUserById(userId);

            if(currentUser != null) {
                currentUser.display_name = changeUser.display_name;
                currentUser.gender = changeUser.gender;
                currentUser.date_of_birth = changeUser.date_of_birth;
                currentUser.phone_number = changeUser.phone_number;
                currentUser.address = changeUser.address;
                db.Entry(currentUser).State = EntityState.Modified;
                db.SaveChanges();
                return currentUser;
            }

            return null;
        }

        public user registerFacebook(user newUser)
        {
            try
            {
                db.users.Add(newUser);
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }
            return newUser;    
        }

    }
}