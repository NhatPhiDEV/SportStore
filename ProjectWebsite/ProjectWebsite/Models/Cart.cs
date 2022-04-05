using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectWebsite.Models
{
    public class Cart
    {
        ProductManagementContext context = new ProductManagementContext();

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Double ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int ProductCount { get; set; }

        public string ProductSize { get; set; }

        public Double IntoMoney
        {
            get { return ProductCount * ProductPrice; }
        }

        //CONTRUCSTOR
        public Cart(int ID)
        {
            ProductID = ID;
            PRODUCT item = context.PRODUCT.Single(i=>i.ID == ProductID);
            ProductName = item.ProductName;
            ProductImage = item.ProductImage;
            ProductSize = item.PRODUCT_SIZE.SizeName;
            ProductCount = 1;
            ProductPrice = double.Parse(item.ProductPrice.ToString());
        }

    }
}