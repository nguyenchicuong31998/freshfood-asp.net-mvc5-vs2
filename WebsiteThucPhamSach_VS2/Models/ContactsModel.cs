using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;

namespace WebsiteThucPhamSach_VS2.Models
{

    public class ContactsModel
    {

        public int id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string full_name { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string phone_number { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string content { get; set; }
        public string reply { get; set; }

        FreshFoodEntities db = new FreshFoodEntities();

        public List<contact> getContactses()
        {
            return db.contacts.ToList();
        }

        public contact getContactById(int id)
        {
            var contact = db.contacts.SingleOrDefault(c => c.id == id);
            if (contact != null)
            {
                return contact;
            }
            return null;
        }
        public bool create(ContactsModel newContact)
        {
            try {
                contact contact = new contact();
                contact.full_name = newContact.full_name;
                contact.email = newContact.email;
                contact.phone_number = newContact.phone_number;
                contact.content = newContact.content;
                contact.start_time = DateTime.Now;
                contact.status = false;
                db.contacts.Add(contact);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public contact updateContactById(int id, contact contact)
        {
            try
            {
                var currentContact = this.getContactById(id);
                if(currentContact != null)
                {
                    currentContact.full_name = contact.full_name;
                    currentContact.email = contact.email;
                    currentContact.phone_number = contact.phone_number;
                    currentContact.content = contact.content;
                    currentContact.reply = contact.reply;
                    currentContact.status = true;
                    currentContact.end_time = DateTime.Now;
                    db.Entry(currentContact).State = EntityState.Modified;
                    db.SaveChanges();
                    return currentContact;
                }
                return null;
            }catch(Exception e)
            {
                throw e;
            }
        }
        public bool DeleteById(int id)
        {
            var contact = this.getContactById(id);
            if(contact != null)
            {
                db.contacts.Remove(contact);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public contact changStatusById()
        {
            try
            {

            }catch(Exception e)
            {
                throw e;
            }
            return null;
        }
    }
}