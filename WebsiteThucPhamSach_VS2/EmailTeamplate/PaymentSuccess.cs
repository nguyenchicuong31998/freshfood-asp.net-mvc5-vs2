using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThucPhamSach_VS2.Models;
namespace WebsiteThucPhamSach_VS2.EmailTeamplate
{
    public class PaymentSuccess
    {
        public string body(string display_name, List<order_details> order)
        {
            //, order_details orderDetail
            string content = "";
            content += "<div style='width:100%;height:500px'>";
            content += "<h1 style='text-align:center;'>Cảm ơn bạn đã đặt hàng.</h1>";
            content += "<p>Xìn chào: " + display_name + "</p>";
            content += "<p>Cảm ơn bạn vì đã mua hàng. Vui lòng xem đơn đặt hàng của bạn dưới đây.</p>";
            content += "<p>Đơn đặt hàng của bạn:</p>";
            content += "<table style='width:400px;height:100px; text-align:center'>";
                    content += "<tr style='background-color:#9bd031; color: white;'>";
                        content += "<th>Id</th>";
                        content += "<th>Tên sản phẩm</th>";
                        content += "<th>Số lượng</th>";
                        content += "<th>Đơn giá</th>";
                        content += "<th>Thành tiền</th>";
                content += "</tr>";
                decimal totalMoney = 0;
                foreach(var item in order) { 
                    content += "<tr>";
                        content += "<td>"+item.order_id+"</td>";
                        content += "<td>" + item.name + "</td>";
                        content += "<td>" + item.quantity + "</td>";
                        content += "<td>" + item.price + "</td>";
                        content += "<td>" + item.into_money + "</td>";
                     content += "</tr>";
                     totalMoney += decimal.Parse(item.into_money.ToString());
                }
            content += "</table>";
            content += "<div style='width:400px;text-align:right;'><b>Tổng tiền: " +  totalMoney + " VNĐ</b></div>";
            content += "<p>Trân trọng.</p>";
            content += "<p>FreshFood.</p>";
            content += "<p>0972612445.</p>";
            content += "<p style='color:black;  text-decoration:none;'>supportfreshfood@gmail.com.</p>";
            content += "</div>";
            return content;
        }
    }
}