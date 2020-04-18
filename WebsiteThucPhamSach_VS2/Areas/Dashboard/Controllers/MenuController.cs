using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Common;
using WebsiteThucPhamSach_VS2.Areas.Dashboard.Models;
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
            var menus = new MenusModel().getMenus();
            return View(menus);
        }
    }
}