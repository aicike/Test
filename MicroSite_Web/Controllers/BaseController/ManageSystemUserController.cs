using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using System.Web.Routing;
using Injection;
using Interface;

namespace Web.Controllers
{
    public class ManageSystemUserController : BaseController
    {
        protected SystemUser LoginSystemUser
        {
            get
            {
                var loginSystemUser = Session[SystemConst.Session.LoginSystemUser] as SystemUser;
                return loginSystemUser;
            }
            set { Session[SystemConst.Session.LoginSystemUser] = value; }
        }
        
       
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.RequestContext.RouteData.Values["controller"] as string;
            var action = filterContext.RequestContext.RouteData.Values["action"] as string;
            var area = filterContext.RouteData.DataTokens["area"] as string;

            if ((LoginSystemUser == null)
                //&& ((controller != null && (controller.Equals("Account", StringComparison.OrdinalIgnoreCase) == false && controller.Equals("Home", StringComparison.OrdinalIgnoreCase) == false && controller.Equals("Area", StringComparison.OrdinalIgnoreCase) == false))
                //)
                )
            {
                filterContext.Result = new RedirectToRouteResult("Default",
                    new RouteValueDictionary{
                        { "controller", "Login" },
                        { "action", "SystemLogin" }
                });
                return;
            }
            //权限
            var systemUserMenuModel = Factory.Get<ISystemUserMenuModel>(SystemConst.IOC_Model.SystemUserMenuModel);
            systemUserMenuModel.CheckHasPermissions(LoginSystemUser.SystemUserRoleID, action, controller, area).NotAuthorizedPage();

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
