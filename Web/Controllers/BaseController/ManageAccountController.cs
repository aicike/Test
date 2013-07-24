using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Poco;
using Injection;
using Interface;

namespace Controllers
{
    public class ManageAccountController : BaseController
    {
        protected Account LoginAccount
        {
            get
            {
                var account = Session[SystemConst.Session.LoginAccount] as Account;
                return account;
            }
            set { Session[SystemConst.Session.LoginAccount] = value; }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.RequestContext.RouteData.Values["controller"] as string;
            var action = filterContext.RequestContext.RouteData.Values["action"] as string;
            var area = filterContext.RouteData.DataTokens["area"] as string;

            if ((LoginAccount == null)
                //&& ((controller != null && (controller.Equals("Account", StringComparison.OrdinalIgnoreCase) == false && controller.Equals("Home", StringComparison.OrdinalIgnoreCase) == false && controller.Equals("Area", StringComparison.OrdinalIgnoreCase) == false))
                //)
                )
            {
                filterContext.Result = new RedirectToRouteResult("Default",
                    new RouteValueDictionary{
                        { "controller", "Login" },
                        { "action", "UserLogin" }
                });
                return;
            }

            //权限
            bool isCheckPermissions = true;
            var attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowCheckPermissionsAttribute), false);
            if (attrs != null && attrs.Length > 0)
            {
                AllowCheckPermissionsAttribute acp = attrs[0] as AllowCheckPermissionsAttribute;
                if (acp != null)
                {
                    isCheckPermissions = acp.AllowCheckPermissions;
                }
            }
            if (isCheckPermissions)
            {
                var menuModel = Factory.Get<IMenuModel>(SystemConst.IOC_Model.MenuModel);
                menuModel.CheckHasPermissions(LoginAccount.RoleID, action, controller, area).NotAuthorizedPage();
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
