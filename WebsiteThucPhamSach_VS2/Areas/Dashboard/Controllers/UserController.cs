using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;
using WebsiteThucPhamSach_VS2.Areas.Dashboard.Models;
namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Controllers
{
    public class UserController : Controller
    {
        // GET: Dashboard/User
        public ActionResult Index()
        {
            if(Session[SessionName.adminId] == null)
            {
                return RedirectToAction("Login", "Dashboard");
            }
            var users = new UsersAdModel().getUsers();
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(user user)
        {
            return View();
        }


        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(user user)
        {
            return View();
        }

        public JsonResult Delete(int id)
        {
            var deleted = new UsersAdModel().deleteUserById(id);
            if (deleted)
            {
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
    }
}