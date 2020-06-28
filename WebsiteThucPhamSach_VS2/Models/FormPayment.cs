using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class FormPayment
    {
        public int id { get; set; }
        public string name { get; set; }

        public FormPayment()
        {

        }
        public FormPayment(int _id, string _name)
        {
            this.id = _id;
            this.name = _name;
        }
    }
}