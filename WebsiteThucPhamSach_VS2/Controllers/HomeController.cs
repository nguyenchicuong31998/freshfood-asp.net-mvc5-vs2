using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace WebsiteThucPhamSach_VS2.Controllers
{
    public class HomeController : Controller
    {
        FreshFoodEntities db = new FreshFoodEntities();
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
            var menus = new MenusModel().getLeftMenu();
            ViewBag.Categories = menus;
            return View();
        }
        //public void ChildMenuLeft(int parentId)
        //{
        //    var leftChildMenus = new MenusModel().getLeftChildMenus(parentId);
        //    ViewBag.leftChildMenuCount = leftChildMenus.Count();
        //    ViewBag.leftChildMenus = leftChildMenus;
        //}

        //public ActionResult Products(int? id, string sortOrder ,int? page)
        //{
        //    if (id == null)
        //    {
        //        var products = new ProductsModel().getProductsPageList(id ?? 0, sortOrder, page);
        //        return View(products);
        //    }
        //    var productById = new ProductsModel().getProductsPageList(id ?? 0, sortOrder , page);
        //    return View(productById);
        //}

        public PartialViewResult ProductsContentPartial(int? id, string order, string price, int? page)
        {
            ViewBag.order = order == null ? "valued" : order;
            //ViewBag.price = price == null ? "" : price;
            if (id == null)
            {
                var products = new ProductsModel().getProductsPageList(id ?? 0, order ,page);
                return PartialView(products);
            }
            var productsById = new ProductsModel().getProductsPageList(id ?? 0, order, page);
            return PartialView(productsById);
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


        public JsonResult QuickView(int id)
        {
            var products = new FreshFoodEntities().products.SingleOrDefault(p=>p.id == id);
            var json = JsonConvert.SerializeObject(products);
            return Json(json, JsonRequestBehavior.AllowGet);
        }


        
        public ActionResult DetailProduct(int id)
        {
            new ProductsModel().updateViewByProductId(id);
            var detailProduct = new ProductsModel().getProductById(id);
            ViewBag.totalFeedback = new FeedbacksModel().getTotalFeedbackByProductId(id);
            float totalRate= new FeedbacksModel().getTotalFeedbackByProductId(id);
            float totalStar = new FeedbacksModel().getTotalStarByProductId(id);
            ViewBag.AvgStar = float.IsNaN(totalStar / totalRate) ? 0 : (totalStar / totalRate);
            return View(detailProduct);
        }


        public ActionResult Cart()
        {
            return View();
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
        public PartialViewResult DisplayFeedbackPartial(int id, int? page)
        {
            var feedbacks = new FeedbacksModel().GetFeedbacksClientByProductId(id, page);
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



        public PartialViewResult RelatedProductPartial(int id, int menuId)
        {
            var productRelated = new ProductsModel().getProductRelatedById(id, menuId);
            return PartialView(productRelated);
        }

        public PartialViewResult ModalProductPartial(int id)
        {
            var product = new ProductsModel().getProductById(id);
            return PartialView(product);
        }
    }
}