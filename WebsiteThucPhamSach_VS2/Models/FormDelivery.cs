using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteThucPhamSach_VS2.Models
{
    public class FormDelivery
    {
        public int id { get; set; }
        public string name { get; set; }

        public FormDelivery()
        {

        }
        public FormDelivery(int _id, string _name)
        {
            this.id = _id;
            this.name = _name;
        }
    }
}