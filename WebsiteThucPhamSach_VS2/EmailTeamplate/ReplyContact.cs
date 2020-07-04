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
            string content = "";
            content += "<div style='width:100%;height:500px'>";
            content += "<h1 style='text-align:center;'>Cảm ơn bạn đã liên hệ với chúng tôi.</h1>";
            content += "<p>Xìn chào: " + contact.full_name + ".</p>";
            content += "<p>Email: " + contact.email + ".</p>";
            content += "<p>Số điện thoại: " + contact.phone_number + ".</p>";
            content += "<p>Nội dung: " + contact.content + ".</p>";
            content += "<p>Trả lời: " + contact.reply + ".</p>";
            content += "<p>Trân trọng.</p>";
            content += "<p>FreshFood.</p>";
            content += "<p>0972612445.</p>";
            content += "<p style='color:black;  text-decoration:none;'>supportfreshfood@gmail.com.</p>";
            content += "</div>";
            return content;
        }
    }
}