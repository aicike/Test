﻿using System;
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
            //判断web项目是独立部署还是云部署
            bool IsIndependence = System.Configuration.ConfigurationManager.AppSettings["IsIndependence"] == "true" ? true : false;
            if (IsIndependence)
            {
                //独立部署
                var amom = Factory.Get<IAccountMainOrganizationModel>(SystemConst.IOC_Model.AccountMainOrganizationModel);

                if (amom.List().Count() > 0)
                {
                    //已有账号
                    Result r = amom.Login(phone_email, password);
                    if (r.HasError == false)
                    {
                        var u = Url.RouteUrl("User", true, new RouteValueDictionary(new { action = "Index", controller = "Home", HostName = "www" }));
                        return JavaScript("window.location.href='" + u + "'");
                    }
                }
                else
                {
                    //没有账号，使用webconfig中账号
                    string IndependenceAccount = System.Configuration.ConfigurationManager.AppSettings["IndependenceAccount"];
                    Result r = new Result();
                    var loginInfo = IndependenceAccount.Split(',');
                    if (loginInfo.Length != 2)
                    {
                        r.Error = "账号配置出错，请联系平台管理员。";
                    }
                    if (phone_email.Equals(loginInfo[0], StringComparison.CurrentCultureIgnoreCase) == false || password.Equals(loginInfo[1]) == false)
                    {
                        r.Error = "用户名或密码错误。";
                    }
                    if (r.HasError)
                    {
                        return JavaScript("LandWaitFor('login','WaitImg',2);" + AlertJS_NoTag(new Dialog(r.Error)));
                    }
                    else
                    {
                        //var u = Url.RouteUrl("Default", true, new RouteValueDictionary(new { action = "Index", controller = "Guide" }));
                        var u = Url.RouteUrl("User", true, new RouteValueDictionary(new { action = "Index", controller = "Home", HostName = "www" }));
                        return JavaScript("window.location.href='" + u + "'");
                    }
                }
            }
            //云部署
            IAccountModel accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            Result result = accountModel.Login(phone_email, password);
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
            object info = Newtonsoft.Json.JsonConvert.SerializeObject(account);
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
