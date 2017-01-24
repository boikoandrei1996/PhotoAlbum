using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplicationPL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Photo",
                url: "photo/{id}",
                defaults: new { controller = "Photo", action = "ShowPhoto" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "ProfileDelete",
                url: "profile/delete",
                defaults: new { controller = "Profile", action = "Delete" }
            );

            routes.MapRoute(
                name: "ProfileId",
                url: "profile/{id}",
                defaults: new { controller = "Profile", action = "Index" },
                constraints: new { id = @"\d+" }
            );
            
            routes.MapRoute(
                name: "ProfileLogin",
                url: "profile/{login}",
                defaults: new { controller = "Profile", action = "IndexByLogin" },
                constraints: new { login = @"\w+" }
            );

            routes.MapRoute(
                name: "ProfileDefault",
                url: "profile/{action}/{id}",
                defaults : new { controller = "Profile" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
