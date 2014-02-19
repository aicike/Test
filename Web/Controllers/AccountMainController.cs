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
    public class AccountMainController : ManageAccountController
    {
        public ActionResult Index(int? id)
        {
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var accountMain=accountMainModel.Get(LoginAccount.CurrentAccountMainID);
            if (accountMain.IsOrganization.HasValue == false || accountMain.IsOrganization.Value==false)
            {
                false.NotAuthorizedPage();
            }
            var list= accountMainModel.ListForOrganization(LoginAccount.ID).ToPagedList(id ?? 1, 15);
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "项目和账号管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AccountMain accountMain, HttpPostedFileBase LogoImagePathFile, HttpPostedFileBase AndroidPathFile, HttpPostedFileBase AndroidSellPathFile, HttpPostedFileBase AppLogoImageFile)
        {
            //IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);

            //var result = accountMainModel.Add(accountMain, LogoImagePathFile, null, AndroidPathFile, AndroidSellPathFile, AppLogoImageFile);
            //if (result.HasError)
            //{
            //    throw new ApplicationException(result.Error);
            //}
            return RedirectToAction("Index", "AccountMainManage");
        }
    }
}
