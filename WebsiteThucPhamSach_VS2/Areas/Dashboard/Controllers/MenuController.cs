using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Common;
using WebsiteThucPhamSach_VS2.Areas.Dashboard.Models;
using WebsiteThucPhamSach_VS2.Models;
namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Controllers
{
    public class MenuController : Controller
    {
        // GET: Dashboard/Menu
        public ActionResult Index()
        {
            if (Session[SessionName.adminId] == null)
            {
                return RedirectToAction("Login", "Dashboard");
            }
            var menus = new MenusAdModel().getMenus();
            return View(menus);
        }

        private void getMenus()
        {
            List<menu> menus = new MenusAdModel().getMenus();
            menus.Insert(0, new menu() { name = "Không chọn danh mục" });
            SelectList listMenus = new SelectList(menus, "id", "name");
            ViewBag.menus = listMenus;
        }

        public ActionResult Create()
        {
            this.getMenus();
            return View();
        }
        [HttpPost]
        public ActionResult Create(MenusAdModel menu)
        {
            if (ModelState.IsValid)
            {
                menu dbmenu = new menu();
                dbmenu.id = menu.id;
                dbmenu.name = menu.name;
                dbmenu.link = menu.link;
                dbmenu.order = menu.order;
                dbmenu.parent_id = menu.parent_id;
                dbmenu.status = true;
                var created = new MenusAdModel().createMenu(dbmenu);
                if (created)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            this.getMenus();
            return View(menu);
        }


        public ActionResult Edit(int id)
        {
            var menu = new MenusAdModel().getMenuById(id);
            MenusAdModel menusAdModel = new MenusAdModel();
            menusAdModel.id = menu.id;
            menusAdModel.name = menu.name;
            menusAdModel.link = menu.link;
            menusAdModel.order = menu.order;
            menusAdModel.parent_id = int.Parse(menu.parent_id.ToString());
            this.getMenus();
            return View(menusAdModel);
        }
        [HttpPost]
        public ActionResult Edit(MenusAdModel menu)
        {
            if (ModelState.IsValid)
            {
                menu dbmenu = new menu();
                dbmenu.id = menu.id;
                dbmenu.name = menu.name;
                dbmenu.link = menu.link;
                dbmenu.order = menu.order;
                dbmenu.parent_id = menu.parent_id;
                var updated = new MenusAdModel().updateMenuById(dbmenu.id, dbmenu);
                if (updated)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            this.getMenus();
            return View(menu);
        }


        public JsonResult DeleteMenuById(int id)
        {
            var deleted = new MenusAdModel().deleteMenuById(id);
            if (deleted)
            {
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }


        public string getNameParentById(int id)
        {
            var menu = new MenusAdModel().getMenuById(id);
            if(menu != null)
            {
                return menu.name; 
            }
            return "Không có";
        }

        public JsonResult ChangeStatus(int id)
        {
            var changed = new MenusAdModel().changeStatusById(id);
            if (changed)
            {
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }
    }
}