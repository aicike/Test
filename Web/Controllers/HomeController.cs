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

namespace Web.Controllers
{
    public class HomeController : ManageAccountController
    {
        public ActionResult Index()
        {
            //判断是否打开配置向导
            //获取网站管理员
            var model = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = model.GetAccountAdminListByAccountMain(LoginAccount.CurrentAccountMainID);
            if (LoginAccount.ID == account.ToList()[0].ID)
            {
                AccountMainGuideModel AMG = new AccountMainGuideModel();
                bool isUntreated;
                AMG.getUntreated(LoginAccount.CurrentAccountMainID, out isUntreated);
                if (isUntreated)
                {
                    ViewBag.AccountAdmin = "true";
                }
                else
                {
                    ViewBag.AccountAdmin = "false";
                }
            }
            else
            {
                ViewBag.AccountAdmin = "false";
            }


            return View();
        }
    }
}
