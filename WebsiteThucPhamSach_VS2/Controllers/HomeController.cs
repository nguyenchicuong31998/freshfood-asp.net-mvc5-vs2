using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;
using WebsiteThucPhamSach_VS2.EmailTeamplate;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Text;

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
        public ActionResult Products(int? id)
        {
            var menus = new MenusModel().getLeftMenu();
            ViewBag.Categories = menus;
            ViewBag.Name = new MenusModel().getNameMenuById(id);
            return View();
        }

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


        public ActionResult Carts()
        {
            return View();
        }

        private List<Cart> getProductInCookie()
        {
            List<Cart> carts = new List<Cart>();

            if(Request.Cookies["CartCookie"] != null)
            {
                string cartCookies = Request.Cookies["CartCookie"].Value;
                carts = JsonConvert.DeserializeObject<List<Cart>>(cartCookies);
            }
            return carts;
        }

        public int getProductCountInCart()
        {
            var cartProduct = this.getProductInCookie();
            return cartProduct.Count();
        }
        public JsonResult UpdateQuantityProductById(int id, int quantity)
        {
            var cartProduct = this.getProductInCookie();
            Cart cart = cartProduct.FirstOrDefault(p => p.id == id);
            cart.quantity = quantity;
            Response.Cookies.Clear();
            Response.Cookies["CartCookie"].Value = new JavaScriptSerializer().Serialize(cartProduct);
            return Json(new
            {
               status = true
            });
        }
        public JsonResult DeleteProductById(int id)
        {
            var cartProduct = this.getProductInCookie();
            cartProduct.RemoveAll(p => p.id == id);
            Response.Cookies.Clear();
            Response.Cookies["CartCookie"].Value = new JavaScriptSerializer().Serialize(cartProduct);
            return Json(new
            {
                status = true
            });
        }

        public PartialViewResult CartsContentPartial()
        {
            var carts = this.getProductInCookie();
            foreach(Cart cart in carts)
            {
                cart.name = Server.UrlDecode(cart.name);
            }
            return PartialView(carts);
        }

        public JsonResult AddCart(int id, int quantity)
        {
            var cartProduct = this.getProductInCookie();
            if(cartProduct.FirstOrDefault(p=>p.id == id) == null)
            {
                product product = db.products.Find(id);
                Cart cart = new Cart()
                {
                    id = product.id,
                    name = Server.UrlEncode(product.name),
                    image = product.image,
                    quantity = quantity,
                    price = Decimal.Parse(product.price.ToString()),
                };
                if(product.price_promotion > 0)
                {
                    cart.price_promotion = Decimal.Parse(product.price_promotion.ToString());
                }
                cartProduct.Add(cart);
                Response.Cookies["CartCookie"].Value = new JavaScriptSerializer().Serialize(cartProduct);
            }
            else
            {

                Cart cart = cartProduct.FirstOrDefault(p => p.id == id);
                cart.quantity += quantity;
                Response.Cookies["CartCookie"].Value = new JavaScriptSerializer().Serialize(cartProduct);
            }
            //if (Request.Cookies["CartCookie"] == null)
            //{
            //    product product = db.products.Find(id);
 
            //    Cart cart = new Cart()
            //    {
            //        id = product.id,
            //        name = Server.UrlEncode(product.name),
            //        image = product.image,
            //        quantity = quantity,
            //        price = Decimal.Parse(product.price.ToString()),
               
            //    };  
            //    //cart.name = Server.UrlEncode(cart.name);
            //    Response.Cookies["CartCookie"].Value = new JavaScriptSerializer().Serialize(cart);
            //}
            //else
            //{
            //    product product = db.products.Find(id);
              
            //    Cart cart = new Cart()
            //    {
            //        id = product.id,
            //        name = Server.UrlEncode(product.name),
            //        image = product.image,
            //        quantity = quantity,
            //        price = Decimal.Parse(product.price.ToString()),

            //    };  
            //    //cart.name = Server.UrlEncode(cart.name);
            //    //Response.Cookies["CartCookie"].Value = "["+ Request.Cookies["CartCookie"].Value + ","+ new JavaScriptSerializer().Serialize(cart)+"]";
            //    Response.Cookies["CartCookie"].Value =  Request.Cookies["CartCookie"].Value + "|" + new JavaScriptSerializer().Serialize(cart);

            //}
            if (Session["id"] == null)
            {
                Response.Cookies["CartCookie"].Expires = DateTime.Now.AddMinutes(15);
            }
            else
            {
                Response.Cookies["CartCookie"].Expires = DateTime.Now.AddDays(30);
            }

            return Json(new
            {
                status = true
            });
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
        [HttpGet]
        public ActionResult Payment()
        {
            Session["url"] = Request.UrlReferrer.ToString();
            if (Session["id"] == null)
            {
                return Redirect("~/Dang-Nhap");
            }
            var user = new UsersModels().getUserById(int.Parse(Session["id"].ToString()));
            PaymentModel payment = new PaymentModel();
            payment.id = user.id;
            payment.display_name = user.display_name;
            payment.phone_number = user.phone_number;
            payment.address = user.address;
            payment.email = user.email;
            return View(payment);
        }

        [HttpPost]
        public ActionResult Payment(PaymentModel payment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var userId = 
                    Response.Cookies["CartCookie"].Expires = DateTime.Now.AddDays(-1);
                    //var body = new PaymentSuccess().body();
                    //new Utils().SendEmail(payment.email, "Chúc mừng bạn đã mua sản phẩm thành công", body, "", "");
                    return Redirect("~/Trang-Chu");
                }
            }catch(Exception e)
            {
                
            }
            return View(payment);
        }
        private int addOrderAndReturnUserId()
        {
            return 1;
        }
        private decimal totalMoney()
        {
            var carts = this.getProductInCookie();
            decimal totalMoney = 0;
            foreach(Cart cart in carts)
            {
                if(cart.price_promotion > 0)
                {
                    var giamGia = String.Format("{0:0}", cart.price * ((100 - cart.price_promotion) / 100) * cart.quantity);
                    totalMoney += Decimal.Parse(giamGia.ToString());
                }
                else
                {
                    totalMoney += cart.price;
                }
            }
            return totalMoney;
        }

        public PartialViewResult YourOrderPartial()
        {
            var carts = this.getProductInCookie();
            foreach (Cart cart in carts)
            {
                cart.name = Server.UrlDecode(cart.name);
            }
            return PartialView(carts);
        }

        public PartialViewResult ModalProductPartial(int id)
        {
            var product = new ProductsModel().getProductById(id);
            return PartialView(product);
        }
    }
}