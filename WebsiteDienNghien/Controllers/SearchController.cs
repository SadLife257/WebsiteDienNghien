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
    public class SearchController : Controller
    {
        QuanLyTiemDienEntities db = new QuanLyTiemDienEntities();

        // GET: Search
        public ActionResult Index(string srchOption, string srchTxt)
        {
            CustomMembershipUser user = (CustomMembershipUser)Membership.GetUser(User.Identity.Name, true);
            if (user != null)
            {
                ViewBag.user_id = user.UserId;
                ViewBag.isAuthenticated = User.Identity.IsAuthenticated;
                ViewBag.first_name = user.FirstName;
            }

            string searchMeta = srchOption;
            string searchText = srchTxt.ToLower();

            if (String.IsNullOrEmpty(searchMeta) || searchMeta.Equals("all"))
            {
                var c = from t in db.products
                        where t.name.ToLower().Contains(searchText)
                        select t;

                ViewBag.count = c.Count();

                return View(c.ToList());
            }

            ViewBag.meta = searchMeta;
            var v = from t in db.products
                    where t.category.meta == searchMeta && t.name.ToLower().Contains(searchText)
                    select t;

            ViewBag.count = v.Count();

            return View(v.ToList());
        }
    }
}