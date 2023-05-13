using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebsiteDienNghien.Auth;
using WebsiteDienNghien.Models;

namespace WebsiteDienNghien.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class CartController : Controller
    {
        QuanLyTiemDienEntities db = new QuanLyTiemDienEntities();

        // GET: Cart
        public ActionResult Index(long id)
        {
            CustomMembershipUser user = (CustomMembershipUser)Membership.GetUser(User.Identity.Name, true);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.isAuthenticated = User.Identity.IsAuthenticated;
                ViewBag.first_name = user.FirstName;
            }

            bool checkForCart = (from t in db.carts
                                 where t.accountid == id && t.isOrder == false
                                 select t).Any();

            if (!checkForCart)
            {
                var cart = new cart
                {
                    accountid = (int)id,
                    total = 0,
                    datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                    isOrder = false
                };
                db.carts.Add(cart);
                db.SaveChanges();
            }

            ViewBag.count = (from t in db.cart_product
                             where t.cart.accountid == id && t.cart.isOrder == false
                             select t.quantity).Count();

            ViewBag.id = id;
            ViewBag.cartId = (from t in db.carts
                             where t.accountid == id && t.isOrder == false
                             select t.id).FirstOrDefault();

            if (ViewBag.count > 0)
            {
                ViewBag.price_total = (from t in db.cart_product
                                       where t.cart.accountid == id && t.cart.isOrder == false
                                       select t).Sum(p => p.product.price * p.quantity);
            }
            else
            {
                ViewBag.price_total = 0;
            }
            
            return View();
        }

        public ActionResult getAccountProduct(long id)
        {
            bool checkForCart = (from t in db.carts
                                 where t.accountid == id && t.isOrder == false
                                 select t).Any();

            if(!checkForCart)
            {
                var cart = new cart
                {
                    accountid = (int)id,
                    total = 0,
                    datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                    isOrder = false
                };
                db.carts.Add(cart);
                db.SaveChanges();
            }

            var v = from t in db.cart_product
                    where t.cart.accountid == id && t.cart.isOrder == false
                    select t;

            return PartialView(v.ToList());
        }

        [HttpPost]
        public ActionResult addToCart(int productid, string ReturnUrl = "")
        {
            int userid = Int32.Parse(Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString());

            bool checkForCart = (from t in db.carts
                                 where t.accountid == userid && t.isOrder == false
                                 select t).Any();

            if (!checkForCart)
            {
                var cart = new cart
                {
                    accountid = userid,
                    total = 0,
                    datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                    isOrder = false
                };
                db.carts.Add(cart);
                db.SaveChanges();
            }

            int cartid = (from t in db.carts
                          where t.accountid == userid && t.isOrder == false
                          select t.id).FirstOrDefault();

            cart_product cart_product = new cart_product
            {
                cartid = cartid,
                productid = productid,
                quantity = int.TryParse(Request.Form["quantity"], out int quantity) ? quantity : 1
                
            };

            db.cart_product.Add(cart_product);
            db.SaveChanges();

            cart temp = (from t in db.carts
                         where t.accountid == userid && t.isOrder == false
                         select t).FirstOrDefault();

            temp.total = (from t in db.cart_product
                          where t.cart.accountid == temp.accountid && t.cart.isOrder == false
                          select t).Sum(p => p.product.price * p.quantity);
            temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            db.Entry(temp).State = EntityState.Modified;
            db.SaveChanges();

            if (Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Index", "Cart", new { id = userid });
        }

        public ActionResult addToOrder(long cartId)
        {
            cart temp = db.carts.Find(cartId);

            temp.isOrder = true;

            db.Entry(temp).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Cart", new { id = temp.accountid });
        }

        public ActionResult Order(long id)
        {
            CustomMembershipUser user = (CustomMembershipUser)Membership.GetUser(User.Identity.Name, true);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.isAuthenticated = User.Identity.IsAuthenticated;
                ViewBag.first_name = user.FirstName;
            }

            ViewBag.id = id;
            ViewBag.count = (from t in db.carts
                            where t.accountid == id && t.isOrder == true
                            select t).Count();

            return View();
        }

        public ActionResult getAccountOrder(long id)
        {
            var v = from t in db.carts
                    where t.accountid == id && t.isOrder == true
                    select t;

            return PartialView(v.ToList());
        }

        public ActionResult getOrderProduct(long id)
        {
            var v = from t in db.cart_product
                    where t.cart.id == id
                    select t;

            return PartialView(v.ToList());
        }
    }
}