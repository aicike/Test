using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.App_Start;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.apk/{*pathInfo}");

            //routes.MapRoute(
            //    name: "User",
            //    url: "{HostName}.imtimely.com/{controller}/{action}/{id}",
            //    defaults: new { id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "User",
            //    url: "{HostName}.imtimely.com/{controller}/{action}/{id}",
            //    defaults: new { id = UrlParameter.Optional }
            //);


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );

            routes.Add("User", new DomainRoute(
               "{HostName}.imtimely.com",     // Domain with parameters 
               "{controller}/{action}/{id}",    // URL with parameters 
               new { id = UrlParameter.Optional }  // Parameter defaults
            ));

        }
    }
}