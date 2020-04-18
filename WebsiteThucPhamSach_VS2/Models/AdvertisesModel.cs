using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;
namespace WebsiteThucPhamSach_VS2.Models
{
    public class AdvertisesModel
    {
        FreshFoodEntities db = new FreshFoodEntities();
        public advertis getLogo()
        {
            var advertis = db.advertises.SingleOrDefault(n => n.order == 1);
            if(advertis == null) {
                return null;
            }
            return advertis;
        }

        
    }
}