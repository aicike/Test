using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Interface;
using Injection;
using Poco;

namespace Web.Controllers
{
    public class BasisSetController : ManageAccountController
    {
        //
        // GET: /BasisSet/

        public ActionResult Index()
        {
            ViewBag.HostName = LoginAccount.HostName;
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var entity = accountMainModel.Get(LoginAccount.CurrentAccountMainID);
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-基础设置", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(entity);
        }

        //修改数据
        [AllowCheckPermissions(false)]
        public ActionResult Edit(AccountMain accountMain, HttpPostedFileBase LogoImagePathFile)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var entity = accountMainModel.Edit_ByAccountMain(accountMain, LogoImagePathFile);
            return RedirectToAction("Index", "BasisSet", new { HostName = LoginAccount.HostName });
        }
    }
}
