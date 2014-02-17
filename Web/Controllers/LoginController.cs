using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Interface;
using Injection;
using agsXMPP;
using System.Threading;
using agsXMPP.protocol.client;
using System.Threading.Tasks;
using agsXMPP.Xml.Dom;
using System.IO;
using System.Data;
using Web.SSO;
using Common;
using System.Web.Routing;

namespace Web.Controllers
{
    public class LoginController : BaseController, System.Web.SessionState.IRequiresSessionState
    {
        #region 平台

        public ActionResult Index(string BackUrl)
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "首页", WebTitleRemark, "");
            ViewBag.Title = webTitle;
            //读取展示图片
            DataTable dt = new DataTable();
            dt.Columns.Add("path");
            string loginPageShow = HttpContext.Server.MapPath("~/File/LoginPageShow/Client");
            DirectoryInfo TheFolder = new DirectoryInfo(loginPageShow);
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                DataRow row = dt.NewRow();
                row["path"] = "~/File/LoginPageShow/Client/" + NextFile.Name;
                dt.Rows.Add(row);
            }
            if (dt.Rows.Count <= 0)
            {
                DataRow row = dt.NewRow();
                row["path"] = "~/File/LoginPageShow/Default/Default.jpg";
                dt.Rows.Add(row);
            }
            ViewBag.ShowPage = dt;
            ViewBag.BackUrl = BackUrl;
            return View();
            //return RedirectToAction("SystemLogin");
        }

        /// <summary>
        /// 平台登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SystemLogin()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "登录", WebTitleRemark, "");
            ViewBag.Title = webTitle;
            //读取展示图片
            DataTable dt = new DataTable();
            dt.Columns.Add("path");
            string loginPageShow = HttpContext.Server.MapPath("~/File/LoginPageShow/System");
            DirectoryInfo TheFolder = new DirectoryInfo(loginPageShow);
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                DataRow row = dt.NewRow();
                row["path"] = "~/File/LoginPageShow/System/" + NextFile.Name;
                dt.Rows.Add(row);
            }
            if (dt.Rows.Count <= 0)
            {
                DataRow row = dt.NewRow();
                row["path"] = "~/File/LoginPageShow/Default/Default.jpg";
                dt.Rows.Add(row);
            }
            ViewBag.ShowPage = dt;
            return View();
        }

        [HttpPost]
        public ActionResult SystemLogin(SystemUser user)
        {
            ISystemUserModel systemUserModel = Factory.Get<ISystemUserModel>(SystemConst.IOC_Model.SystemUserModel);
            var result = systemUserModel.Login(user.Email, user.LoginPwdPage);
            if (result.HasError)
            {
                return JavaScript("LandWaitFor('login','WaitImg',2);" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            var url = Url.RouteUrl("System_default", new { action = "Index", controller = "SystemUserHome" });
            return JavaScript("window.location.href='" + url + "'");
        }

        #endregion

        #region 售楼部


        /// <summary>
        /// 售楼部登录
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogin(String hostName)
        {
            Session["IsHaveMessage"] = "0";
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "登录", WebTitleRemark, "");
            ViewBag.Title = webTitle;

            ViewBag.BackUrl = Request.QueryString["BackURL"];
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(string phone_email, string password)
        {
            IAccountModel accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var result = accountModel.Login(phone_email, password);
            if (result.HasError)
            {
                return JavaScript("LandWaitFor('login','WaitImg',2);" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            var account = Session[SystemConst.Session.LoginAccount] as Account;

            #region 单点登录

            //产生令牌
            string tokenValue = Guid.NewGuid().ToString().ToUpper();
            HttpCookie tokenCookie = new HttpCookie("Token");
            tokenCookie.Values.Add("Value", tokenValue);
            tokenCookie.Domain = SystemConst.WebUrl;
            Response.AppendCookie(tokenCookie);

            //产生主站凭证
            object info =Newtonsoft.Json.JsonConvert.SerializeObject(account);
            CacheManager.TokenInsert(tokenValue, info, DateTime.Now.AddMinutes(double.Parse(System.Configuration.ConfigurationManager.AppSettings["timeout"])));

            //跳转回分站
            if (Request.QueryString["BackURL"] != null)
            {
                return JavaScript("window.location.href='" + Server.UrlDecode(Request.QueryString["BackURL"]) + "'");
            }

            #endregion

            //var url = Url.RouteUrl("User", true, new { action = "Index", controller = "Home", HostName = account.HostName });
            var url = Url.RouteUrl("User", true, new RouteValueDictionary(new { action = "Index", controller = "Home", HostName = account.HostName }));
            return JavaScript("window.location.href='" + url + "'");
        }


        #endregion
    }
}
