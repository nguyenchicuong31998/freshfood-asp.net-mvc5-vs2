using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;

namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Dashboard/Feedback
        public ActionResult Index()
        {
            var feedbacks = new FeedbacksModel().getFeedbacks();
            return View(feedbacks);
        }

        public ActionResult View(int id)
        {
            var feedback = new FeedbacksModel().getFeedbackById(id);
            if (feedback == null)
            {
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        public JsonResult Delete(int id)
        {
            var deleted = new FeedbacksModel().deleteById(id);
            if (deleted)
            {
                return Json(
                    new
                    {
                        status = true
                    }
                );
            }
            return Json(
                new
                {
                    status = false
                }
            );
        }

        public string GetNameUserById(int id)
        {
            var userName = new FeedbacksModel().getNameUserById(id);
            return userName;
        }

        public string GetNameProductById(int id)
        {
            var productName = new FeedbacksModel().getNameProductById(id);
            return productName;
        }

        public JsonResult ChangeStatusById(int id)
        {
            var changed = new FeedbacksModel().changeStatusById(id);
            if (changed)
            {
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }
    }
}