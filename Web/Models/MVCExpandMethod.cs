using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Interface;
using Injection;
using Poco;
using System.Web.Mvc.Ajax;
using Web.Models;

namespace System.Web.Mvc.Html
{
    public static class MVCExpandMethod
    {

        public static bool CheckHasPermissions(ViewContext viewContext, string action, string controller = null, object routeValues = null)
        {
            //var session = HttpContext.Current.Session;

            var loginSystemUser = HttpContext.Current.Session[Poco.SystemConst.Session.LoginSystemUser] as Poco.SystemUser;
            var loginAccount = HttpContext.Current.Session[Poco.SystemConst.Session.LoginAccount] as Poco.Account;
            if (loginSystemUser == null && loginAccount == null)
            {
                return false;
            }
            if (loginSystemUser != null && loginSystemUser.SystemUserRoleID == 1)
            {
                return true;
            }
            controller = controller ?? viewContext.RequestContext.RouteData.Values["controller"] as string;
            action = action ?? viewContext.RequestContext.RouteData.Values["action"] as string;
            string area = null;
            if (routeValues != null)
            {
                var p = routeValues.GetType().GetProperty("Area");
                if (p != null)
                {
                    area = p.GetValue(routeValues, null) as string;
                }
            }
            if (loginSystemUser != null)
            {
                var systemUserMenuModel = Factory.Get<ISystemUserMenuModel>(SystemConst.IOC_Model.SystemUserMenuModel);
                return systemUserMenuModel.CheckHasPermissions(loginSystemUser.SystemUserRoleID, action, controller, area);
            }
            if (loginAccount != null)
            {
                var menuModel = Factory.Get<IMenuModel>(SystemConst.IOC_Model.MenuModel);
                return menuModel.CheckHasPermissions(loginAccount.RoleIDs, action, controller, area);
            }
            return false;
        }

        public static MvcHtmlString ActionLink<TModel>(this HtmlHelper<TModel> htmlHelper, string linkText, string actionName, bool isCheckPermissions)
        {
            if (isCheckPermissions)
            {
                if (!CheckHasPermissions(htmlHelper.ViewContext, actionName))
                {
                    return MvcHtmlString.Empty;
                }
            }
            return htmlHelper.ActionLink(linkText, actionName);
        }

        public static MvcHtmlString ActionLink<TModel>(this HtmlHelper<TModel> htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, bool isCheckPermissions, bool isShowText = false)
        {
            if (isCheckPermissions)
            {
                if (!CheckHasPermissions(htmlHelper.ViewContext, actionName, controllerName, routeValues))
                {
                    if (isShowText)
                    {
                        return MvcHtmlString.Create(linkText);
                    }
                    else
                    {
                        return MvcHtmlString.Empty;
                    }
                }
            }
            return htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString ActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, bool isCheckPermissions, bool isShowText = false)
        {
            if (isCheckPermissions)
            {
                if (!CheckHasPermissions(ajaxHelper.ViewContext, actionName, controllerName, routeValues))
                {
                    if (isShowText)
                    {
                        return MvcHtmlString.Create(linkText);
                    }
                    else
                    {
                        return MvcHtmlString.Empty;
                    }
                }
            }
            return ajaxHelper.ActionLink(linkText, actionName, controllerName, routeValues, ajaxOptions);
        }


    }
}