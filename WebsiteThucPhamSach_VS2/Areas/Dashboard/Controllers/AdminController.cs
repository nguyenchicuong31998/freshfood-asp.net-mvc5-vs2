using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Areas.Dashboard.Models;
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
            var admins = new AdminsModel().getAdmins();
            return View(admins);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<role> roles = new RolesModel().getRoles();
            SelectList listRoles = new SelectList(roles, "id", "name");
            ViewBag.roles = listRoles;
            return View();
        }
        [HttpPost]
        public ActionResult Create(AdminsModel admin)
        {
            if (ModelState.IsValid)
            {
                var created = new AdminsModel().create(admin);
                if (created)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            List<role> roles = new RolesModel().getRoles();
            SelectList listRoles = new SelectList(roles, "id", "name");
            ViewBag.roles = listRoles;
            return View(admin);
        }

        public ActionResult Edit(int id)
        {
            var admin = new AdminsModel().getAdminById(id);
            AdminsModel adminsModel = new AdminsModel();
            adminsModel.full_name = admin.full_name;
            adminsModel.email = admin.email;
            adminsModel.password = admin.password;
            adminsModel.gender = admin.gender;
            adminsModel.date_of_birth = admin.date_of_birth;
            adminsModel.phone_number = admin.phone_number;
            adminsModel.address = admin.address;
            adminsModel.role = int.Parse(admin.role.ToString());
            List<role> roles = new RolesModel().getRoles();
            SelectList listRoles = new SelectList(roles, "id", "name");
            ViewBag.roles = listRoles;
            return View(adminsModel);
        }
        [HttpPost]
        public ActionResult Edit(AdminsModel admin)
        {
            if (ModelState.IsValid)
            {
                var updated = new AdminsModel().updateById(admin.id, admin);
                if (updated)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                    //return View(admin);
                }
            }
            List<role> roles = new RolesModel().getRoles();
            SelectList listRoles = new SelectList(roles, "id", "name");
            ViewBag.roles = listRoles;
            return View(admin);
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