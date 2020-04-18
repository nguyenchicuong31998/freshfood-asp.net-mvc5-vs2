using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;

namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Controllers
{
    public class AdminController : Controller
    {
        // GET: Dashboard/Admin
        FreshFoodEntities db = new FreshFoodEntities();
        public ActionResult Index()
        {
            if (Session[SessionName.adminId] == null)
            {
                return RedirectToAction("Login", "Dashboard");
            }
            var admins = db.admins.ToList();
            return View(admins);
        }

        public JsonResult OpenStatus(int id)
        {
            var admin = db.admins.FirstOrDefault(n => n.id == id);
            if (admin.status == true)
            {
                admin.status = false;
                UpdateModel(admin);
                db.SaveChanges();
            }
            else
            {
                admin.status = true;
                UpdateModel(admin);
                db.SaveChanges();
            }
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        }
    }
}