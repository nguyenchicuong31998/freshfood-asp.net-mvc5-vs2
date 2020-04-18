using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Common;
using WebsiteThucPhamSach_VS2.Models;
namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Models
{
    public class AdminsModel
    {
        FreshFoodEntities db = new FreshFoodEntities();

        public List<admin> getAdmins()
        {
            return db.admins.ToList();
        }

        public admin getAdminById(int id)
        {
            return db.admins.SingleOrDefault(ad => ad.id == id);
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
    }
}