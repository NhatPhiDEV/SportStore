using ProjectWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductManagementContext context;

        public HomeController()
        {
            context = new ProductManagementContext();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection, CUSTOMER customer)
        {
            var name = collection["Name"];
            var sex = collection["Sex"];
            var phone = collection["Phone"];
            var address = collection["Address"];
            var email = collection["Email"];
            var username = collection["Username"];
            var password = collection["Password"];

            customer.CustomerName = name;
            customer.CustomerSex = sex;
            customer.CustomerPhone = phone;
            customer.CustomerAddress = address;
            customer.CustomerEmail = email;
            customer.Username = username;
            customer.Password = password;
            context.CUSTOMER.Add(customer);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //Get data product Especially
        public ActionResult GetProductEspecially()
        {
            var item = context.PRODUCT.Where(m=>m.ID > 50).Take(3).ToList();
            return PartialView(item);
        }
        //Get data new Product to View
        public ActionResult GetNewProduct()
        {
            var item = context.PRODUCT.Take(8).ToList();
            return PartialView(item);
        }

        //Get data Product Most
        public ActionResult GetProductMost()
        {
            var item = context.PRODUCT.OrderByDescending(p=>p.ProductCount).Take(4).ToList();
            return PartialView(item);
        }

        //Get detail Product
        public ActionResult getDetail(int ID)
        {
            var item = from it in context.PRODUCT where it.ID == ID select it;
            return View(item);
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var username = collection["Username"];
            var password = collection["Password"];

            CUSTOMER customer = context.CUSTOMER.SingleOrDefault(p => p.Username == username
            && p.Password == password);
            if(customer != null)
            {
                ViewBag.Message = "Chúc mừng bạn đăng nhập thành công";
                Session["Account"] = customer;
            }
            else
                ViewBag.Message = "Tên đăng nhập hoặc mật khẩu không đúng";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult DisplayLogin()
        {
            return PartialView();
        }
    }
}