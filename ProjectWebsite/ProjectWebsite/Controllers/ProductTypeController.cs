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
    public class ProductTypeController : Controller
    {
        private ProductManagementContext db = new ProductManagementContext();

        /// <summary>
        /// Xem, thêm, xóa, sửa loại sản phẩm...
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
                return View(db.PRODUCT_TYPE.ToList());
            }
        }

        // GET: ProductType/Create
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

        // POST: ProductType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Type,TypeName")] PRODUCT_TYPE pRODUCT_TYPE)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (pRODUCT_TYPE.TypeName != null)
                {
                    db.PRODUCT_TYPE.Add(pRODUCT_TYPE);
                    db.SaveChanges();
                    TempData["Message"] = "Thêm Thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Thất Bại";
                    return View(pRODUCT_TYPE);
                }

            }
        }

        // GET: ProductType/Edit/5
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
                PRODUCT_TYPE pRODUCT_TYPE = db.PRODUCT_TYPE.Find(id);
                if (pRODUCT_TYPE == null)
                {
                    return HttpNotFound();
                }
                return View(pRODUCT_TYPE);
            }
        }

        // POST: ProductType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Type,TypeName")] PRODUCT_TYPE pRODUCT_TYPE)
        {
            if (Session["TaiKhoanAdmin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (pRODUCT_TYPE.TypeName != null)
                {
                    db.Entry(pRODUCT_TYPE).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Message"] = "Cập nhật thành công";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = "Thất Bại";
                    return View(pRODUCT_TYPE);
                }

            }
        }

        // GET: ProductType/Delete/5
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
                PRODUCT_TYPE pRODUCT_TYPE = db.PRODUCT_TYPE.Find(id);
                if (pRODUCT_TYPE == null)
                {
                    return HttpNotFound();
                }
                return View(pRODUCT_TYPE);
            }
        }

        // POST: ProductType/Delete/5
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
                PRODUCT_TYPE pRODUCT_TYPE = db.PRODUCT_TYPE.Find(id);
                db.PRODUCT_TYPE.Remove(pRODUCT_TYPE);
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
