using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;
namespace WebsiteThucPhamSach_VS2.EmailTeamplate
{
    public class PaymentSuccess
    {
        public string body(string display_name)
        {
            //, order_details orderDetail
            string content = "";
            content += "<div style='width:100%;height:500px'>";
            content += "<h1 style='text-align:center;'>Cảm ơn bạn đã đặt hàng.</h1>";
            content += "<p>Xìn chào: " + display_name + "</p>";
            content += "<p>Cảm ơn bạn vì đã mua hàng. Vui lòng xem đơn đặt hàng của bạn dưới đây.</p>";
            content += "<p>Đơn đặt hàng của bạn:</p>";
            content += "<p>Trân trọng.</p>";
            content += "<p>FreshFood.</p>";
            content += "<p>0972612445.</p>";
            content += "<p style='color:black;  text-decoration:none;'>supportfreshfood@gmail.com.</p>";
            content += "</div>";
            return content;
        }
    }
}