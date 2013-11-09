using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Interface;
using Injection;

namespace Web.Controllers
{
    public class MyTaskController : ManageAccountController
    {
        public ActionResult Index(int? id)
        {
            var taskModel = Factory.Get<ITaskModel>(SystemConst.IOC_Model.TaskModel);
            var list = taskModel.GetMyTasks(LoginAccount.ID).ToPagedList(id ?? 1, 15);
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-我的任务", LoginAccount.CurrentAccountMainName, SystemConst.WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.HostName = LoginAccount.HostName;
            return View(list);
        }
    }
}
