using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;
using WebsiteThucPhamSach_VS2.Areas.Dashboard.Models;
using WebsiteThucPhamSach_VS2.EmailTeamplate;
namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Controllers
{
    public class UserController : Controller
    {
        // GET: Dashboard/User
        Utils utils = new Utils();
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
        public ActionResult Create(UsersAdModel user)
        {
            try
            {
                var emailExisted = new UsersAdModel().checkEmailExisted(user.email);
                if (!emailExisted)
                {
                    var created = new UsersAdModel().createUser(user);
                    if (created)
                    {
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                }
                else
                {
                    ModelState.AddModelError("email", "Địa chỉ email đã tồn tại. Vui lòng nhập địa chỉ email khác.");
                }

            }catch(Exception e)
            {
                throw e;
            }
            return View(user);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new UsersAdModel().getUserById(id);
            UsersAdModel usersAdModel = new UsersAdModel();
            usersAdModel.display_name = user.display_name;
            usersAdModel.gender = user.gender;
            usersAdModel.date_of_birth = user.date_of_birth;
            usersAdModel.phone_number = user.phone_number;
            usersAdModel.address = user.address;
            usersAdModel.email = user.email;
            return View(usersAdModel);
        }

        [HttpPost]
        public ActionResult Edit(UsersAdModel user)
        {
            try
            {
                var updated = new UsersAdModel().updateUserById(user.id, user);
                if (updated)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }catch(Exception e)
            {
                throw e;
            }
            return View(user);
        }

        public JsonResult OpenStatus(int id)
        {
            var changed = new UsersAdModel().changeStatusById(id);
            if (changed)
            {
                return Json(new { status = true });
            }
            return Json(new { status = false });
        }

        public ActionResult ResetPasswordForUser(string email)
        {
            var emailExists = new UsersModels().getUserByEmail(email);
            if (emailExists == null)
            {
                return Json(new
                {
                    status = false
                });
            }
            var newPassword = utils.RandomChar(8);
            var updatedUser = new UsersModels().updatePassword(emailExists, newPassword);
            string body = new ResetPasswordForUser().body(updatedUser.display_name, newPassword);
            var isTrue = utils.SendEmail(emailExists.email, "Đặt lại mật khẩu", body, "", "");
            if (isTrue)
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