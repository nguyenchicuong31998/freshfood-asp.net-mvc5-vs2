using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Controllers
{
    public class EventController : Controller
    {
        // GET: Dashboard/Event
        public ActionResult Index()
        {
            var events = new EventsModel().getEvents();
            return View(events);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EventsModel @event)
        {
            return View(@event);
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(EventsModel @event)
        {
            return View(@event);
        }


        public JsonResult ChangeTopHotById(int id)
        {
            var changedTopHot = new EventsModel().changeTopHotById(id);
            if (changedTopHot)
            {
                return Json(new { status = true });
            }
            return Json(new { status = false });
        }

        public JsonResult ChangeStatusById(int id)
        {
            var changedStatus = new EventsModel().changeStatusById(id);
            if (changedStatus)
            {
                return Json(new { status = true });
            }
            return Json(new { status = false });
        }

        public JsonResult Delete(int id)
        {
            var deleted = new EventsModel().deleteById(id);
            if (deleted)
            {
                return Json(new { status = true });
            }
            return Json(new { status = false });
        }


        public string GetNameAdminById(int id)
        {
            var admin = new EventsModel().getAdminById(id);
            if(admin != null)
            {
                return admin.full_name;
            }
            return "";
        }

    }
}