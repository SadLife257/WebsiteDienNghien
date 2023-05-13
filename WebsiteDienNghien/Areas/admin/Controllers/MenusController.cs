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
    public class MenusController : Controller
    {
        private QuanLyTiemDienEntities db = new QuanLyTiemDienEntities();

        // GET: admin/Menus
        public ActionResult Index()
        {
            ViewBag.unread_feedback = (from t in db.feedbacks
                                       where t.read == false
                                       select t).Count();

            return View(db.menus.ToList());
        }

        // GET: admin/Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu menu = db.menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: admin/Menus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,link,meta,hide,order,datebegin")] menu menu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    menu.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    menu.meta = Functions.ConvertToUnSign(menu.meta);
                    menu.order = getMaxOrder();
                    db.menus.Add(menu);
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

            return View(menu);
        }

        // GET: admin/Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu menu = db.menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: admin/Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,link,meta,hide,order,datebegin")] menu menu)
        {
            try
            {
                menu temp = db.menus.Find(menu.id);
                if (ModelState.IsValid)
                {
                    temp.name = menu.name;
                    temp.meta = Functions.ConvertToUnSign(menu.meta);
                    temp.link = menu.link;
                    temp.hide = menu.hide;
                    temp.order = menu.order;
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

            return View(menu);
        }

        // GET: admin/Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu menu = db.menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            menu menu = db.menus.Find(id);
            db.menus.Remove(menu);
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
            return db.menus.Count() + 1;
        }
    }
}
