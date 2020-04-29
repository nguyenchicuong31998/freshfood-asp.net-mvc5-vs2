using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Vui lòng chọn hình sản phẩm")]
        public string image { get; set; }
        public string more_images { get; set; }
        public string detail { get; set; }
        public string keywords { get; set; }
        public Nullable<bool> includeVAT { get; set; }
        public Nullable<int> sort_order { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> price_promotion { get; set; }
        public Nullable<bool> top_hot { get; set; }
        public Nullable<int> menu_id { get; set; }
        public Nullable<System.DateTime> start_time { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tổng số lượng sản phẩm")]
        public Nullable<int> total_product { get; set; }
        public Nullable<int> total_sold { get; set; }
        public Nullable<bool> special { get; set; }
        public Nullable<int> view_count { get; set; }
        public Nullable<bool> status { get; set; }
        FreshFoodEntities db = new FreshFoodEntities();
        public List<product> getProducts()
        {
            return db.products.ToList();
        }
        
        public product GetProductById(int id)
        {
            var product = db.products.SingleOrDefault(p => p.id == id);
            return product != null ? product : null;
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
            product.sort_order = productAd.sort_order;
            product.price = productAd.price;
            product.price_promotion = productAd.price_promotion;
            product.top_hot = productAd.top_hot;
            product.menu_id = productAd.menu_id;
            product.start_time = productAd.start_time;
            product.total_product = productAd.total_product;
            product.total_sold = productAd.total_sold;
            product.special = productAd.special;
            product.view_count = productAd.view_count;
            product.status = productAd.status;
            db.products.Add(product);
            db.SaveChanges();
            return true;
        }
    }
}