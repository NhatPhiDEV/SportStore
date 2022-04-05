using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWebsite.Models;

namespace ProjectWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly ProductManagementContext context;

        public AccountController()
        {
            context = new ProductManagementContext();
        }
        // GET: Account
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection collection, CUSTOMER cus)
        {
            var fullname = collection["name"];
            var user = collection["username"];
            var sex = collection["gender"];
            var pass = collection["password"];
            var address = collection["address"];
            var email = collection["email"];
            var phone = collection["phone"];
            var birthay = String.Format("{0:MM/dd/yyyy}", collection["birthday"]);
            if (user != null && pass != null)
            {
                cus.CustomerName = fullname;
                cus.Username = user;
                cus.Password = pass;
                cus.CustomerSex = sex;
                cus.CustomerAddress = address;
                cus.Birthday = DateTime.Parse(birthay);
                cus.CustomerPhone = phone;
                cus.CustomerEmail = email;

                context.CUSTOMER.Add(cus);
                context.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return this.Register();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var username = collection["username"];
            var password = collection["pass"];

            CUSTOMER Cus = context.CUSTOMER.SingleOrDefault(n => n.Username == username && n.Password == password);
            if (Cus != null)
            {
                ViewBag.Message = "Đăng nhập thành công!";
                Session["Name"] = Cus.CustomerName;
                Session["TaiKhoan"] = Cus;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Tên tài khoản hoặc mật khẩu không đúng!";
            }
            return this.Login();
        }

        public ActionResult NameUser()
        {
            return PartialView();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}