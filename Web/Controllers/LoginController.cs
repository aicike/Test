using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Interface;
using Injection;

namespace Web.Controllers
{
    public class LoginController : BaseController
    {
        #region 平台

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 平台登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SystemLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SystemLogin(SystemUser user)
        {
            ISystemUserModel systemUserModel = Factory.Get<ISystemUserModel>(SystemConst.IOC_Model.SystemUserModel);
            var result = systemUserModel.Login(user.Email, user.LoginPwdPage);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
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
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin(Account user)
        {
            IAccountModel accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var result = accountModel.Login(user.Email, user.LoginPwdPage);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            var account = Session[SystemConst.Session.LoginAccount] as Account;
            var url = Url.RouteUrl("User", new { action = "Index", controller = "Home", HostName = account.HostName });
            return JavaScript("window.location.href='" + url + "'");
        }

        #endregion
    }
}
