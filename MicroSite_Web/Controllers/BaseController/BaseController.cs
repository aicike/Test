using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using Poco;

namespace System.Web.Mvc
{
    public class BaseController : Controller
    {
        public ContentResult Alert(Dialog dialog)
        {
            var js = Content(AlertJS(dialog));
            return js;
        }

        public string AlertJS(Dialog dialog)
        {
            var json = JsonConvert.SerializeObject(dialog);
            var js= string.Format("<script>JAlert({0})</script>", json);
            return js;
        }

        public string AlertJS_NoTag(Dialog dialog)
        {
            var json = JsonConvert.SerializeObject(dialog);
            var js = string.Format("JAlert({0})", json);
            return js;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            if (request != null && request.UrlReferrer != null && request.UrlReferrer.AbsolutePath != null)
            {
                ViewBag.RawUrl = filterContext.RequestContext.HttpContext.Request.UrlReferrer.AbsoluteUri;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
