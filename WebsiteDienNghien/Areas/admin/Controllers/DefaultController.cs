using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteDienNghien.Auth;
using WebsiteDienNghien.Models;

namespace WebsiteDienNghien.Areas.admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class DefaultController : Controller
    {
        private QuanLyTiemDienEntities db = new QuanLyTiemDienEntities();

        // GET: admin/Default
        public ActionResult Index()
        {
            ViewBag.unread_feedback = (from t in db.feedbacks
                                       where t.read == false
                                       select t).Count();

            ViewBag.account_num = (from t in db.accounts
                                   select t).Count();

            ViewBag.product_num = (from t in db.products
                                   select t).Count();

            ViewBag.income_num = 0;
            bool checkTotal = (from t in db.carts
                               select t).Any();
            if(checkTotal)
            {
                ViewBag.income_num = (from t in db.carts
                                      select t.total).Sum();
            }
            

            ViewBag.order_num = (from t in db.carts
                                 where t.isOrder == true
                                 select t).Count();

            return View();
        }

        public ActionResult getNotifyFeedBack()
        {
            var v = from t in db.feedbacks
                    where t.read == false
                    orderby t.datebegin ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult FeedbackList()
        {
            return View();
        }

        public ActionResult getFeedBackList()
        {
            var v = from t in db.feedbacks
                    orderby t.datebegin ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult FeedbackDetail(long id)
        {
            var v = (from t in db.feedbacks
                    where t.id == id
                    select t).FirstOrDefault();

            try
            {
                feedback temp = db.feedbacks.Find(id);

                temp.read = true;

                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(v);
        }

        public ActionResult deleteFeedback(long id)
        {
            feedback feedback = db.feedbacks.Find(id);
            db.feedbacks.Remove(feedback);
            db.SaveChanges();

            return RedirectToAction("FeedbackList");
        }
    }
}