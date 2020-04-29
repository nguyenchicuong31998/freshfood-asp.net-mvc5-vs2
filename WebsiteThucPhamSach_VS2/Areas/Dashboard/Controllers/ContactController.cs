using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;
using WebsiteThucPhamSach_VS2.EmailTeamplate;
namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Controllers
{
    public class ContactController : Controller
    {
        // GET: Dashboard/Contact
        Utils utils = new Utils();
        public ActionResult Index()
        {
            var contacts = new ContactsModel().getContactses();
            return View(contacts);
        }

        public ActionResult View(int id)
        {
            var contact = new ContactsModel().getContactById(id);
            if(contact == null)
            {
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public ActionResult Reply(int id)
        {
            var contact = new ContactsModel().getContactById(id);
            if (contact == null)
            {
                return RedirectToAction("Index");
            }
            return View(contact);
        }
        [HttpPost]
        public ActionResult Reply(contact contact)
        {
            try
            {
                var contacted = new ContactsModel().updateContactById(contact.id, contact);
                if (contacted != null)
                {
                    var body = new ReplyContact().body(contacted);
                    var isTrue = utils.SendEmail(contacted.email, "Trả lời liên hệ", body, "", "");
                    if (isTrue)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return View(contact);
        }

        public JsonResult Delete(int id)
        {
            var deleted = new ContactsModel().DeleteById(id);
            if (deleted)
            {
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false
            }, JsonRequestBehavior.AllowGet);
        }
    }
}