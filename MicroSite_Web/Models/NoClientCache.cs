using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace System.Web.Mvc
{
    public class NoClientCache : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();
            base.OnResultExecuting(filterContext);
        }
    } 
}
