using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;
using WebsiteThucPhamSach_VS2.EmailTeamplate;
using System.Configuration;
using Facebook;
using System.Data;

namespace WebsiteThucPhamSach_VS2.Controllers
{
    public class HeaderController : Controller
    {
        // GET: Header

        Utils utils = new Utils();
        public PartialViewResult  HeaderPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsersModels user)
        {
            if (ModelState.IsValid)
            {
                var results = new UsersModels().login(user.email, user.password);
                if (results != null)
                {
                    Session["display_name"] = results.display_name;
                    Session["id"] = results.id;
                    Session.Timeout = 30;
                    if (Session["url"] != null)
                    {
                        return Redirect(Session["url"].ToString());
                    }
                    return Redirect("~/Trang-Chu");
                }
                else
                {
                    ViewBag.errorLogin = "Thông tin đăng nhập không đúng";
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            Session["id"] = null;
            Session.Clear();
            return Redirect("~/Trang-Chu");
        }

        private Uri RedirectUriFaceBook
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["fbAppId"],
                client_secret = ConfigurationManager.AppSettings["fbAppSecret"],
                redirect_uri = RedirectUriFaceBook.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string Code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["fbAppId"],
                client_secret = ConfigurationManager.AppSettings["fbAppSecret"],
                redirect_uri = RedirectUriFaceBook.AbsoluteUri,
                code = Code
            });
            var access_token = result.access_token;
            if (!string.IsNullOrEmpty(access_token))
            {
                fb.AccessToken = access_token;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string id = me.id;
                string email = me.email;
                string display_name = me.last_name + " " + me.first_name;
                user user  = new user();
                if (email == null)
                {
                    user.email = "facebook@gmail.com";
                }
                else
                {
                    user.email = email;
                }
                user.date_of_birth = DateTime.Parse("1/12/2000");
                user.password = me.email;
                user.display_name = display_name;
                user.status = true;
                var userExisted = new UsersModels().getUserByEmail(user.email);
                if(userExisted == null)
                {
                    var createdUser = new UsersModels().registerFacebook(user);
                    Session["id"] = createdUser.id;
                    Session["display_name"] = createdUser.display_name;
                    Session.Timeout = 30;
                    if (Session["url"] != null)
                    {
                        return Redirect(Session["url"].ToString());
                    }
                    return Redirect("~/Trang-Chu");
                }
                else
                {
                    Session["id"] = userExisted.id;
                    Session["display_name"] = userExisted.display_name;
                    Session.Timeout = 30;
                    if (Session["url"] != null)
                    {
                        return Redirect(Session["url"].ToString());
                    }
                    return Redirect("~/Trang-Chu");
                }
            }
            return View();
        }

        public ActionResult LoginGoogle()
        {

            return View();
        }

        public ActionResult Register()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel newUser)
        {
            if (ModelState.IsValid)
            {
                var emailExisted = new UsersModels().checkEmailExisted(newUser.email);
                if (emailExisted)
                {
                    ModelState.AddModelError("Email", "Địa chỉ email đã tồn tại. Vui lòng nhập địa chỉ email khác.");
                    return View(newUser);
                }
                else
                {
                    user userInfo = new user();
                    userInfo.display_name = newUser.display_name;
                    userInfo.date_of_birth = newUser.date_of_birth;
                    userInfo.email = newUser.email;
                    userInfo.gender = newUser.gender;
                    userInfo.password = newUser.password;
                    new UsersModels().register(userInfo);
                    return Redirect("~/Dang-Nhap");
                }
            }
            return View(newUser);
        }

        [HttpGet]
        public ActionResult PersonalInformation()
        {
            PersonalInfoModel personalInfoModel = new PersonalInfoModel();
            if(Session["id"] == null)
            {
                return Redirect("~/Dang-Nhap");
            }
            int id = (int)Session["id"];
            var user = new UsersModels().getUserById(id);
            personalInfoModel.display_name = user.display_name;
            personalInfoModel.date_of_birth = user.date_of_birth;
            personalInfoModel.address = user.address;
            personalInfoModel.gender = user.gender;
            personalInfoModel.phone_number = user.phone_number;
            return View(personalInfoModel);
        }

        [HttpPost]
        public ActionResult PersonalInformation(PersonalInfoModel personalInfo)
        {
            if (ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                int userId = (int)Session["id"];
                user userInfo = new user();
                userInfo.display_name = personalInfo.display_name;
                userInfo.date_of_birth = personalInfo.date_of_birth;
                userInfo.address = personalInfo.address;
                userInfo.gender = personalInfo.gender;
                userInfo.phone_number = personalInfo.phone_number;
                var updatedUser = new UsersModels().updateUser(userId, userInfo);
                Session["display_name"] = updatedUser.display_name;
                return Redirect("~/Thong-Tin-Ca-Nhan");
            }
            return View(personalInfo);
        }
        [HttpGet]
        public PartialViewResult ForgotPasswordPartial()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult ForgotPasswordPartial(string email)
        {
            var emailExists = new UsersModels().getUserByEmail(email);
            if (emailExists == null)
            {
                return Json(new
                {
                    status = false
                });
            }
            var newPassword = utils.RandomChar(6);
            var updatedUser = new UsersModels().updatePassword(emailExists, newPassword);
            string body = new ForgotPassword().body(updatedUser.display_name, newPassword);
            var isTrue = utils.SendEmail(emailExists.email, "Đặt lại mật khẩu", body, "", "");
            if (isTrue) {
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
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel changePassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(Session["id"] == null)
                    {
                        return Redirect("~/Trang-Chu");
                    }
                    var user = new UsersModels().getUserById((int)Session["id"]);
                    var isTrue = new UsersModels().checkPassword(user.password, changePassword.password);
                    if (isTrue)
                    {
                        new UsersModels().updatePassword(user, changePassword.new_password);
                        Session["id"] = null;
                        return Redirect("~/Dang-Nhap");
                    }
                    ModelState.AddModelError("password", "Mật khẩu không chính xác");
                    return View(changePassword);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return View(changePassword);
        }

        public string getLogo()
        {
            var logo = new AdvertisesModel().getLogo();
            if (logo == null)
            {
                return "";
            }
            return logo.url != "" ? logo.url : null;
        }
    }
}