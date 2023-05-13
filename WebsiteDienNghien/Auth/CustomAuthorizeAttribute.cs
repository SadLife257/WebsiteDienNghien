using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteDienNghien.Auth
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return ((CurrentUser != null && !CurrentUser.IsInRole(Roles)) || CurrentUser == null) ? false : true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectToRouteResult routeData = null;
            string controller = new Areas.admin.Controllers.DefaultController().ToString();
            if (CurrentUser == null)
            {
                if (controller.Equals(filterContext.Controller.ToString()))
                {
                   routeData = new RedirectToRouteResult
                   ("admin_login", new RouteValueDictionary
                   (new
                   {
                       controller = "Authorize",
                       action = "Login",
                   }
                   ));

                    System.Diagnostics.Debug.WriteLine("Switch");
                }
                else
                {
                    routeData = new RedirectToRouteResult
                   ("Login", new RouteValueDictionary
                   (new
                   {
                       controller = "Account",
                       action = "Login",
                   }
                   ));

                    System.Diagnostics.Debug.WriteLine("No Switch");
                }
               
            }
            else
            {
                routeData = new RedirectToRouteResult
                (new RouteValueDictionary
                 (new
                 {
                     controller = "Account",
                     action = "AccessDenied"
                 }
                 ));
            }

            filterContext.Result = routeData;
        }
    }
}