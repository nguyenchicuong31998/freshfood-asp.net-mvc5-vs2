using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Common;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Controllers
{
    public class BillController : Controller
    {
        // GET: Dashboard/Bill
        FreshFoodEntities db = new FreshFoodEntities();
        Utils utils = new Utils();
        public ActionResult Index()
        {
            var orders = db.orders.OrderBy(h=>h.start_time).ToList();
            return View(orders);
        }

        public JsonResult orderDetail(int orderId)
        {
            var order = db.orders.First(n => n.id == orderId);
            var id = order.id;
            var display_name = order.display_name;
            var orderDetail = db.order_details.Where(n => n.order_id == orderId).ToList();
            return Json(new { id = id, name = display_name, orderDetail = orderDetail });
        }

        public JsonResult changeStatus(int orderId)
        {
            var order = db.orders.FirstOrDefault(o => o.id == orderId);
            order.status = !order.status;
            order.end_time = DateTime.Now.Date;
            UpdateModel(order);
            db.SaveChanges();
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cancel(int orderId)
        {
            var orderDetail = db.order_details.Where(od => od.order_id == orderId).ToList();
            foreach (var item in orderDetail)
            {
                db.order_details.Remove(item);
                var product = db.products.SingleOrDefault(p => p.id == item.product_id);
                product.total_product += item.quantity;
                UpdateModel(product);
                db.SaveChanges();
            }
            var order = db.orders.SingleOrDefault(o => o.id == orderId);
            db.orders.Remove(order);
            db.SaveChanges();
            return Json(new
            {
                status = true
            });
        }

        public ActionResult ExportExcel()
        {
            var data = db.orders.OrderBy(h => h.start_time).ToList();
            var gv = new GridView();
            gv.DataSource = data;
            gv.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Báo cáo hóa đơn "+DateTime.Now.Day+"-"+ DateTime.Now.Month+ "-"+ DateTime.Now.Year+".xls");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            for (int i = 0; i < gv.Rows.Count; i++)
            {
                //Apply text style to each Row
                gv.Rows[i].Attributes.Add("class", "textmode");
                gv.Rows[i].Cells[0].Text = (i + 1).ToString(); ;
                gv.Rows[i].Cells.RemoveAt(11);
            }
            //Add màu nền cho header của file excel
            gv.HeaderRow.BackColor = System.Drawing.Color.DarkGreen;
            //Màu chữ cho header của file excel
            gv.HeaderStyle.ForeColor = System.Drawing.Color.White;

            gv.HeaderRow.Cells[0].Text = "Stt";
            gv.HeaderRow.Cells[1].Text = "Id";
            gv.HeaderRow.Cells[2].Text = "Họ và tên";
            gv.HeaderRow.Cells[3].Text = "Số điện thoại";
            gv.HeaderRow.Cells[4].Text = "Địa chỉ";
            gv.HeaderRow.Cells[5].Text = "Email";
            gv.HeaderRow.Cells[6].Text = "Tổng tiền";
            gv.HeaderRow.Cells[7].Text = "Ngày đặt";
            gv.HeaderRow.Cells[8].Text = "Ngày xác nhận";
            gv.HeaderRow.Cells[9].Text = "Hình thức thanh toán";
            gv.HeaderRow.Cells[10].Text = "Hình thức giao hàng";
            gv.HeaderRow.Cells[11].Visible = false;

            gv.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            var orders = db.orders.OrderBy(h => h.start_time).ToList();
            return View(orders);
        }
    }
}