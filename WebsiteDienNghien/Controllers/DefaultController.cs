using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebsiteDienNghien.Auth;
using WebsiteDienNghien.Models;

namespace WebsiteDienNghien.Controllers
{
    public class DefaultController : Controller
    {
        QuanLyTiemDienEntities db = new QuanLyTiemDienEntities();

        // GET: Default
        public ActionResult Index()
        {
            CustomMembershipUser user = (CustomMembershipUser)Membership.GetUser(User.Identity.Name, true);
            if (user != null)
            {
                /*CustomPrincipal principal = new CustomPrincipal(Membership.GetUser().Email);*/
                ViewBag.user_id = user.UserId;
                ViewBag.isAuthenticated = User.Identity.IsAuthenticated;
                ViewBag.first_name = user.FirstName;
            }
            else
            {
                ViewBag.user_id = "";
            }
            return View();
        }

        public ActionResult getNew()
        {
            var v = from t in db.news
                    where t.hide == false
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getMenu()
        {
            var v = from t in db.menus
                    where t.hide == false
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getCategory()
        {
            ViewBag.meta = "san-pham";
            var v = from t in db.categories
                    where t.hide == false
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getCategorySideBar()
        {
            ViewBag.meta = "san-pham";
            var v = from t in db.categories
                    where t.hide == false
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getLastestProduct()
        {
            ViewBag.meta = "san-pham";
            var v = from t in db.products
                    where t.hide == false
                    orderby t.datebegin ascending
                    select t;

            return PartialView(v.Take(12).ToList());
        }

        public ActionResult getProductCategory()
        {
            ViewBag.meta = "san-pham";
            var v = from t in db.categories
                    where t.hide == false
                    orderby t.order ascending
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getDisplayProduct(long id)
        {
            ViewBag.meta = "san-pham";
            var v = from t in db.products
                    where t.categoryid == id && t.hide == false
                    orderby t.order ascending
                    select t;
            return PartialView(v.Take(4).ToList());
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult sendFeedBack()
        {
            try
            {
                var feedBack = new feedback
                {
                    subject = Request.Form["contact-subject"],
                    content = Request.Form["contact-body"],
                    email = Request.Form["contact-email"],
                    name = Request.Form["contact-name"],
                    datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                    read = false,
                };
                db.feedbacks.Add(feedBack);
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

            return RedirectToAction("Contact", "Default");
        }
    }
}