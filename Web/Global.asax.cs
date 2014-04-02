using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EF;
using System.Data.Entity;
using System.Globalization;

namespace Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : Spring.Web.Mvc.SpringMvcApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<Context>(null);
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception unhandledException = Server.GetLastError();
        }
         
        //protected void Application_AcquireRequestState(object sender, EventArgs e)
        //{
        //    CultureInfo ci = null;
        //    String[] userLang = Request.UserLanguages;

        //    if (userLang.Length > 0)
        //    {
        //        ci = new CultureInfo(userLang[0]);
        //    }
        //    else
        //    {
        //        ci = new CultureInfo("zh");
        //    }
        //    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
        //    System.Threading.Thread.CurrentThread.CurrentCulture =
        //    CultureInfo.CreateSpecificCulture(ci.Name);
        //}
    }
}