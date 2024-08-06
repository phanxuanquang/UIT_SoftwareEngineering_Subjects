using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EBook.Models;

namespace EBook.Controllers
{
    public class LoaiSachController : Controller
    {
        private EBookDB db = new EBookDB();

        // GET: LoaiSach
        public ActionResult Index()
        {
            return View(db.LoaiSach.ToList());
        }

        // GET: LoaiSach/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSachModel loaiSachModel = db.LoaiSach.Find(id);
            if (loaiSachModel == null)
            {
                return HttpNotFound();
            }
            return View(loaiSachModel);
        }

        // GET: LoaiSach/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiSach/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoaiID,TenTheLoai")] LoaiSachModel loaiSachModel)
        {
            if (ModelState.IsValid)
            {
                db.LoaiSach.Add(loaiSachModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiSachModel);
        }

        // GET: LoaiSach/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSachModel loaiSachModel = db.LoaiSach.Find(id);
            if (loaiSachModel == null)
            {
                return HttpNotFound();
            }
            return View(loaiSachModel);
        }

        // POST: LoaiSach/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoaiID,TenTheLoai")] LoaiSachModel loaiSachModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiSachModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiSachModel);
        }

        // GET: LoaiSach/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiSachModel loaiSachModel = db.LoaiSach.Find(id);
            if (loaiSachModel == null)
            {
                return HttpNotFound();
            }
            return View(loaiSachModel);
        }

        // POST: LoaiSach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiSachModel loaiSachModel = db.LoaiSach.Find(id);
            db.LoaiSach.Remove(loaiSachModel);
            db.SaveChanges();
            return RedirectToAction("Index");
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
