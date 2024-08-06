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
    public class SachController : Controller
    {
        private EBookDB db = new EBookDB();

        // GET: Sach
        public ActionResult Index()
        {
            return View(db.Database.SqlQuery<SachViewModel>("LietKeSach").ToList());
        }

        // GET: Sach/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SachModel sachModel = db.Sach.Find(id);
            if (sachModel == null)
            {
                return HttpNotFound();
            }
            return View(sachModel);
        }

        // GET: Sach/Create
        public ActionResult Create()
        {
            List<LoaiSachModel> loaisach = db.LoaiSach.ToList<LoaiSachModel>();
            SachViewModel model = new SachViewModel();
            model.DanhMuc = loaisach;
            return View(model);
        }

        // POST: Sach/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TuaSach,ImgFile,Noidung,LoaiID")] SachViewModel sachModel)
        {
            SachModel sach = new Models.SachModel();
            sach.LoaiID = sachModel.LoaiID;
            sach.TuaSach = sachModel.TuaSach;
            sach.Noidung = sachModel.Noidung;
            if (ModelState.IsValid)
            {
                db.Sach.Add(sach);
                db.SaveChanges();
                if (sachModel.ImgFile != null)
                    sachModel.ImgFile.SaveAs(Server.MapPath("/images") + "/" + sach.Id + ".jpg");
                return RedirectToAction("Index");
            }
            sachModel.DanhMuc = db.LoaiSach.ToList<LoaiSachModel>();
            return View(sachModel);
        }

        // GET: Sach/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SachModel sach = db.Sach.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            SachViewModel sachModel = new SachViewModel();
            sachModel.Id = sach.Id;
            sachModel.TuaSach = sach.TuaSach;
            sachModel.HinhMinhHoa = sach.HinhMinhHoa;
            sachModel.Noidung = sach.Noidung;
            sachModel.LoaiID = sach.LoaiID;
            sachModel.DanhMuc = db.LoaiSach.ToList<LoaiSachModel>();
            return View(sachModel);
        }

        // POST: Sach/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TuaSach,ImgFile,HinhMinhHoa,Noidung,LoaiID")] SachViewModel sachModel)
        {
            SachModel sach = new Models.SachModel();
            sach.Id = sachModel.Id;
            sach.TuaSach = sachModel.TuaSach;
            sach.HinhMinhHoa = sachModel.HinhMinhHoa;
            sach.Noidung = sachModel.Noidung;
            sach.LoaiID = sachModel.LoaiID;
            if (ModelState.IsValid)
            {
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                if (sachModel.ImgFile != null)
                    sachModel.ImgFile.SaveAs(Server.MapPath("/images") + "/" + sach.Id + ".jpg");
                return RedirectToAction("Index");
            }
            sachModel.DanhMuc = db.LoaiSach.ToList<LoaiSachModel>();
            return View(sachModel);
        }

        // GET: Sach/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SachModel sachModel = db.Sach.Find(id);
            if (sachModel == null)
            {
                return HttpNotFound();
            }
            return View(sachModel);
        }

        // POST: Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SachModel sachModel = db.Sach.Find(id);
            db.Sach.Remove(sachModel);
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
