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
        FreshFoodEntities db = new FreshFoodEntities();
        public PartialViewResult NavigationPartial()
        {
            var menus = (from n in db.menus
                        where n.parent_id == null && n.status == true
                        select n).ToList();
            return PartialView(menus);
        }


        public PartialViewResult ChildMenuPartial(int parentId)
        {
            var childMenus = (
                               from n in db.menus
                               where n.parent_id == parentId && n.status == true
                               select n
                             ).ToList();

            ViewBag.childCount = childMenus.Count();
            ViewBag.childMenus = childMenus;
            return PartialView("ChildMenuPartial");
        }
    }
}