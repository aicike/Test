using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;

namespace Web.Controllers
{
    public class SetController : ManageAccountController
    {
        /// <summary>
        /// 首页
        /// </summary>
        public ActionResult Index()
        {
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = accountModel.Get(LoginAccount.ID);
            return View(account);
        }
    }
}
