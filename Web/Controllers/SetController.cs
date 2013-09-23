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
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-个人信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(account);
        }
        [AllowCheckPermissions(false)]
        [HttpPost]
        public ActionResult Edit(Account account, HttpPostedFileBase HeadImagePathFile)
        {
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);

            var result = accountModel.Edit(account, LoginAccount.CurrentAccountMainID, HeadImagePathFile);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return RedirectToAction("Index", "set", new { HostName = LoginAccount.HostName });
        }
    }
}
