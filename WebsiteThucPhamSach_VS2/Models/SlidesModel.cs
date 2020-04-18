using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class SlidesModel
    {
        FreshFoodEntities db = new FreshFoodEntities();
        public List<advertis> GetAdvertises()
        {
            return db.advertises.ToList();
        }

        public advertis getAdvertisByOrder(int order)
        {
            var advertis = db.advertises.SingleOrDefault(n => n.order == order &&  n.status == true);
           return advertis != null ? advertis : null;
        }
    }
}