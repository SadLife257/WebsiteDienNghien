using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteDienNghien.Auth;
using WebsiteDienNghien.Models;
using WebsiteDienNghien.Utils;

namespace WebsiteDienNghien.Areas.admin.Controllers
{
    /*[CustomAuthorize(Roles = "User")]*/
    public class CategoriesController : Controller
    {
        private QuanLyTiemDienEntities db = new QuanLyTiemDienEntities();

        // GET: admin/Categories
        public ActionResult Index()
        {
            ViewBag.unread_feedback = (from t in db.feedbacks
                                       where t.read == false
                                       select t).Count();

            return View(db.categories.ToList());
        }

        // GET: admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: admin/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    category.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    category.meta = Functions.ConvertToUnSign(category.meta);
                    category.order = getMaxOrder();
                    db.categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return View(category);
        }

        // GET: admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,link,meta,hide,order,datebegin")] category category)
        {
            try
            {
                category temp = db.categories.Find(category.id);
                if (ModelState.IsValid)
                {
                    temp.name = category.name;
                    temp.meta = Functions.ConvertToUnSign(category.meta);
                    temp.hide = category.hide;
                    temp.order = category.order;
                    temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(category);
        }

        // GET: admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            category category = db.categories.Find(id);
            db.categories.Remove(category);
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

        public int getMaxOrder()
        {
            if (db.categories.Count() < 1)
                return 1;
            return db.categories.Count();
        }
    }
}
