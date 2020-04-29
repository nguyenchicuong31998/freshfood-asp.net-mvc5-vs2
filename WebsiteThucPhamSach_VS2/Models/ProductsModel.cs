using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;
using System.Data.Entity.SqlServer;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class ProductsModel
    {
        FreshFoodEntities db = new FreshFoodEntities();

        public List<product> getProducts()
        {
            return db.products.Where(p => p.status == true).OrderBy(r => Guid.NewGuid()).ToList();
        }
        public product getProductById(int id)
        {
            return db.products.SingleOrDefault(p => p.id == id);
        }

        public List<product> getProductsByNewStartTime()
        {
            return db.products.OrderByDescending(p => p.start_time).Where(p=>p.status == true).Take(12).ToList();
        }

        public List<product> getProductsByViewCount()
        {
            return db.products.OrderByDescending(p => p.view_count).Take(12).ToList();
        }

        
    }
}