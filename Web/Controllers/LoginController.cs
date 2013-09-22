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

namespace Web.Controllers
{
    public class LoginController : BaseController, System.Web.SessionState.IRequiresSessionState
    {
        #region 平台

        public ActionResult Index()
        {
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
            string webTitle = string.Format(SystemConst.Business.WebTitle, "登陆", WebTitleRemark, "");
            ViewBag.Title = webTitle;
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
            string webTitle = string.Format(SystemConst.Business.WebTitle, "登陆", WebTitleRemark, "");
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(Account user)
        {
            IAccountModel accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var result = accountModel.Login(user.Email, user.LoginPwdPage);
            if (result.HasError)
            {
                //return Alert(new Dialog(result,null,"alert('asdf')"));
                return JavaScript("LandWaitFor('login','WaitImg',2);" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            var account = Session[SystemConst.Session.LoginAccount] as Account;
            var url = Url.RouteUrl("User", new { action = "Index", controller = "Home", HostName = account.HostName });
            return JavaScript("window.location.href='" + url + "'");
        }
        

        #endregion
    }
}
