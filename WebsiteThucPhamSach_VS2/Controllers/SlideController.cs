using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
namespace WebsiteThucPhamSach_VS2.Controllers
{
    public class SlideController : Controller
    {
        // GET: Slide
        public PartialViewResult SlidePartial()
        {
            var slide = new SlidesModel().GetAdvertises();
            return PartialView("SlidePartial",slide);
        }

        public string getAdvertisByOrder(int order)
        {
            var advertis = new SlidesModel().getAdvertisByOrder(order);
            if(advertis == null)
            {
                return " ";
            }
            return advertis.url != "" ? advertis.url : " ";
        }
    }
}