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
    public class DashboardController : Controller
    {
        // GET: Dashboard/Home
        public ActionResult Index()
        {
            if (Session[SessionName.adminId] == null)
            {
                return RedirectToAction("Login", "Dashboard");
            }
            ViewBag.totalUser = new UsersAdModel().totalUser();
            ViewBag.totalFeeback = new FeedbacksModel().totalFeedbacks();
            ViewBag.totalProduct = new ProductsAdModel().getTotalProducts();
            return View();
        }

        public PartialViewResult HeaderPartial()
        {
            if (Session[SessionName.adminId] == null)
            {
                Response.Redirect("~/Dashboard/Dashboard/Login");
            }
            return PartialView();
        }

        [ChildActionOnly]
        public PartialViewResult SiderBarPartial()
        {
            if (Session[SessionName.adminId] == null)
            {
                Response.Redirect("~/Dashboard/Dashboard/Login");
            }
            return PartialView();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel admin)
        {
            try
            {
                if (ModelState.IsValid) { 
                    var currentAdmin = new AdminsModel().login(admin.email, admin.password);
                    if (currentAdmin != null)
                    {
                        Session[SessionName.adminId] = currentAdmin.id;
                        Session[SessionName.adminName] = currentAdmin.full_name;
                        Session[SessionName.adminRole] = currentAdmin.role1.name;
                        Session.Timeout = 30;
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        ViewBag.errorLogin = "Thông tin đăng nhập không đúng";
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return View(admin);
        }

        public ActionResult Logout()
        {
            Session[SessionName.adminId] = null;
            Session.Clear();
            return RedirectToAction("Login", "Dashboard");
        }
    }
}