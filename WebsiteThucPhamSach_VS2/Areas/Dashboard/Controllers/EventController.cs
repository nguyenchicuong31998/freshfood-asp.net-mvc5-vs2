using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;
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
        [ValidateInput(false)]
        public ActionResult Create(EventsModel @event)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                       string fileName = Path.GetFileName(file.FileName);
                       @event.image = Path.Combine(Server.MapPath("~/Img/"), fileName);
                       file.SaveAs(@event.image);
                       @event.image = "~/Img/" + fileName;
                    }
                }
                @event.admin_id = int.Parse(Session[SessionName.adminId].ToString());
                var created = new EventsModel().create(@event);
                if (created)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            return View(@event);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var @event = new EventsModel().getEventById(id);
            if(@event == null)
            {

            }
            EventsModel eventModel = new EventsModel();
            eventModel.image = @event.image;
            eventModel.title = @event.title;
            eventModel.content = @event.content;
            return View(eventModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(EventsModel @event)
        {

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                if (file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    @event.image = Path.Combine(Server.MapPath("~/Img/"), fileName);
                    file.SaveAs(@event.image);
                    @event.image = "~/Img/" + fileName;
                }
                else
                {
                    var evented = new EventsModel().getEventById(@event.id);
                    @event.image = evented.image;
                }
            }
            @event.admin_id = int.Parse(Session[SessionName.adminId].ToString());
            var updated = new EventsModel().updateById(@event.id, @event);
            if (updated)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
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