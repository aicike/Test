using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;
using Business;
using System.Data;

namespace Web.Controllers
{
    public class HomeController : ManageAccountController
    {
        [AllowCheckPermissions(false)]
        public ActionResult Index()
        {
            ViewBag.AccountID = LoginAccount.ID;
            //判断是否打开配置向导
            //获取网站管理员
            var model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            //var account = model.GetAccountAdminListByAccountMain(LoginAccount.CurrentAccountMainID);
            //if (account != null)
            //{
            //    if (account.Count() > 0)
            //    {
            //        if (LoginAccount.ID == account.ToList()[0].ID)
            //        {
            //            AccountMainGuideModel AMG = new AccountMainGuideModel();
            //            bool isUntreated;
            //            AMG.getUntreated(LoginAccount.CurrentAccountMainID, out isUntreated);
            //            if (isUntreated)
            //            {
            //                ViewBag.AccountAdmin = "true";
            //            }
            //            else
            //            {
            //                ViewBag.AccountAdmin = "false";
            //            }
            //        }
            //        else
            //        {
            //            ViewBag.AccountAdmin = "false";
            //        }
            //    }
            //    else
            //    {
            //        ViewBag.AccountAdmin = "false";
            //    }
            //} 
            ViewBag.AccountAdmin = "false";


            ViewBag.UNAme = LoginAccount.Name;

            //获取用户数
            var AccountUserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
            ViewBag.UserCnt = AccountUserModel.GetAccountUser(LoginAccount.ID);


            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "首页" ,LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;


            ViewBag.AMID = LoginAccount.CurrentAccountMainID;

            return View();
        }
    }
}
