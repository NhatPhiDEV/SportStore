using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWebsite.Models;
using PagedList;
using PagedList.Mvc;

namespace ProjectWebsite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductManagementContext context;

        public ProductController()
        {
            context = new ProductManagementContext();
        }
        // GET: Product

        public ActionResult GetProduct(int ? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
           
            var item = context.PRODUCT.ToList();
            return View(item.ToPagedList(pageNum,pageSize));
        }

        //Get Category        
        public ActionResult GetProductType()
        {
            var item = context.PRODUCT_TYPE.ToList();
            return PartialView(item);
        }

        //Get Size
        public ActionResult GetProductSize()
        {
            var item = context.PRODUCT_SIZE.ToList();
            return PartialView(item);
        }
        //Get Brand
        public ActionResult GetProductBrand()
        {
            var item = context.PRODUCT_BRAND.ToList();
            return PartialView(item);
        }
        //Get ProductWithType

        public ActionResult GetProductWithType(int id, int? page)
        {
            var item = context.PRODUCT.Where(c => c.Type == id).ToList();
            int pageSize = 9;
            int pageNum = (page ?? 1);
            return View(item.ToPagedList(pageNum, pageSize));
        }

        public ActionResult GetProductWithSize(int id, int? page)
        {
            var item = context.PRODUCT.Where(c => c.Size == id).ToList();
            int pageSize = 9;
            int pageNum = (page ?? 1);
            return View(item.ToPagedList(pageNum, pageSize));
        }

        public ActionResult GetProductWithBrand(int id, int? page)
        {
            var item = context.PRODUCT.Where(c => c.Brand == id).ToList();
            int pageSize = 9;
            int pageNum = (page ?? 1);
            return View(item.ToPagedList(pageNum, pageSize));
        }       
    }
}