using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;
using System.Web.Script.Serialization;

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
            var events = new EventsModel().getEventsClient();
            return View(events);
        }

        public ActionResult DetailNew(int id)
        {
            var @event = new EventsModel().getEventById(id);
            if(@event == null)
            {
                return RedirectToAction("News");
            }
            return View(@event);        
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
            var detailProduct = new ProductsModel().getProductById(id);
            return View(detailProduct);
        }

        [HttpGet]
        public PartialViewResult AddFeedbackPartial()
        {
            return PartialView();
        }
        [HttpPost]
        public JsonResult AddFeedbackPartial(string newFeedback)
        {
            
            var feedback = new JavaScriptSerializer().Deserialize<feedback>(newFeedback);
            if (feedback != null)
            {
                if (Session["id"] != null)
                {
                    feedback.user_id = int.Parse(Session["id"].ToString());
                    feedback.start_time = DateTime.Now;
                    feedback.status = true;
                    var created = new FeedbacksModel().create(feedback);
                    if (created)
                    {
                        return Json(
                          new { status = true }
                        );
                    }
                }
                else
                {
                    Session["url"] = Request.UrlReferrer.ToString();
                    return Json(new { yetLogin = true });
                }
            }
            return Json(
              new { status = false}
            );
        }

        [HttpGet]
        public PartialViewResult DisplayFeedbackPartial(int id)
        {
            var feedbacks = new FeedbacksModel().GetFeedbacksClientByProductId(id);
            return PartialView(feedbacks);
        }

        public string GetNameUserById(int id)
        {
            var userName = new FeedbacksModel().getNameUserById(id);
            return userName;
        }

        public string GetNameProductById(int id)
        {
            var productName = new FeedbacksModel().getNameProductById(id);
            return productName;
        }
    }
}