using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;

namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Models
{
    public class MenusAdModel
    {
        FreshFoodEntities db = new FreshFoodEntities();

        public int id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        public string name { get; set; }
        public Nullable<int> parent_id { get; set; }
        public Nullable<int> order { get; set; }
        public Nullable<bool> status { get; set; }
        public string link { get; set; }

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

        public bool createMenu(menu menu)
        {
            try
            {
                db.menus.Add(menu);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

            }
            return false;
        }

        public bool updateMenuById(int id, menu updateMenu)
        {
            var currentMenu = this.getMenuById(id);
            if (currentMenu != null)
            {
                currentMenu.name = updateMenu.name;
                currentMenu.parent_id = updateMenu.parent_id;
                currentMenu.link = updateMenu.link;
                currentMenu.order = updateMenu.order;
                db.Entry(currentMenu).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }


        public bool deleteMenuById(int id)
        {
            try
            {
                var menu = this.getMenuById(id);
                if (menu != null)
                {
                    db.menus.Remove(menu);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }catch(Exception e)
            {
                return false;
            }
        }

        public bool changeStatusById(int id)
        {
            var menu = this.getMenuById(id);
            if (menu != null)
            {
                menu.status = !menu.status;
                db.Entry(menu).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}