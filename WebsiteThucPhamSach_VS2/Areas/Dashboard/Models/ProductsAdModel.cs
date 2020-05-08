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
        [Required(ErrorMessage = "Vui lòng nhập mô tả chi tiết sản phẩm")]
        public string detail { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập từ khóa sản phẩm")]
        public string keywords { get; set; }
        public Nullable<int> includeVAT { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm")]
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> price_promotion { get; set; }
        public Nullable<bool> top_hot { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập chọn danh mục sản phẩm")]
        public Nullable<int> menu_id { get; set; }
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
            product.price = productAd.price;
            product.price_promotion = productAd.price_promotion;
            product.top_hot = false;
            product.menu_id = int.Parse(productAd.menu_id.ToString());
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
    }
}