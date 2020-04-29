using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;

namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Models
{
    public class UsersAdModel
    {
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