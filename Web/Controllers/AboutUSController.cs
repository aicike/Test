using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Controllers;
using Injection;
using Interface;

namespace Web.Controllers
{
    public class AboutUSController : ManageAccountController
    {
        //
        // GET: /AboutUS/

        public ActionResult Index()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "App设置-关于我们", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            var model = Factory.Get<IAboutUSModel>(SystemConst.IOC_Model.AboutUSModel);
            var item = model.GetAboutUS(LoginAccount.CurrentAccountMainID);

            if (TempData["Msg"] != null)
            {
                var msg = TempData["Msg"].ToString();
                ViewBag.Msg = msg;
                ViewBag.HasError = 1;
            }
            if (TempData["HasError"] != null)
            {
                ViewBag.HasError = TempData["HasError"].ToString();
            }
            return View(item);
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(AboutUS amr)
        {
            var model = Factory.Get<IAboutUSModel>(SystemConst.IOC_Model.AboutUSModel);

            Result result = null;
            if (amr.ID != 0)
            {
                //修改
                result = model.Edit(amr);
            }
            else
            {
                //添加
                amr.AccountMainID = LoginAccount.CurrentAccountMainID;
                result = model.Add(amr);
            }
            if (result.HasError)
            {
                TempData["Msg"] = result.Error;
                TempData["HasError"] = 1;
            }
            else
            {
                TempData["HasError"] = 0;
            }
            return RedirectToAction("Index", "AboutUS", new { HostName = LoginAccount.HostName });
        }
    }
}
