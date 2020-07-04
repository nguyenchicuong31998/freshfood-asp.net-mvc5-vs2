using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;
namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Models
{
    public class ProductsAdModel
    {
        public int id { get; set; }
        public string code { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string name { get; set; }
        public string description { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn hình ảnh sản phẩm")]
        public string image { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn hình ảnh chi tiết sản phẩm")]
        public string more_images { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mô tả chi tiết sản phẩm")]
        public string detail { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập từ khóa sản phẩm")]
        public string keywords { get; set; }
        public Nullable<int> includeVAT { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> price_promotion { get; set; }
        public Nullable<bool> top_hot { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn danh mục sản phẩm")]
        public string[] menu_id { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp")]
        public Nullable<int> provider_id { get; set; }
        public Nullable<System.DateTime> start_time { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tổng số lượng sản phẩm")]
        public Nullable<int> total_product { get; set; }
        public Nullable<int> total_sold { get; set; }
        public Nullable<bool> special { get; set; }
        public Nullable<int> view_count { get; set; }
        public Nullable<bool> status { get; set; }
        public virtual menu menu { get; set; }

        FreshFoodEntities db = new FreshFoodEntities();
        public List<product> getProducts()
        {
            return db.products.OrderByDescending(p=>p.start_time).ToList();
        }
        
        public product GetProductById(int id)
        {
            var product = db.products.SingleOrDefault(p => p.id == id);
            return product != null ? product : null;
        }

        public string getNameByProviderId(int id)
        {
            var provider = new ProviderModel().getProviderActiveById(id);
            return provider.name != "" ? provider.name : "";
        }

        public int getTotalProducts()
        {
            int totalProducts = db.products.Count();
            return totalProducts == null ? 0 : totalProducts;
        }

        public int totalActiveProducts()
        {
            return db.products.Where(p=>p.status == true).Count();
        }

        public int totalInActiveProducts()
        {
            return db.products.Where(p => p.status == false).Count();
        }

        public bool createProduct(ProductsAdModel productAd)
        {
            var product = new product();
            product.code = productAd.code;
            product.name = productAd.name;
            product.description = productAd.description;
            product.image = productAd.image;
            product.more_images = productAd.more_images;
            product.detail = productAd.detail;
            product.keywords = productAd.keywords;
            product.includeVAT = productAd.includeVAT;
            product.price = productAd.price;
            product.price_promotion = productAd.price_promotion;
            product.top_hot = false;
            product.menu_id = JsonConvert.SerializeObject(productAd.menu_id);
            product.provider_id = int.Parse(productAd.provider_id.ToString());
            product.start_time = productAd.start_time;
            product.total_product = productAd.total_product;
            product.total_sold = 0;
            product.special = false;
            product.view_count = 0;
            product.status = true;
            db.products.Add(product);
            db.SaveChanges();
            return true;
        }

        public bool updateProductById(int id, ProductsAdModel productAd)
        {
            try
            {
                var product = this.GetProductById(id);
                if(product != null)
                {
                    product.code = productAd.code;
                    product.name = productAd.name;
                    product.description = productAd.description;
                    product.image = productAd.image;
                    product.more_images = productAd.more_images;
                    product.detail = productAd.detail;
                    product.keywords = productAd.keywords;
                    product.includeVAT = productAd.includeVAT;
                    product.price = productAd.price;
                    product.price_promotion = productAd.price_promotion;
                    product.menu_id = JsonConvert.SerializeObject(productAd.menu_id);
                    product.provider_id = int.Parse(productAd.provider_id.ToString());
                    product.start_time = productAd.start_time;
                    product.total_product = productAd.total_product;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {

            }
            return false;
        }

        public bool deleteById(int id)
        {
            try
            {
                var product = this.GetProductById(id);
                if (product != null)
                {
                    db.products.Remove(product);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool changeStatusById(int id)
        {
            try
            {
                var product = this.GetProductById(id);
                if (product != null)
                {
                    product.status = !product.status;
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}