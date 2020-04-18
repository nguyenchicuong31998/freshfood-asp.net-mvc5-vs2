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
            string content = "<h1>Mật khẩu của bạn đã được thay đổi.</h1>";
            content += "<p>Xìn chào: "+ display_name + "</p>";
            content += "<p>Đây là mật khẩu mới của bạn: " + newPassword + "</p>";
            content += "<p>Trân trọng.</p>";
            content += "<p>FreshFood.</p>";
            return content;
        }
    }
}