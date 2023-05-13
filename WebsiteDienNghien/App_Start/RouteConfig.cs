using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteDienNghien
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Contact", "lien-lac",
            new { controller = "Default", action = "Contact"},
            namespaces: new[] { "WebsiteDienNghien.Controllers" });

            routes.MapRoute("Login", "dang-nhap",
            new { controller = "Account", action = "Login"},
            namespaces: new[] { "WebsiteDienNghien.Controllers" });

            routes.MapRoute("Register", "dang-ky",
            new { controller = "Account", action = "Registration"},
            namespaces: new[] { "WebsiteDienNghien.Controllers" });

            routes.MapRoute("Search", "{type}/{meta}",
            new { controller = "Search", action = "Index", meta = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "tim-kiem" }
            },
            namespaces: new[] { "WebsiteDienNghien.Controllers" });

            routes.MapRoute("Order", "giao-hang/{id}",
            new { controller = "Cart", action = "Order", id = UrlParameter.Optional },
            namespaces: new[] { "WebsiteDienNghien.Controllers" });

            routes.MapRoute("Cart", "gio-hang/{id}",
            new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "WebsiteDienNghien.Controllers" });

            routes.MapRoute("Product", "{type}/{meta}",
            new { controller = "Product", action = "Index", meta = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "san-pham" }
            },
            namespaces: new[] { "WebsiteDienNghien.Controllers" });

            routes.MapRoute("Detail", "{type}/{meta}/{id}",
            new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
            new RouteValueDictionary
            {
                { "type", "san-pham" }
            },
            namespaces: new[] { "WebsiteDienNghien.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebsiteDienNghien.Controllers" }
            );
        }
    }
}
