using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteThucPhamSach_VS2.EmailTeamplate
{
    public class ForgotPassword
    {
        public string body(string display_name, string newPassword)
        {
            string content = "";
            content += "<div style='width:100%;height:500px'>";
            content += "<h1 style='text-align:center;'>Quên mật khẩu.</h1>";
            content += "<p>Xìn chào: " + display_name + "</p>";
            content += "<p>Đây là mật khẩu mới của bạn: <b>" + newPassword + "</b></p>";
            content += "<p>Trân trọng.</p>";
            content += "<p>FreshFood.</p>";
            content += "<p>0972612445.</p>";
            content += "<p style='color:black;  text-decoration:none;'>supportfreshfood@gmail.com.</p>";
            content += "</div>";
            return content;
        }
    }
}