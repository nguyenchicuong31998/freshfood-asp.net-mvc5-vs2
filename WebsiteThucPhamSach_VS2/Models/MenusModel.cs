using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class MenusModel
    {
        FreshFoodEntities db = new FreshFoodEntities();
        public List<menu> getMenus()
        {
            var menus = (from n in db.menus
                         where n.parent_id == null && n.status == true
                         select n).ToList();
            return menus;
        }

        public List<menu> getChildMenus(int parentId)
        {
            var childMenus = (
                               from n in db.menus
                               where n.parent_id == parentId && n.status == true
                               select n
                             ).ToList();
            return childMenus;
        }

        public List<menu> getLeftMenu()
        {
            var leftMenus = (from n in db.menus
                             where n.status == true && n.order != 1 && n.order == 2
                             select n).ToList();
            return leftMenus;
        }

        public List<menu> getLeftChildMenus(int parentId)
        {
            var childMenusLeft = (
                               from n in db.menus
                               where n.parent_id == parentId && n.status == true && n.order != 1
                               select n
                             ).ToList();
            return childMenusLeft;
        }

        public string getNameMenuById(int? id)
        {
            if(id != null)
            {
                var menu = db.menus.SingleOrDefault(p => p.id == id);
                string name = menu.name;
                return name == "" ? "" : name;
            }
            return "";
        }
    }
}