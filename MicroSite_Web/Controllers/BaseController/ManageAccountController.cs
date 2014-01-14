using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Poco;
using Injection;
using Interface;
using System.Web.UI;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

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
                        { "action", "Index" }
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
            if (isCheckPermissions && LoginAccount.IsSuperAdmin == false)
            {
                var menuModel = Factory.Get<IMenuModel>(SystemConst.IOC_Model.MenuModel);
                menuModel.CheckHasPermissions(LoginAccount.RoleIDs, action, controller, area).NotAuthorizedPage();
            }

            //上一次请求信息
            var request = filterContext.RequestContext.HttpContext.Request;
            if (request != null && request.UrlReferrer != null && request.UrlReferrer.AbsolutePath != null)
            {
                ViewBag.RawUrl = filterContext.RequestContext.HttpContext.Request.UrlReferrer.AbsoluteUri;
            }

            GetMenu3(controller, action);
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 获取3级菜单
        /// </summary>
        public virtual void GetMenu3(string controller, string action)
        {
            if (string.IsNullOrEmpty(controller) == false && string.IsNullOrEmpty(action) == false)
            {
                var menuModel = Injection.Factory.Get<Interface.IMenuModel>(Poco.SystemConst.IOC_Model.MenuModel);
                var menu = menuModel.List_Cache().Where(a => a.Controller != null && a.Controller.Equals(controller, StringComparison.CurrentCultureIgnoreCase)
                    && a.Action != null && a.Action.Equals(action, StringComparison.CurrentCultureIgnoreCase)
                    && a.Level == 3).FirstOrDefault();
                if (menu != null)
                {
                    if (LoginAccount.IsSuperAdmin == false)
                    {
                        var menuList = menuModel.GetMenuByRoleID(LoginAccount.RoleIDs, menu.ParentMenuID);
                        ViewBag.Menu3List = menuList;
                    }
                    else
                    {
                        var menuList = menuModel.List_Cache().Where(a => a.ParentMenuID == menu.ParentMenuID);
                        ViewBag.Menu3List = menuList;
                    }
                }
                var menuOptionModel = Injection.Factory.Get<Interface.IMenuOptionModel>(Poco.SystemConst.IOC_Model.MenuOptionModel);
                var option = menuOptionModel.List_Cache().Where(a => a.Menu.Controller != null && a.Menu.Controller.Equals(controller, StringComparison.CurrentCultureIgnoreCase)
                && a.Action != null && a.Action.Equals(action, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (option != null)
                {
                    ViewBag.Menu3ActionPath = string.Format("{0} > {1}", option.Menu.Name, option.Name); ;
                }
            }
        }
    }
}
