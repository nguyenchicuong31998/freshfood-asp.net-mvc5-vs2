using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;

namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Models
{
    public class MenusAdModel
    {
        FreshFoodEntities db = new FreshFoodEntities();
        public List<menu> getMenuOtherOne()
        {
            return db.menus.Where(m => m.order != 1).ToList();
        }

        public List<menu> getMenus()
        {
            return db.menus.ToList();
        }

        public menu getMenuById(int id)
        {
            var menu = db.menus.SingleOrDefault(mn => mn.id == id);
            return menu;
        }
    }
}