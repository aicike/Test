using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interface;
using Poco;
using Injection;
using Controllers;

namespace Web.Controllers
{
    public class TaskRuleController : ManageAccountController
    {
        public ActionResult Index(int? id)
        {
            var taskRuleModel = Factory.Get<ITaskRuleModel>(SystemConst.IOC_Model.TaskRuleModel);
            var list = taskRuleModel.GetByAccountMainID(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-任务规则制定", LoginAccount.CurrentAccountMainName, SystemConst.WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.HostName = LoginAccount.HostName;
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-任务规则制定", LoginAccount.CurrentAccountMainName, SystemConst.WebTitleRemark);
            ViewBag.HostName = LoginAccount.HostName;
            return View();
        }

        [HttpPost]
        public ActionResult Add(TaskRule taskRule)
        {
            var taskRuleModel = Factory.Get<ITaskRuleModel>(SystemConst.IOC_Model.TaskRuleModel);
            taskRule.AccountMainID = LoginAccount.CurrentAccountMainID;
            var result = taskRuleModel.Add(taskRule);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "TaskRule", new { HostName = LoginAccount.CurrentAccountMainName }) + "'");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ITaskRuleModel taskRuleModel = Factory.Get<ITaskRuleModel>(SystemConst.IOC_Model.TaskRuleModel);
            var result = taskRuleModel.Get(id);
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-任务规则制定", LoginAccount.CurrentAccountMainName, SystemConst.WebTitleRemark);
            ViewBag.HostName = LoginAccount.HostName;
            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(TaskRule taskRule)
        {
            ITaskRuleModel taskRuleModel = Factory.Get<ITaskRuleModel>(SystemConst.IOC_Model.TaskRuleModel);
            taskRule.AccountMainID = LoginAccount.CurrentAccountMainID;
            var result = taskRuleModel.Edit(taskRule);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "TaskRule", new { HostName = LoginAccount.CurrentAccountMainName }) + "'");
        }

        public ActionResult Delete(int id)
        {
            ITaskRuleModel taskRuleModel = Factory.Get<ITaskRuleModel>(SystemConst.IOC_Model.TaskRuleModel);
            var result = taskRuleModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "TaskRule", new { HostName = LoginAccount.CurrentAccountMainName }) + "'");
        }
    }
}
