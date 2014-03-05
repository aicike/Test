using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Poco;
using Business;
using Controllers;
using Interface;

namespace Web.Controllers
{
    public class AutoMessageReplyController : ManageAccountController
    {
        public ActionResult Index()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "被添加自动回复-编辑", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            var model = Factory.Get<IAutoMessage_ReplyModel>(SystemConst.IOC_Model.AutoMessage_ReplyModel);
            AutoMessage_Reply amr = model.GetByAccountID(LoginAccount.ID);
            if (amr == null)
            {
                amr = new AutoMessage_Reply();
            }
            if (TempData["Msg"] != null)
            {
                var msg = TempData["Msg"].ToString();
                ViewBag.Msg = msg;
                ViewBag.HasError = 1;
            }
            if (TempData["HasError"] != null) {
                ViewBag.HasError = TempData["HasError"].ToString();
            }
            return View(amr);
        }

        [HttpPost]
        public ActionResult Index(AutoMessage_Reply amr)
        {
            var model = Factory.Get<AutoMessage_ReplyModel>(SystemConst.IOC_Model.AutoMessage_ReplyModel);
            Result result = null;
            if (amr.ID != 0)
            {
                //修改
                amr.AccountID = LoginAccount.ID;
                result = model.Edit(amr);
            }
            else
            {
                //添加
                amr.AccountID = LoginAccount.ID;
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
            return RedirectToAction("Index", "AutoMessageReply", new { HostName = LoginAccount.HostName });
        }

    }
}
