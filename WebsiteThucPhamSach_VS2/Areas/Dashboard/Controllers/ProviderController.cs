using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;

namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Controllers
{
    public class ProviderController : Controller
    {
        // GET: Dashboard/Provider
        public ActionResult Index()
        {
            var providers = new ProviderModel().getProviders();
            return View(providers);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProviderModel provider)
        {
            if (ModelState.IsValid)
            {
                var created = new ProviderModel().create(provider);
                if (created)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            return View(provider);
        }

        public ActionResult Edit(int id)
        {
            var provider = new ProviderModel().getProviderById(id);
            ProviderModel providerModel = new ProviderModel();
            providerModel.name = provider.name;
            providerModel.email = provider.email;
            providerModel.address = provider.address;
            providerModel.phone_number = provider.phone_number;
            providerModel.description = provider.description;
            providerModel.link_google_map = provider.link_google_map;
            return View(providerModel);
        }

        [HttpPost]
        public ActionResult Edit(ProviderModel provider)
        {
            if (ModelState.IsValid)
            {
                var updated = new ProviderModel().updateProviderById(provider.id, provider);
                if (updated)
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            return View(provider);
        }

        public JsonResult ChangeStatus(int id)
        {
            var changed = new ProviderModel().changeStatusById(id);
            if (changed)
            {
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteMenuById(int id)
        {
            var deleted = new ProviderModel().DeleteById(id);
            if (deleted)
            {
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }
    }
}