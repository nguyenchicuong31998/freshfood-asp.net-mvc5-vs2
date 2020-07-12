using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebsiteThucPhamSach_VS2.Models;
using WebsiteThucPhamSach_VS2.Areas.Dashboard.Models;
namespace WebsiteThucPhamSach_VS2.Areas.Dashboard.Controllers
{
    public class StatisticalController : Controller
    {
        // GET: Dashboard/Statistical
        FreshFoodEntities db = new FreshFoodEntities();
        GlobalVariables global = new GlobalVariables();
        public ActionResult Index()
        {
            var year = DateTime.Now.Year;
            List<int> ItemMonth = new List<int>();
            for (int i = 1; i < 13; i++)
            {
                ItemMonth.Add(i);
            }
            List<int> ItemYear = new List<int>();
            for (int i = year; i >= 2010; i--)
            {
                ItemYear.Add(i);
            }
            SelectList itemMonth = new SelectList(ItemMonth);
            SelectList itemYear = new SelectList(ItemYear);
            ViewBag.ItemMonth = itemMonth;
            ViewBag.ItemYear = itemYear;
            return View();
        }


        public PartialViewResult ListStatisticalPartial(int month, int year)
        {
            var listStatistical = db.orders.Where(n => n.start_time.Value.Month == month && n.start_time.Value.Year == year).OrderBy(n => n.start_time).ToList();
            if (listStatistical.Count() > 0)
            {
                Session["order"] = listStatistical;
            }
            else
            {
                Session["order"] = null;
            }
    
            ViewBag.listStatistical = listStatistical;
            return PartialView();
        }

        public ActionResult ExportExcel()
        {
            if (Session["order"] == null)
            {
                return View(db.orders.OrderBy(h => h.start_time).ToList());
            }
            var data = Session["order"];
            var gv = new GridView();
            gv.DataSource = data;
            gv.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Báo cáo hóa đơn " + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".xls");
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