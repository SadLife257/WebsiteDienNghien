using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteDienNghien.Models;

namespace WebsiteDienNghien.Areas.admin.Controllers
{
    public class CartsController : Controller
    {
        private QuanLyTiemDienEntities db = new QuanLyTiemDienEntities();

        // GET: admin/Carts
        public ActionResult Index()
        {
            var carts = db.carts.Include(c => c.account);

            ViewBag.unread_feedback = (from t in db.feedbacks
                                       where t.read == false
                                       select t).Count();

            return View(carts.ToList());
        }

        // GET: admin/Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: admin/Carts/Create
        public ActionResult Create()
        {
            ViewBag.accountid = new SelectList(db.accounts, "id", "username");
            return View();
        }

        // POST: admin/Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,accountid,datebegin,total,isOrder")] cart cart)
        {
            if (ModelState.IsValid)
            {
                db.carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    cart.total = 0;
                    cart.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    db.carts.Add(cart);
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

            ViewBag.accountid = new SelectList(db.accounts, "id", "username", cart.accountid);
            return View(cart);
        }

        // GET: admin/Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            ViewBag.accountid = new SelectList(db.accounts, "id", "username", cart.accountid);
            return View(cart);
        }

        // POST: admin/Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,accountid,datebegin,total,isOrder")] cart cart)
        {
            try
            {
                cart temp = db.carts.Find(cart.id);
                if (ModelState.IsValid)
                {
                    temp.accountid = cart.accountid;
                    temp.total = cart.total;
                    temp.isOrder = cart.isOrder;
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

            ViewBag.accountid = new SelectList(db.accounts, "id", "username", cart.accountid);
            return View(cart);
        }

        // GET: admin/Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: admin/Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cart cart = db.carts.Find(id);
            cart.cart_product.Clear();
            db.carts.Remove(cart);
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
