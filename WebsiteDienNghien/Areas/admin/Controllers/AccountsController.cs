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
    public class AccountsController : Controller
    {
        private QuanLyTiemDienEntities db = new QuanLyTiemDienEntities();

        // GET: admin/Accounts
        public ActionResult Index()
        {
            ViewBag.unread_feedback = (from t in db.feedbacks
                                       where t.read == false
                                       select t).Count();

            return View(db.accounts.ToList());
        }

        public void getRole(long? id = null)
        {
            if (id != null)
            {
                account account = db.accounts.Find(id);
                ViewBag.roleid = new MultiSelectList(db.roles, "id", "name");
            }
            ViewBag.roleid = new MultiSelectList(db.roles, "id", "name");
        }

        // GET: admin/Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: admin/Accounts/Create
        public ActionResult Create()
        {
            getRole();
            return View();
        }

        // POST: admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,email,password,address,phonenumber,firstname,lassname")] account account, int[] roles)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    foreach(int id in roles)
                    {
                        account.roles.Add(db.roles.Find(id));
                    }
                    db.accounts.Add(account);
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

            return View(account);
        }

        // GET: admin/Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            getRole();
            return View(account);
        }

        // POST: admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,email,password,address,phonenumber,firstname,lassname")] account account, int[] roles)
        {
            try
            {
                account temp = db.accounts.Find(account.id);
                if (ModelState.IsValid)
                {
                    temp.username = account.username;
                    temp.firstname = account.firstname;
                    temp.lassname = account.lassname;
                    temp.email = account.email;
                    temp.password = account.password;
                    temp.phonenumber = account.phonenumber;
                    temp.address = account.address;
                    temp.roles.Clear();
                    foreach (int id in roles)
                    {
                        temp.roles.Add(db.roles.Find(id));
                    }
                    /*temp.roles = new SelectList(db.roles, "id", "name", selectedId)*/;
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
            return View(account);
        }

        // GET: admin/Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: admin/Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            account account = db.accounts.Find(id);
            account.roles.Clear();
            db.accounts.Remove(account);
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
