using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Common;
using WebsiteThucPhamSach_VS2.Models;
namespace WebsiteThucPhamSach_VS2.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public PartialViewResult RatingStar(int id)
        {
            ViewBag.id = id;
            ViewBag.totalFeedback = new FeedbacksModel().getTotalFeedbackByProductId(id);
            float totalRate = new FeedbacksModel().getTotalFeedbackByProductId(id);
            float totalStar = new FeedbacksModel().getTotalStarByProductId(id);
            ViewBag.AvgStar = float.IsNaN(totalStar / totalRate) ? 0 : (totalStar / totalRate);
            return PartialView();
        }
    }
}