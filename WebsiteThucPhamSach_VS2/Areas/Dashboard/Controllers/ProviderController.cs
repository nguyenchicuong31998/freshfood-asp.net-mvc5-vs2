using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using QRCoder;
using System.Drawing;
using System.IO;

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

        public string GennerateQRCode(string address)
        {
            var url = "";
            QRCodeGenerator objectQR = new QRCodeGenerator();
            string data = "https://www.google.com/maps/place/" + Server.UrlEncode(address);
            QRCodeData qrCodeData = objectQR.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            Bitmap bitMap = new QRCode(qrCodeData).GetGraphic(20);
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();
                url = String.Format("data:image/png;base64," + Convert.ToBase64String(byteImage));

            }
            return url;
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