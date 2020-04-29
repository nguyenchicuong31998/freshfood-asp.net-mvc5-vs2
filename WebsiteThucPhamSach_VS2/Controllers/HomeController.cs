using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;

namespace WebsiteThucPhamSach_VS2.Controllers
{
    public class HomeController : Controller
    {
        [HandleError]
        public ActionResult Index()
        {
            UsersModels users = new UsersModels();
            var results = users.getUsers();
            return View(results);
        }

        public ActionResult About()
        {
            var about = new AboutsModel().getAboutByStatusTrue();
            return View(about);
        }

        public ActionResult Products()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contacts()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contacts(ContactsModel contact)
        {
            if (ModelState.IsValid)
            {
                var created = new ContactsModel().create(contact);
                if(created == true)
                {
                    TempData["message"] = "1";
                    ViewBag.ok = "Nguyễn Chí Cường";
                    //TempData["message"] = "Bạn đã gửi liên hệ thành công";
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            return View(contact);
        }

        public PartialViewResult FeaturedProductPartial()
        {
            var products = new ProductsModel().getProductsByViewCount();
            return PartialView(products);
        }
        public PartialViewResult NewProductPartial()
        {
            var products = new ProductsModel().getProductsByNewStartTime();
            return PartialView(products);
        }

        public PartialViewResult ProductForYouPartial()
        {
            var products = new ProductsModel().getProducts();
            return PartialView(products);
        }

        public ActionResult DetailProduct(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}