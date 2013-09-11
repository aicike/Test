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
    public class InstallAppReplyController : ManageAccountController
    {
        //
        // GET: /InstallAppReply/

        public ActionResult Index()
        {
            var AutoAddModel = Factory.Get<IAutoMessage_AddModel>(SystemConst.IOC_Model.AutoMessage_AddModel);
            var AutoAdd = AutoAddModel.GetInfo(LoginAccount.CurrentAccountMainID);
            ViewBag.HostName = LoginAccount.HostName;
            if (AutoAdd != null)
            {
                return View(AutoAdd);
            }
            else
            {
                AutoMessage_Add Autpadd = new AutoMessage_Add();
                return View(Autpadd);
            }
        }

        [HttpPost]
        public ActionResult AddOrUpd(AutoMessage_Add AutoMessAdd)
        {
            var AutoAddModel = Factory.Get<IAutoMessage_AddModel>(SystemConst.IOC_Model.AutoMessage_AddModel);
            var AutoAdd = AutoAddModel.GetInfo(LoginAccount.CurrentAccountMainID);
            Result result = null;
            if (AutoAdd != null)
            {
                AutoAdd.Content = AutoMessAdd.Content;
                result = AutoAddModel.Edit(AutoAdd);
            }
            else
            {
                AutoMessAdd.AccountMainID = LoginAccount.CurrentAccountMainID;
                result = AutoAddModel.Add(AutoMessAdd);
            }
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error, Url.Action("Index", "InstallAppReply", new { HostName = LoginAccount.HostName })));
            }
            else
            {
                return Alert(new Dialog("保存成功", Url.Action("Index", "InstallAppReply", new { HostName = LoginAccount.HostName })));
            }
        }

        public ActionResult Delete(int id)
        {
            var AutoAddModel = Factory.Get<IAutoMessage_AddModel>(SystemConst.IOC_Model.AutoMessage_AddModel);
            AutoAddModel.Delete(id);
            return RedirectToAction("Index", "InstallAppReply", new { HostName = LoginAccount.HostName });
        }
    }
}
