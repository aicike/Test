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
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.Xml.Dom;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Web.SSO;

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
                #region 单点登录分站验证

                //令牌验证结果
                if (Request.QueryString["Token"] != null)
                {
                    if (Request.QueryString["Token"] != "$Token$")
                    {
                        //持有令牌
                        string tokenValue = Request.QueryString["Token"];
                        //调用WebService获取主站凭证
                        TokenService tokenService = new TokenService();
                        object o = tokenService.TokenGetCredence(tokenValue);
                        if (o != null)
                        {
                            //令牌正确
                            Session["Token"] = o;
                            Response.Write("恭喜，令牌存在，您被授权访问该页面！");
                        }
                        else
                        {
                            //令牌错误
                            Response.Redirect(this.replaceToken());
                            //string url = Request.Url.AbsoluteUri;
                            //filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" }, { "BackURL", url } });
                        }
                    }
                    else
                    {
                        //未持有令牌
                        Response.Redirect(this.replaceToken());
                        //string url = Request.Url.AbsoluteUri;
                        //filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" }, { "BackURL", url } });
                    }
                }
                //未进行令牌验证，去主站验证
                else
                {
                    Response.Redirect(this.getTokenURL());
                    //filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }

                #endregion
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
        /// 获取带令牌请求的URL
        /// 在当前URL中附加上令牌请求参数
        /// </summary>
        /// <returns></returns>
        private string getTokenURL()
        {
            string url = Request.Url.AbsoluteUri;
            Regex reg = new Regex(@"^.*\?.+=.+$");
            if (reg.IsMatch(url))
                url += "&Token=$Token$";
            else
                url += "?Token=$Token$";

            return string.Format("http://{0}/SSOService?BackURL={1}", SystemConst.WebUrl,Server.UrlEncode(url));
        }

        /// <summary>
        /// 去掉URL中的令牌
        /// 在当前URL中去掉令牌参数
        /// </summary>
        /// <returns></returns>
        private string replaceToken()
        {
            string url = Request.Url.AbsoluteUri;
            url = Regex.Replace(url, @"(\?|&)Token=.*", "", RegexOptions.IgnoreCase);
            return string.Format("http://{0}/Login?BackURL={1}" ,SystemConst.WebUrl, Server.UrlEncode(url));
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
                    var menuList = menuModel.GetMenuByRoleID(LoginAccount.RoleIDs, menu.ParentMenuID);
                    ViewBag.Menu3List = menuList;
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
