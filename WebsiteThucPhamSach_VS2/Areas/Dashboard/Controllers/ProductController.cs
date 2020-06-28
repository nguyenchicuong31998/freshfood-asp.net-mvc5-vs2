using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Areas.Dashboard.Models;
using WebsiteThucPhamSach_VS2.Common;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Controllers
{
    public class ProductController : Controller
    {
        // GET: Dashboard/Products
        public ActionResult Index()
        {
            if (Session[SessionName.adminId] == null)
            {
                return RedirectToAction("Login", "Dashboard");
            }
            var products = new ProductsAdModel().getProducts();
            return View(products);
        }
        private void getMenus()
        {
            List<menu> menus = new MenusAdModel().getMenusActive();
            SelectList listMenus = new SelectList(menus, "id", "name");
            ViewBag.menus = listMenus;
        }

        public string getNameProviderById(int id)
        {
            var name = new ProductsAdModel().getNameByProviderId(id);
            return name;
        }

        private void getProviders()
        {
            List<provider> providers = new ProviderModel().getProvidersActive();
            SelectList listProviders = new SelectList(providers, "id", "name");
            ViewBag.providers = listProviders;
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.getMenus();
            this.getProviders();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ProductsAdModel product)
        {
            try
            {
                //if (product.image == "")
                //{
                //    ModelState.AddModelError("image", "Bạn vui lòng chọn hình ảnh sản phẩm");
                //    return View(product);
                //}
                //if (product.more_images == "")
                //{
                //    ModelState.AddModelError("image", "Bạn vui lòng chọn hình ảnh sản phẩm");
                //    return View(product);
                //}
                List<string> arrImages = new List<string>();
                if (ModelState.IsValid) {
                    var limitMoreImage = Request.Files.GetMultiple("more_images").Count();
                    if (limitMoreImage > 4)
                    {
                        ModelState.AddModelError("image", "Bạn vui lòng chọn hình ảnh");
                        ModelState.AddModelError("more_images", "Bạn vui lòng chọn tối đa 4 ảnh");
                        this.getMenus();
                        return View(product);
                    }
                    if (Request.Files.Count > 0)
                    {
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            if(Request.Files.GetKey(i) == "image")
                            {
                                HttpPostedFileBase file = Request.Files.Get(i);
                                if (file.ContentLength > 0)
                                {
                                    string fileName = Path.GetFileName(file.FileName);
                                    product.image = Path.Combine(Server.MapPath("~/Img/"), fileName);
                                    file.SaveAs(product.image);
                                    product.image = "~/Img/" + fileName;
                                }
                            }
                            if(Request.Files.GetKey(i) == "more_images")
                            {
                                HttpPostedFileBase file = Request.Files.Get(i);
                                if (file.ContentLength > 0)
                                {
                                    string fileName = Path.GetFileName(file.FileName);
                                    var linkFolder = Path.Combine(Server.MapPath("~/Img/"), fileName);
                                    file.SaveAs(linkFolder);
                                    string url = "~/Img/" + fileName;
                                    arrImages.Add(url);
                                }
                            }

                        }
                    }
                    product.more_images = new JavaScriptSerializer().Serialize(arrImages);
                    product.view_count = 0;
                    product.start_time = DateTime.Now;
                    product.status = true;
                    var createdProduct = new ProductsAdModel().createProduct(product);
                    if (createdProduct)
                    {
                        return Redirect(Request.UrlReferrer.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            this.getMenus();
            this.getProviders();
            return View(product);
        }

        public JsonResult getListMenuIds(int id)
        {
            var Ids = new ProductsAdModel().GetProductById(id).menu_id;
            return Json(new { data = Ids });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var product = new ProductsAdModel().GetProductById(id);
            ProductsAdModel productAd = new ProductsAdModel();
            productAd.id = product.id;
            productAd.code = product.code;
            productAd.name = product.name;
            productAd.description = product.description;
            productAd.image = product.image;
            productAd.more_images = product.more_images;
            productAd.detail = product.detail;
            productAd.keywords = product.keywords;
            productAd.includeVAT = product.includeVAT;
            productAd.price = product.price;
            productAd.price_promotion = product.price_promotion;
            productAd.provider_id = product.provider_id;
            productAd.start_time = product.start_time;
            productAd.total_product = product.total_product;
            this.getMenus();
            this.getProviders();
            return View(productAd);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductsAdModel product)
        {

            ModelState.Remove("image");
            ModelState.Remove("more_images");
            var producted = new ProductsAdModel().GetProductById(product.id);
            List<string> arrImages = new List<string>();
            if (ModelState.IsValid)
            {
                var limitMoreImage = Request.Files.GetMultiple("more_images").Count();
                if (limitMoreImage > 4)
                {
                    ModelState.AddModelError("image", "Bạn vui lòng chọn hình ảnh");
                    ModelState.AddModelError("more_images", "Bạn vui lòng chọn tối đa 4 ảnh");
                    this.getMenus();
                    this.getProviders();
                    return View(product);
                }
                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        if (Request.Files.GetKey(i) == "image")
                        {
                            HttpPostedFileBase file = Request.Files.Get(i);
                            if (file.ContentLength > 0)
                            {
                                string fileName = Path.GetFileName(file.FileName);
                                product.image = Path.Combine(Server.MapPath("~/Img/"), fileName);
                                file.SaveAs(product.image);
                                product.image = "~/Img/" + fileName;
                            }
                            else
                            {
                                product.image = producted.image;
                            }
                        }
                        if (Request.Files.GetKey(i) == "more_images")
                        {
                            HttpPostedFileBase file = Request.Files.Get(i);
                            if (file.ContentLength > 0)
                            {
                                string fileName = Path.GetFileName(file.FileName);
                                var linkFolder = Path.Combine(Server.MapPath("~/Img/"), fileName);
                                file.SaveAs(linkFolder);
                                string url = "~/Img/" + fileName;
                                arrImages.Add(url);
                            }
                        }
                    }
                    if (arrImages.Count() <= 0)
                    {
                        product.more_images = producted.more_images;
                    }
                    else
                    {
                        product.more_images = new JavaScriptSerializer().Serialize(arrImages);
                    }
                    product.start_time = DateTime.Now;

                }
                var updated = new ProductsAdModel().updateProductById(product.id, product);
                if (updated)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            product.image = producted.image;
            product.more_images = producted.more_images;
            this.getMenus();
            this.getProviders();
            return View(product);
        }

        public JsonResult Delete(int id)
        {
            var deleted = new ProductsAdModel().deleteById(id);
            if (deleted)
            {
                return Json(new { status = true });
            }
            return Json(new { status = false });
        }

        public JsonResult ChangeStatusById(int id)
        {
            var changedStatus = new ProductsAdModel().changeStatusById(id);
            if (changedStatus)
            {
                return Json(new { status = true });
            }
            return Json(new { status = false });
        }

    }
}