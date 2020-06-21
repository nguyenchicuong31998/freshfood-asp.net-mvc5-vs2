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


        private void listRoles()
        {
            List<role> roles = new List<role>();
            roles.Add(new role(){ id = 1, name = "Quản lý" });
            roles.Add(new role(){ id = 2, name = "Nhân viên" });
            SelectList listRoles = new SelectList(roles,"name", "name");
            ViewBag.roles = listRoles;
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.listRoles();
            return View();
        }
        [HttpPost]
        public ActionResult Create(role role)
        {
            var checkName = new RolesModel().checkRoleExists(role.name);
            if (checkName)
            {
                ModelState.AddModelError("name", "Quyền đã tồn tại");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    role.status = true;
                    var created = new RolesModel().createRole(role);
                    if (created)
                    {
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                }
            }
            this.listRoles();
            return View(role);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var roled = new RolesModel().getRoleById(id);
            this.listRoles();
            return View(roled);
        }
        [HttpPost]
        public ActionResult Edit(role updateRole)
        {
            if (ModelState.IsValid)
            {
                var updated = new RolesModel().updateRoleById(updateRole.id, updateRole);
                if (updated)
                {
                    return Redirect(Request.UrlReferrer.ToString());
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

        public JsonResult OpenStatus(int id)
        {
            var changed = new RolesModel().changeStatusById(id);
            if (changed)
            {
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }
    }
}