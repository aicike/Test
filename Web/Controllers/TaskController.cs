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
    public class TaskController : ManageAccountController
    {
        public ActionResult Index(int? id)
        {
            var taskModel = Factory.Get<ITaskModel>(SystemConst.IOC_Model.TaskModel);
            var list = taskModel.GetByCreateAccountID(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-任务管理", LoginAccount.CurrentAccountMainName, SystemConst.WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.HostName = LoginAccount.HostName;
            return View(list);
        }

        [HttpGet]
        public ActionResult Add()
        {
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-任务管理", LoginAccount.CurrentAccountMainName, SystemConst.WebTitleRemark);

            var taskRuleModel = Factory.Get<ITaskRuleModel>(SystemConst.IOC_Model.TaskRuleModel);
            var taskRules = taskRuleModel.GetByAccountMainID(LoginAccount.CurrentAccountMainID).ToList();
            var selectListTaskRule = new SelectList(taskRules, "ID", "TaskInfoName");
            List<SelectListItem> newTaskRuleList = new List<SelectListItem>();
            newTaskRuleList.Add(new SelectListItem { Text = "请选择", Value = "select", Selected = true });
            newTaskRuleList.AddRange(selectListTaskRule);
            ViewData["TaskRule"] = newTaskRuleList;

            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var subAccountList = accountModel.GetSubAccounts(LoginAccount.ID);
            var selectListSubAccounts = new SelectList(subAccountList, "ID", "Name");
            List<SelectListItem> newSubAccountList = new List<SelectListItem>();
            newSubAccountList.Add(new SelectListItem { Text = "请选择", Value = "select", Selected = true });
            newSubAccountList.AddRange(selectListTaskRule);
            ViewData["AccountList"] = newSubAccountList;

            return View();
        }

        [HttpPost]
        public ActionResult Add(Task task)
        {
            var taskModel = Factory.Get<ITaskModel>(SystemConst.IOC_Model.TaskModel);
            var result = taskModel.Add(task);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Task", new { HostName = LoginAccount.CurrentAccountMainName }));
        }
    }
}
