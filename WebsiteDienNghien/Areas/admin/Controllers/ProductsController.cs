using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteDienNghien.Models;
using WebsiteDienNghien.Utils;

namespace WebsiteDienNghien.Areas.admin.Controllers
{
    /*[CustomAuthorize(Roles = "User")]*/
    public class ProductsController : Controller
    {
        private QuanLyTiemDienEntities db = new QuanLyTiemDienEntities();

        // GET: admin/Products
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.category);

            ViewBag.unread_feedback = (from t in db.feedbacks
                                       where t.read == false
                                       select t).Count();

            return View(products.ToList());
        }

        public void getCategory(long? selectedId = null)
        {
            ViewBag.categoryid = new SelectList(db.categories.Where(x => x.hide == false)
                .OrderBy(x => x.order), "id", "name", selectedId);
        }

        // GET: admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: admin/Products/Create
        public ActionResult Create()
        {
            getCategory();
            return View();
        }

        // POST: admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,price,img,detail,size,color,meta,hide,order,datebegin,categoryid")] product product, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload/images/products"), filename);
                        img.SaveAs(path);
                        product.img = filename; 
                    }
                    else
                    {
                        product.img = "logo_2.png";
                    }
                    product.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    product.meta = Functions.ConvertToUnSign(product.meta);
                    product.order = getMaxOrder(product.categoryid);
                    db.products.Add(product);
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

            ViewBag.categoryid = new SelectList(db.categories, "id", "name", product.categoryid);
            return View(product);
        }

        // GET: admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            getCategory(product.categoryid);
            return View(product);
        }

        // POST: admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,price,img,detail,size,color,meta,hide,order,datebegin,categoryid")] product product, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                product temp = db.products.Find(product.id);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Content/upload/images/products"), filename);
                        img.SaveAs(path);
                        temp.img = filename;
                    }

                    temp.name = product.name;
                    temp.price = product.price;
                    temp.detail = product.detail;
                    temp.meta = Functions.ConvertToUnSign(product.meta);
                    temp.color = product.color;
                    temp.size = product.size;
                    temp.hide = product.hide;
                    temp.order = product.order;
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
            ViewBag.categoryid = new SelectList(db.categories, "id", "name", product.categoryid);
            return View(product);
        }

        // GET: admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
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

        public int getMaxOrder(long? CategoryId)
        {
            if (CategoryId == null)
                return 1;
            return (db.products.Where(x => x.categoryid == CategoryId).Count() + 1);
        }
    }
}
