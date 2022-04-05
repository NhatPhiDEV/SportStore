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
    public class ProductSizeController : Controller
    {
        private ProductManagementContext db = new ProductManagementContext();

        /// <summary>
        /// Xem, thêm, xóa, sửa size quần áo...
        /// Author: Phan thanh tú (18/07/2021)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View(db.PRODUCT_SIZE.ToList());
            }
        }

        // GET: ProductSize/Create
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

        // POST: ProductSize/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Size,SizeName")] PRODUCT_SIZE pRODUCT_SIZE)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (pRODUCT_SIZE.SizeName != null)
                {
                    db.PRODUCT_SIZE.Add(pRODUCT_SIZE);
                    db.SaveChanges();
                    TempData["Message"] = "Thêm Thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Thất Bại";
                    return View(pRODUCT_SIZE);
                }
                
            }
        }

        // GET: ProductSize/Edit/5
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
                PRODUCT_SIZE pRODUCT_SIZE = db.PRODUCT_SIZE.Find(id);
                if (pRODUCT_SIZE == null)
                {
                    return HttpNotFound();
                }
                return View(pRODUCT_SIZE);
            }
        }

        // POST: ProductSize/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Size,SizeName")] PRODUCT_SIZE pRODUCT_SIZE)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (pRODUCT_SIZE.SizeName != null)
                {
                    db.Entry(pRODUCT_SIZE).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Cập nhật thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Thất Bại";
                    return View(pRODUCT_SIZE);
                }
               
            }
        }

        // GET: ProductSize/Delete/5
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
                PRODUCT_SIZE pRODUCT_SIZE = db.PRODUCT_SIZE.Find(id);
                if (pRODUCT_SIZE == null)
                {
                    return HttpNotFound();
                }
                return View(pRODUCT_SIZE);
            }
        }

        // POST: ProductSize/Delete/5
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
                PRODUCT_SIZE pRODUCT_SIZE = db.PRODUCT_SIZE.Find(id);
                db.PRODUCT_SIZE.Remove(pRODUCT_SIZE);
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
