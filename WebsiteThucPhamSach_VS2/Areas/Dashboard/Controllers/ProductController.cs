using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Areas.Dashboard.Models;
using WebsiteThucPhamSach_VS2.Common;
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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProductsAdModel product)
        {
            try
            {
                if (ModelState.IsValid) { 
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase file = Request.Files[0];
                        if (file.ContentLength > 0)
                        {
                            string fileName = Path.GetFileName(file.FileName);
                            product.image = Path.Combine(Server.MapPath("~/Img/"), fileName);
                            file.SaveAs(product.image);
                            product.image = "~/Img/" + fileName;
                        }
                    }
                    product.view_count = 0;
                    product.start_time = DateTime.Now;
                    product.status = true;
                    var createdProduct = new ProductsAdModel().createProduct(product);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return View(product);
        }


        public ActionResult Edit(int id)
        {
            
            return View();
        }
    }
}