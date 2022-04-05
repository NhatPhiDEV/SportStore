using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectWebsite.Models;

namespace ProjectWebsite.Controllers
{
    public class AdminController : Controller
    {
        ProductManagementContext context = new ProductManagementContext();
        public ActionResult AdminIndex()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View();
            }

        }
        public ActionResult LogOut()
        {
            Session["TaiKhoanAdmin"] = 0;
            return View("Login");
        }

        /// <summary>
        /// Thông tin đăng nhập của admin
        /// Author: Phan thanh tú (18/07/2021)
        /// </summary>
        /// <returns></returns>
        public ActionResult InfoAdmin()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return PartialView();
            }

        }

        /// <summary>
        /// Đăng nhập vào trang Admin
        /// Author: Phan thanh tú (18/07/2021)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var username = collection["txtUsername"];
            var password = collection["txtPassword"];

            if (String.IsNullOrEmpty(username))
            {
                TempData["Message"] = "Vui lòng nhập tài khoản!";
            }
            else if (String.IsNullOrEmpty(password))
            {
                TempData["Message"] = "Vui lòng nhập mật khẩu!";
            }
            else
            {
                ADMIN admin = context.ADMIN.SingleOrDefault(n => n.Username == username && n.Password == password);
                if (admin != null)
                {
                    Session["TaiKhoanAdmin"] = admin;
                    return RedirectToAction("AdminIndex", "Admin");
                }
                else
                    TempData["Message"] = "Thông tin đăng nhập không chính xác!";
            }
            return View();
        }
        /// <summary>
        /// Hàm quản lý khách hàng (Xóa + Tìm Kiếm) sử dụng Ajax + Jquery
        /// Author: Hồ Nhật Phi (23/07/2021)
        /// </summary>
        /// <returns></returns>
        #region Quản lý khách hàng
        // GET: Customers
        public ActionResult CustomersIndex()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View();
            }
        }
        //Hàm đẩy dử liệu lên client bằng json (Có tích hợp tính năng search)
        [HttpGet]
        public JsonResult GetData(string tukhoa, int trang)
        {

            context.Configuration.ProxyCreationEnabled = false;
            try
            {
                var pageSize = 15;
                var ListCUS = context.CUSTOMER.ToList();
                var Cus = ListCUS.Where(x => x.CustomerName.ToLower().Contains(tukhoa.ToLower()) || x.CustomerEmail.ToLower().Contains(tukhoa.ToLower()) ||
                x.CustomerPhone.ToLower().Contains(tukhoa.ToLower()) || x.Username.ToLower().Contains(tukhoa.ToLower())).ToList();
                //So dong trong trang

                //So trang
                var pageNumber = Cus.Count() % pageSize;
                if (pageNumber == 0)
                {
                    pageNumber = Cus.Count() / pageSize;
                }
                else
                {
                    pageNumber = Cus.Count() / pageSize + 1;
                }
                var kqtk = Cus.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                return Json(new
                {
                    code = 200,
                    ListCUS = kqtk,
                    Sotrang = pageNumber,
                    msg = "Lấy danh sách thành công!"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Lấy danh sách thất bại: " + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        // Hàm xóa dữ liệu
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var cus = context.CUSTOMER.SingleOrDefault(i => i.CustomerID == id);
                context.CUSTOMER.Remove(cus);
                context.SaveChanges();
                return Json(new
                {
                    code = 200,
                    msg = "Xóa khách hàng thành công!"
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Xóa khách hàng thất bại!" + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        /// <summary>
        /// Hàm quản lý sản phẩm (Thêm+ xóa+ sửa+ Tìm kiếm)
        /// </summary>
        /// <returns></returns>
        #region Quản lý sản phẩm

        public ActionResult ProductIndex()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View();
            }
        }
        //Hàm đẩy dử liệu lên client bằng json (Có tích hợp tính năng search)
        [HttpGet]
        public JsonResult GetProduct(string tukhoa, int trang)
        {
            context.Configuration.ProxyCreationEnabled = false;
            try
            {
                var pageSize = 15;
                var ListPro = context.PRODUCT.Select(c => new
                {
                    c.ID,
                    c.ProductName,
                    c.ProductPrice,
                    c.ProductImage,
                    c.ProductCount
                ,
                    c.PRODUCT_SIZE.SizeName,
                    c.PRODUCT_TYPE.TypeName,
                    c.PRODUCT_BRAND.BrandName,
                    c.PRODUCT_SALE.SaleName,
                    c.DateInsert,
                    c.Note
                }).ToList();
                var Pro = ListPro.Where(x => x.ProductName.ToLower().Contains(tukhoa.ToLower()) || x.BrandName.ToLower().Contains(tukhoa.ToLower())
                || x.ProductPrice.ToString().ToLower().Contains(tukhoa.ToLower())).ToList();
                //So dong trong trang

                //So trang
                var pageNumber = Pro.Count() % pageSize;
                if (pageNumber == 0)
                {
                    pageNumber = Pro.Count() / pageSize;
                }
                else
                {
                    pageNumber = Pro.Count() / pageSize + 1;
                }
                var kqtk = Pro.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                return Json(new
                {
                    code = 200,
                    ListPro = kqtk,
                    Sotrang = pageNumber,
                    msg = "Lấy danh sách thành công!"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Lấy danh sách thất bại: " + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        // Hàm thêm dữ liệu

        #region Các hàm load dữ liệu với json
        //Load All Brand
        [HttpGet]
        public JsonResult AllBrand()
        {
            try
            {
                var allbrand = context.PRODUCT_BRAND.Select(c => new { c.Brand, c.BrandName }).ToList();
                return Json(new { code = 200, allbrand = allbrand, msg = "Load danh sách thương hiệu thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Load danh sách thương hiệu thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //Load All Type
        [HttpGet]
        public JsonResult AllType()
        {
            try
            {
                var alltype = context.PRODUCT_TYPE.Select(c => new { c.Type, c.TypeName }).ToList();
                return Json(new { code = 200, alltype = alltype, msg = "Load danh sách loại sản phẩm thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Load danh sách loại sản phẩm thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //Load All Size
        [HttpGet]
        public JsonResult AllSize()
        {
            try
            {
                var allsize = context.PRODUCT_SIZE.Select(c => new { c.Size, c.SizeName }).ToList();
                return Json(new { code = 200, allsize = allsize, msg = "Load danh sách size thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Load danh sách size thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //Load All Sale
        [HttpGet]
        public JsonResult AllSale()
        {
            try
            {
                var allsale = context.PRODUCT_SALE.Select(c => new { c.Sale, c.SaleName }).ToList();
                return Json(new { code = 200, allsale = allsale, msg = "Load danh sách khuyến mãi thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Load danh sách khuyến mãi thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        // Load Detail (Chi tiết)
        [HttpGet]
        public JsonResult Detail(int id)
        {
            try
            {
                context.Configuration.ProxyCreationEnabled = false;
                var allprocduct = context.PRODUCT.Where(c => c.ID == id).FirstOrDefault();
                return Json(new { code = 200, allprocduct = allprocduct, msg = "Load chi tiết thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Load chi tiết thất bại: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion


        // Hàm xóa dữ liệu (sử dụng ajax)
        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            try
            {
                var pro = context.PRODUCT.SingleOrDefault(i => i.ID == id);
                context.PRODUCT.Remove(pro);
                context.SaveChanges();
                return Json(new
                {
                    code = 200,
                    msg = "Xóa sản phẩm thành công!"
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Xóa sản phẩm thất bại!" + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Thêm sản phẩm
        /// Author: Nhật Phi(24/04/2021)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddProduct(ProductViewModel obj)
        {
            try
            {

                PRODUCT item = new PRODUCT();
                var filename = obj.ProductImage;
                if (obj.ProductImage != null)
                {
                    filename.SaveAs(Server.MapPath("~/Content/images/") + Guid.NewGuid() + Path.GetExtension(filename.FileName));
                }
                item.ProductName = obj.ProductName;
                item.ProductPrice = decimal.Parse(obj.ProductPrice);
                item.ProductCount = obj.ProductCount;
                item.Size = obj.Size;
                item.Type = obj.Type;
                item.Brand = obj.Brand;
                item.Sale = obj.Sale;
                item.DateInsert = DateTime.Parse(obj.DateInsert.ToString());
                item.ProductImage = obj.ProductImage.FileName;
                context.PRODUCT.Add(item);
                context.SaveChanges();
                return Json(new
                {
                    code = 200,
                    msg = "Thêm sản phẩm thành công!"
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Thêm sản phẩm thất bại!" + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// Cập nhật sản phẩm
        /// Author: Nhật Phi (24/07/2021)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditProduct(ProductViewModel obj)
        {
            try
            {
                PRODUCT item = context.PRODUCT.FirstOrDefault(n => n.ID == obj.ID);
                var filename = obj.ProductImage;
                if (obj.ProductImage != null)
                {
                    filename.SaveAs(Server.MapPath("~/Content/images/") + Guid.NewGuid() + Path.GetExtension(filename.FileName));
                }
                item.ProductName = obj.ProductName;
                item.ProductPrice = decimal.Parse(obj.ProductPrice);
                item.ProductCount = obj.ProductCount;
                item.Size = obj.Size;
                item.Type = obj.Type;
                item.Brand = obj.Brand;
                item.Sale = obj.Sale;
                item.DateInsert = DateTime.Parse(obj.DateInsert.ToString());
                item.ProductImage = obj.ProductImage.FileName;
                context.SaveChanges();
                return Json(new
                {
                    code = 200,
                    msg = "Cập nhật sản phẩm thành công!"
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Cập nhật sản phẩm thất bại!" + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion


        #region Quản lý đơn hàng
        public ActionResult BillIndex()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public JsonResult GetBill(string tukhoa, int trang)
        {
            context.Configuration.ProxyCreationEnabled = false;
            try
            {
                var pageSize = 15;
                var ListBill = context.BILL.Select(c => new
                {
                    c.ID,
                    c.DateCreated,
                    c.CUSTOMER.CustomerName,
                    c.FullName,
                    c.Address,
                    c.Phone,
                    c.TotalMoney,
                    c.Status
                }).ToList();
                var Bill = ListBill.Where(x => x.FullName.ToLower().Contains(tukhoa.ToLower()) || x.CustomerName.ToLower().Contains(tukhoa.ToLower())).ToList();
                //So dong trong trang

                //So trang
                var pageNumber = Bill.Count() % pageSize;
                if (pageNumber == 0)
                {
                    pageNumber = Bill.Count() / pageSize;
                }
                else
                {
                    pageNumber = Bill.Count() / pageSize + 1;
                }
                var kqtk = Bill.Skip((trang - 1) * pageSize).Take(pageSize).ToList();
                return Json(new
                {
                    code = 200,
                    ListBill = kqtk,
                    Sotrang = pageNumber,
                    msg = "Lấy danh sách thành công!"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Lấy danh sách thất bại: " + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult ChangStatus(int id)
        {
            try
            {
                BILL bill = context.BILL.Where(c => c.ID == id).FirstOrDefault();
                if(bill.Status == 0)
                {
                    bill.Status = 1; 
                    context.SaveChanges();
                }
                else
                {
                    bill.Status = 0;
                    context.SaveChanges();
                }
                
                return Json(new
                {
                    code = 200,
                    msg = "Cập nhật trạng thái thành công!"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Cập nhật thất bại: " + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteBill(int id)
        {
            try
            {
                var bill = context.BILL.SingleOrDefault(i => i.ID == id);
                context.BILL.Remove(bill);
                context.SaveChanges();
                return Json(new
                {
                    code = 200,
                    msg = "Xóa đơn hàg thành công!"
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 500,
                    msg = "Xóa đơn hàng thất bại!" + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetTableDetails(int id)
        {
            context.Configuration.ProxyCreationEnabled = false;
            try
            {
                var item = context.PRODUCT_DETAIL.Select(c => new { c.BillID,c.PRODUCT.ProductName, c.PRODUCT.ProductImage, c.PRODUCT.PRODUCT_SIZE.SizeName, c.ProductCount, c.PRODUCT.ProductPrice }).Where(c => c.BillID == id).ToList();
                return Json(new
                {
                    code = 200,
                    itemdt = item,
                    msg = "Lấy danh sách thành công!"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = 500,
                    msg = "lấy danh sách thất bại!" + ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
            

        }
        #endregion

    }
}