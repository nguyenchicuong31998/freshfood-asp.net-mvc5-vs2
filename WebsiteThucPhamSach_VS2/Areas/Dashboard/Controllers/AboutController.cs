using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Areas.Dashboard.Models;
using WebsiteThucPhamSach_VS2.Models;

namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Controllers
{
    public class AboutController : Controller
    {
        // GET: Dashboard/About
        public ActionResult Index()
        {
            var abouts = new AboutsModel().getAbouts();
            return View(abouts);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(about newAbout)
        {
            newAbout.content = newAbout.content;
            newAbout.start_time = DateTime.Now;
            newAbout.status = false;
            var created = new AboutsModel().createAbout(newAbout);
            if (created)
            {
                return RedirectToAction("Index", "About");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var about = new AboutsModel().getAboutById(id);
            return View(about);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(about updateAbout)
        {
                var updated = new AboutsModel().updateAboutById(updateAbout.id, updateAbout);
                if (updated)
                {
                    return RedirectToAction("Index", "About");
                }
            return View();
        }

        public JsonResult Delete(int id)
        {

            var deleted = new AboutsModel().deleteAboutById(id);
            if(deleted != true)
            {
                return Json(new
                {
                    status = false
                });
            }
            return Json(new
            {
                status = true
            });
        }

        public JsonResult ChangeStatus(int id)
        {
            var changed = new AboutsModel().changeStatusById(id);
            if (changed)
            {
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
    }
}