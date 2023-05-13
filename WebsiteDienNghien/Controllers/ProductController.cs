using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebsiteDienNghien.Auth;
using WebsiteDienNghien.Models;

namespace WebsiteDienNghien.Controllers
{
    public class ProductController : Controller
    {
        QuanLyTiemDienEntities db = new QuanLyTiemDienEntities();
        // GET: Product
        //52000632 - Nguyễn Lê Gia Bảo
        public ActionResult Index(string meta)
        {
            CustomMembershipUser user = (CustomMembershipUser)Membership.GetUser(User.Identity.Name, true);
            if (user != null)
            {
                ViewBag.user_id = user.UserId;
                ViewBag.isAuthenticated = User.Identity.IsAuthenticated;
                ViewBag.first_name = user.FirstName;
            }

            ViewBag.meta = meta;
            var v = from t in db.categories
                    where t.meta == meta
                    select t;

            ViewBag.count = (from t in db.products
                            where t.categoryid == v.FirstOrDefault().id
                            select t).Count();

            return View(v.FirstOrDefault());
        }

        //52000632 - Nguyễn Lê Gia Bảo
        public ActionResult Detail(long id)
        {
            CustomMembershipUser user = (CustomMembershipUser)Membership.GetUser(User.Identity.Name, true);
            if (user != null)
            {
                ViewBag.user_id = user.UserId;
                ViewBag.isAuthenticated = User.Identity.IsAuthenticated;
                ViewBag.first_name = user.FirstName;
            }

            ViewBag.meta = "san-pham";
            var v = from t in db.products
                    where t.id == id && t.hide == false
                    orderby t.order ascending
                    select t;
            return PartialView(v.FirstOrDefault());
        }

        public ActionResult getProductList(long id)
        {
            ViewBag.meta = "san-pham";
            var v = from t in db.products
                    where t.categoryid == id && t.hide == false
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult getProductBlock(long id)
        {
            ViewBag.meta = "san-pham";
            var v = from t in db.products
                    where t.categoryid == id && t.hide == false
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
        }
    }
}