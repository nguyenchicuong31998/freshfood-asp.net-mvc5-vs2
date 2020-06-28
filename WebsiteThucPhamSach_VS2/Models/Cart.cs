using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class Cart
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public Decimal price { get; set; }
        public Decimal price_promotion { get; set; }
        public int quantity { get; set; }
        public Decimal totalMoney
        {
            get {
                return price * quantity;
            }
        }
    }
}