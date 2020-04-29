using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;
namespace WebsiteThucPhamSach_VS2.EmailTeamplate
{
    public class ReplyContact
    {
        public string body(contact contact)
        {
            string content = "<h1>Trả lời liên hệ.</h1>";
            content += "<p>Xìn chào: " + contact.full_name + ".</p>";
            content += "<p>Email: " + contact.email + ".</p>";
            content += "<p>Số điện thoại: " + contact.phone_number + ".</p>";
            content += "<p>Nội dung: " + contact.content + ".</p>";
            content += "<p>Trả lời: " + contact.reply + ".</p>";
            content += "<p>Trân trọng.</p>";
            content += "<p>FreshFood.</p>";
            return content;
        }
    }
}