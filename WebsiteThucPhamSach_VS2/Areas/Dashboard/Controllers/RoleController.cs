using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Areas.Dashboard.Models;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;
namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Controllers
{
    public class RoleController : Controller
    {
        // GET: Dashboard/Role

        public ActionResult Index()
        {
            if (Session[SessionName.adminId] == null)
            {
                return RedirectToAction("Login", "Dashboard");
            }
            var roles = new RolesModel().getRoles();
            return View(roles);
        }

        public ActionResult CreatePartial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePartial(role role)
        {
            if (ModelState.IsValid)
            {
                role.status = true;
                var created = new RolesModel().createRole(role);
                if (created)
                {
                    return RedirectToAction("Index","Role");
                }
            }
            return RedirectToAction("Index", "Role");
        }

        public ActionResult EditPartial(int id)
        {
            var roled = new RolesModel().getRoleById(id);
            return View(roled);
        }

        [HttpPost]
        public ActionResult EditPartial(role updateRole)
        {
            if (ModelState.IsValid)
            {
                var updated = new RolesModel().updateRoleById(updateRole.id, updateRole);
                if (updated)
                {
                    return RedirectToAction("Index", "Role");
                }
            }
            return View();
        }

        public JsonResult DeleteRole(int id)
        {
            var deleted = new RolesModel().deleteRoleById(id);
            if (deleted) { 
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false
            }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult OpenStatus(int id)
        //{
        //    var admin = db.admins.FirstOrDefault(n => n.id == id);
        //    if (admin.status == true)
        //    {
        //        admin.status = false;
        //        UpdateModel(admin);
        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        admin.status = true;
        //        UpdateModel(admin);
        //        db.SaveChanges();
        //    }
        //    return Json(new { status = true }, JsonRequestBehavior.AllowGet);
        //}
    }
}