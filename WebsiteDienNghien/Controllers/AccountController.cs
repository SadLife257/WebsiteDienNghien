using WebsiteDienNghien.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebsiteDienNghien.Models;

namespace WebsiteDienNghien.Controllers
{
    public class AccountController : Controller
    {
        QuanLyTiemDienEntities db = new QuanLyTiemDienEntities();

        // GET: Account
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
                            Roles = user.Roles.Select(r =>r.name).ToList()
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

        [HttpGet]
        public ActionResult Registration()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel registrationView)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;

            if (ModelState.IsValid)
            {
                // Email Verification
                string userName = Membership.GetUserNameByEmail(registrationView.Email);
                if (!string.IsNullOrEmpty(userName))
                {
                    //False When Submit
                    ModelState.AddModelError("Lỗi", "Email đã được dùng để đăng ký tài khoản");
                    return View(registrationView);
                }

                //Save User Data
                
                var user = new account()
                {
                    username = registrationView.Username,
                    firstname = registrationView.Firstname,
                    lassname = registrationView.Lastname,
                    email = registrationView.Email,
                    address = registrationView.Address,
                    phonenumber = registrationView.Phone,
                    password = registrationView.Password,
                    roles = (from t in db.roles
                            where t.name.Contains("User")
                            select t).ToList()
                };
                db.accounts.Add(user);

                var cart = new cart
                {
                    accountid = user.id,
                    total = 0,
                    datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                    isOrder = false
                };
                db.carts.Add(cart);
                
                db.SaveChanges();

                messageRegistration = "Tài khoản được tạo thành công";
                statusRegistration = true;
            }
            else
            {
                messageRegistration = "Lỗi";
            }
            /*DISPLAY THIS SOMEWHERE*/
            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;

            return View(registrationView);
        }

        public ActionResult ForgetPass()
        {
            return View();
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