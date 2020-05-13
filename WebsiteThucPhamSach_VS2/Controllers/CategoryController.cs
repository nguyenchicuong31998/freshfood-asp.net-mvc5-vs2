using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;

namespace WebsiteThucPhamSach_VS2.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Menu
        public PartialViewResult LeftChildMenusPartial(int parentId)
        {
            var leftChildMenus = new MenusModel().getLeftChildMenus(parentId);
            ViewBag.categoryId = parentId;
            ViewBag.leftChildMenuCount = leftChildMenus.Count();
            ViewBag.leftChildMenus = leftChildMenus;
            return PartialView("LeftChildMenusPartial");
        }
    }
}