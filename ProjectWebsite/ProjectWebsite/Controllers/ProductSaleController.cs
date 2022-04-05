using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectWebsite.Models;

namespace ProjectWebsite.Controllers
{
    public class ProductSaleController : Controller
    {
        private ProductManagementContext db = new ProductManagementContext();

        // GET: ProductSale
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View(db.PRODUCT_SALE.ToList());
            }
        }

        // GET: ProductSale/Create
        public ActionResult Create()
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

        // POST: ProductSale/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sale,SaleName")] PRODUCT_SALE pRODUCT_SALE)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (pRODUCT_SALE.SaleName != 0)
                {
                    db.PRODUCT_SALE.Add(pRODUCT_SALE);
                    db.SaveChanges();
                    TempData["Message"] = "Thêm Thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Thất Bại";
                    return View(pRODUCT_SALE);
                }
                
            }
        }

        // GET: ProductSale/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PRODUCT_SALE pRODUCT_SALE = db.PRODUCT_SALE.Find(id);
                if (pRODUCT_SALE == null)
                {
                    return HttpNotFound();
                }
                return View(pRODUCT_SALE);
            }
        }

        // POST: ProductSale/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sale,SaleName")] PRODUCT_SALE pRODUCT_SALE)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {

                if (pRODUCT_SALE.SaleName != 0)
                {
                    db.Entry(pRODUCT_SALE).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Cập nhật thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Thất Bại";
                    return View(pRODUCT_SALE);
                }
                
            }
        }

        // GET: ProductSale/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PRODUCT_SALE pRODUCT_SALE = db.PRODUCT_SALE.Find(id);
                if (pRODUCT_SALE == null)
                {
                    return HttpNotFound();
                }
                return View(pRODUCT_SALE);
            }
        }

        // POST: ProductSale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                PRODUCT_SALE pRODUCT_SALE = db.PRODUCT_SALE.Find(id);
                db.PRODUCT_SALE.Remove(pRODUCT_SALE);
                db.SaveChanges();
                TempData["Message"] = "Xóa thành công";
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
