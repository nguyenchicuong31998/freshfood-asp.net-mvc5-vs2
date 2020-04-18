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
            return View();
        }

        public ActionResult Products()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public PartialViewResult FeaturedProductPartial()
        {
            var products = new ProductsModel().getProductsByNewStartTime();
            return PartialView(products);
        }
    }
}