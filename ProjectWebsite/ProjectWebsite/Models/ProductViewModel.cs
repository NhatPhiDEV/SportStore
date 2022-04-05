using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebsite.Models
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public int ProductCount { get; set; }
        public int Size { get; set; }
        public int Type { get; set; }
        public int Brand { get; set; }
        public int Sale { get; set; }
        public string DateInsert { get; set; }
        public HttpPostedFileWrapper ProductImage { get; set; }

    }
}