using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
namespace WebsiteThucPhamSach_VS2.Controllers
{
    public class NavigationController : Controller
    {
        // GET: Navigation
        //FreshFoodEntities db = new FreshFoodEntities();
        public PartialViewResult NavigationPartial()
        {
            var menus = new MenusModel().getMenus();
            return PartialView(menus);
        }


        public PartialViewResult ChildMenuPartial(int parentId)
        {
            var childMenus = new MenusModel().getChildMenus(parentId);
            ViewBag.childCount = childMenus.Count();
            ViewBag.childMenus = childMenus;
            return PartialView("ChildMenuPartial");
        }
    }
}