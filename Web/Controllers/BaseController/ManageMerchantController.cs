using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Injection;
using System.Web.Routing;

namespace Controllers
{
    public class ManageMerchantController : BaseController
    {
        //
        // GET: /ManageMerchant/
        protected Merchant LoginMerchant
        {
            get
            {
                var loginMerchant = Session[SystemConst.Session.LoginMerchant] as Merchant;
                return loginMerchant;
            }
            set { Session[SystemConst.Session.LoginMerchant] = value; }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.RequestContext.RouteData.Values["controller"] as string;
            var action = filterContext.RequestContext.RouteData.Values["action"] as string;
            var area = filterContext.RouteData.DataTokens["area"] as string;

            if ((LoginMerchant == null)
                //&& ((controller != null && (controller.Equals("Account", StringComparison.OrdinalIgnoreCase) == false && controller.Equals("Home", StringComparison.OrdinalIgnoreCase) == false && controller.Equals("Area", StringComparison.OrdinalIgnoreCase) == false))
                //)
                )
            {
                filterContext.Result = new RedirectToRouteResult("Default",
                    new RouteValueDictionary{
                        { "controller", "MerchantLand" },
                        { "action", "Index" }
                });
                return;
            }
            //上一次请求信息
            var request = filterContext.RequestContext.HttpContext.Request;
            if (request != null && request.UrlReferrer != null && request.UrlReferrer.AbsolutePath != null)
            {
                ViewBag.RawUrl = filterContext.RequestContext.HttpContext.Request.UrlReferrer.AbsoluteUri;
            }


            base.OnActionExecuting(filterContext);
        }

    }
}
