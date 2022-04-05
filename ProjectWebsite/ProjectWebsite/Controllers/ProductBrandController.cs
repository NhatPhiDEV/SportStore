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
    public class ProductBrandController : Controller
    {
        private ProductManagementContext db = new ProductManagementContext();

        // GET: ProductBrand
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View(db.PRODUCT_BRAND.ToList());
            }
        }

        
        // GET: ProductBrand/Create
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

        // POST: ProductBrand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Brand,BrandName")] PRODUCT_BRAND pRODUCT_BRAND)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (pRODUCT_BRAND.BrandName != null)
                {
                    db.PRODUCT_BRAND.Add(pRODUCT_BRAND);
                    db.SaveChanges();
                    TempData["Message"] = "Thêm Thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Thất Bại";
                    return View(pRODUCT_BRAND);
                }
            }
        }

        // GET: ProductBrand/Edit/5
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
                PRODUCT_BRAND pRODUCT_BRAND = db.PRODUCT_BRAND.Find(id);
                if (pRODUCT_BRAND == null)
                {
                    return HttpNotFound();
                }
                return View(pRODUCT_BRAND);
            }
        }

        // POST: ProductBrand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Brand,BrandName")] PRODUCT_BRAND pRODUCT_BRAND)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (pRODUCT_BRAND.BrandName != null)
                {
                    db.Entry(pRODUCT_BRAND).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Cập nhật thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Thất Bại";
                    return View(pRODUCT_BRAND);
                }
                
            }
        }

        // GET: ProductBrand/Delete/5
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
                PRODUCT_BRAND pRODUCT_BRAND = db.PRODUCT_BRAND.Find(id);
                if (pRODUCT_BRAND == null)
                {
                    return HttpNotFound();
                }
                return View(pRODUCT_BRAND);
            }
        }

        // POST: ProductBrand/Delete/5
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
                PRODUCT_BRAND pRODUCT_BRAND = db.PRODUCT_BRAND.Find(id);
                db.PRODUCT_BRAND.Remove(pRODUCT_BRAND);
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
