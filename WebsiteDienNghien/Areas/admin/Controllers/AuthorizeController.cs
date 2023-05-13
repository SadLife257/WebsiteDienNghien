using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebsiteDienNghien.Auth;
using Newtonsoft.Json;

namespace WebsiteDienNghien.Areas.admin.Controllers
{
    public class AuthorizeController : Controller
    {
        // GET: admin/Authorize
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            CustomMembershipUser user = (CustomMembershipUser)Membership.GetUser(User.Identity.Name, true);
            if (user != null)
            {
                ViewBag.user_id = user.UserId;
                ViewBag.isAuthenticated = User.Identity.IsAuthenticated;
                ViewBag.first_name = user.FirstName;
            }

            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginView.Username, loginView.Password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(loginView.Username, false);

                    if (user != null)
                    {
                        CustomSerializeModel userModel = new CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Roles = user.Roles.Select(r => r.name).ToList()
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                        (
                            1, loginView.Username, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                        );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Default");
                    }
                }
            }
            ModelState.AddModelError("Lỗi", "Tài khoản hoặc mật khẩu không chính xác");
            return View(loginView);
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}