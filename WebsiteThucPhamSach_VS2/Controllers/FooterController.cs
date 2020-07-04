using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteThucPhamSach_VS2.Controllers
{
    public class FooterController : Controller
    {
        // GET: Footer
        public PartialViewResult FooterPartial()
        {

            return PartialView();
        }


        public ActionResult TutorialOrder()
        {
            return View();
        }

        public ActionResult TermsAndPolicy()
        {
            return View();
        }

        public ActionResult CancelOrderAndChangeOrder()
        {
            return View();
        }
    }
}