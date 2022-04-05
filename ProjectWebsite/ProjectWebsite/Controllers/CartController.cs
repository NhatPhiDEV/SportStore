using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWebsite.Models;

namespace ProjectWebsite.Controllers
{
    public class CartController : Controller
    {
        ProductManagementContext context;
        
        public CartController()
        {
            context = new ProductManagementContext();
        }

        //Lay Gio Hang
        public List<Cart> TakeCart()
        {
            List<Cart> listCart = Session["GioHang"] as List<Cart>;
            if(listCart == null)
            {
                //Neu gio hang khong ton tai thi khoi tao gio hang
                listCart = new List<Cart>();
                Session["GioHang"] = listCart;
            }
            return listCart;
        }

        //Them hang vao gio
        public ActionResult Addtocart(int ID, string strURL)
        {
            //Lay session gio hang
            List<Cart> listCart = TakeCart();
            //Kiem tra mat hang nay da ton tai trong gio hang chua
            Cart item = listCart.Find(i => i.ProductID == ID);
            if(item == null)
            {
                item = new Cart(ID);
                listCart.Add(item);
                return Redirect(strURL);
            }
            else
            {
                item.ProductCount++;
                return Redirect(strURL);
            }
        }

        //Tong so luong
        private int TotalCount()
        {
            int Icount = 0;
            List<Cart> listCart = Session["GioHang"] as List<Cart>;
            if(listCart != null)
            {
                Icount = listCart.Sum(i => i.ProductCount);
            }
            return Icount;
        }
        //Tinh tong tien
        private double TotalMoney()
        {
            double dTotalMoney = 0;
            List<Cart> listCart = Session["GioHang"] as List<Cart>;
            if(listCart != null)
            {
                dTotalMoney = listCart.Sum(i => i.IntoMoney);
            }
            return dTotalMoney;
        }

        private double TotalMoneyUSD()
        {
            double dTotalMoneyUSD = 0;
            List<Cart> listCart = Session["GioHang"] as List<Cart>;
            if (listCart != null)
            {
                dTotalMoneyUSD = (listCart.Sum(i => i.IntoMoney)) / 23000;
            }
            return double.Parse(string.Format(new CultureInfo("en-US"), "{0:#,00}", dTotalMoneyUSD));
        }
        //Xay dung gio hang
        public ActionResult Cart()
        {          
            List<Cart> listCart = TakeCart();
            ViewBag.TotalCount = TotalCount();
            ViewBag.TotalMoney = TotalMoney();
            return PartialView(listCart);
        }

        //Xay dung gio hang partial()
        public ActionResult CartPartial()
        {            
            ViewBag.TotalCount = TotalCount();
            ViewBag.TotalMoney = TotalMoney();
            return PartialView();
        }
        //Xoa gio hang
        public ActionResult DeleteCart(int ID)
        {
            List<Cart> listCart = TakeCart();
            //Kiem tra product da co trong Session["Cart"] hay chua
            Cart item = listCart.SingleOrDefault(i => i.ProductID == ID);

            if(item != null)
            {
                listCart.RemoveAll(i => i.ProductID == ID);
                return RedirectToAction("Index", "Home");
            }
            if(listCart.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }           
        }
        //Cap nhat gio hang
        [HttpPost]
        public ActionResult EditCart(int ? ID, FormCollection collection)
        {
            List<Cart> listCart = TakeCart();
            string count = collection["txtCount"];
            string size = collection["size"];
            Cart item = listCart.SingleOrDefault(i => i.ProductID == ID);
            if(item != null && count !=null && size!=null)
            {
                item.ProductSize = size;
                item.ProductCount = int.Parse(count);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }       
        //Xoa san pham trong view dat hang
        public ActionResult DeleteCartOder(int ID)
        {
            List<Cart> listCart = TakeCart();
            //Kiem tra product da co trong Session["Cart"] hay chua
            Cart item = listCart.SingleOrDefault(i => i.ProductID == ID);
            if (item != null)
            {
                listCart.RemoveAll(i => i.ProductID == ID);
                return RedirectToAction("Oder","Cart");
            }
            if (listCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }           
            return RedirectToAction("Oder", "Cart");
        }
        public ActionResult DeleteAll()
        {
            Session["GioHang"] = 0;
            return RedirectToAction("Index", "Home");
        }

        //Dat hang 
        [HttpGet]
        public ActionResult Oder()
        {
            //Kiem tra dang nhap chua
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }
            if (Session["GioHang"] == null || Session["GioHang"].ToString() == "")
            {
                return RedirectToAction("Index", "Home");
            }
            List<Cart> listCart = TakeCart();
            ViewBag.TotalCount = TotalCount();
            ViewBag.TotalMoneyUSD = TotalMoneyUSD();
            ViewBag.TotalMoney = TotalMoney();
            return View(listCart);
        }
        [HttpPost]
        public ActionResult Oder(FormCollection collection)
        {
            ViewBag.SumMoney = TotalMoney();
            BILL bill = new BILL();
            CUSTOMER customer = (CUSTOMER)Session["TaiKhoan"];
            List<Cart> cart = TakeCart();
            bill.CustomerID = customer.CustomerID;
            bill.DateCreated = DateTime.Now;
            bill.TotalMoney = (decimal)ViewBag.SumMoney;
            var status = 0;
            var name = collection["fullname"];
            var address = collection["address"];
            var phone = collection["phone"];

            bill.Status = status;
            bill.FullName = name;
            bill.Phone = phone;
            bill.Address = address;

            context.BILL.Add(bill);
            context.SaveChanges();
            foreach (var iteam in cart)
            {
                PRODUCT_DETAIL detail = new PRODUCT_DETAIL();
                detail.BillID = bill.ID;
                detail.ProductID = iteam.ProductID;
                detail.ProductCount = iteam.ProductCount;
                detail.Note = iteam.ProductSize;
                context.PRODUCT_DETAIL.Add(detail);
            }
            context.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("BillCompleted", "Cart");

        }
        public ActionResult BillCompleted()
        {
            return View();
        }
       
    }
}