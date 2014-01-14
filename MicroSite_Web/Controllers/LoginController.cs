using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Interface;
using Injection;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace Web.Controllers
{
    public class LoginController : BaseController, System.Web.SessionState.IRequiresSessionState
    {
        #region 平台

        public ActionResult Index()
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
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(string phone_email, string password)
        {
            string superAdminStr = System.Configuration.ConfigurationManager.AppSettings["SuperAdmin"];
            string l_loginName = superAdminStr.Split('|')[0];
            string l_loginPwd = superAdminStr.Split('|')[1];
            if (l_loginName == phone_email && l_loginPwd == password)
            {
                Session[SystemConst.Session.LoginAccount] = new Poco.Account() { IsSuperAdmin = true, HostName = "MicroSite" };
                var url = Url.RouteUrl("User", new { action = "Index", controller = "Guide", HostName = "MicroSite" }, true);
                return JavaScript("window.location.href='" + url + "'");
            }
            else
            {
                IAccountModel accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
                var result = accountModel.Login(phone_email, password);
                if (result.HasError)
                {
                    return JavaScript("LandWaitFor('login','WaitImg',2);" + AlertJS_NoTag(new Dialog(result.Error)));
                }
                var account = Session[SystemConst.Session.LoginAccount] as Account;
                var url = Url.RouteUrl("User", new { action = "Index", controller = "Home", HostName = account.HostName }, true);
                return JavaScript("window.location.href='" + url + "'");
            }
        }
        #endregion
    }
}
