using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.WebAPI_Poco;

namespace Web.Controllers
{
    public class WebRequest_TaskController : Controller
    {
        /// <summary>
        /// 获取我创建的任务列表
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public string GetMyCreateTask(int accountID)
        {
            var taskModel = Factory.Get<ITaskModel>(SystemConst.IOC_Model.TaskModel);
            var query = taskModel.GetByCreateAccountID(accountID).OrderBy(a => a.EnumTaskStatus).OrderByDescending(a => a.ID);
            var list = query.ToList().Select(a => new App_Task()
            {
                ID = a.ID,
                AccountMainID = a.AccountMainID,
                CreateAccountID = a.CreateAccountID,
                CreateAccount = a.CreateAccount.Name,
                ReceiverAccountID = a.ReceiverAccountID,
                ReceiverAccount = a.ReceiverAccount.Name,
                CreateDate = a.CreateDate.ToString("yyyy-MM-dd"),
                BeginDate = a.BeginDate.ToString("yyyy-MM-dd"),
                EndDate = a.EndDate.ToString("yyyy-MM-dd"),
                EnumTaskStatus = a.EnumTaskStatus,
                TaskSpecification = a.TaskSpecification
            });
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取包含我的任务列表
        /// </summary>
        /// <returns></returns>
        public string GetMyTaskList(int accountID) {
            var taskModel = Factory.Get<ITaskModel>(SystemConst.IOC_Model.TaskModel);
            var query = taskModel.GetByCreateAccountID(accountID).OrderBy(a => a.EnumTaskStatus).OrderByDescending(a => a.ID);
            var list = query.ToList().Select(a => new App_Task()
            {
                ID = a.ID,
                AccountMainID = a.AccountMainID,
                CreateAccountID = a.CreateAccountID,
                CreateAccount = a.CreateAccount.Name,
                ReceiverAccountID = a.ReceiverAccountID,
                ReceiverAccount = a.ReceiverAccount.Name,
                CreateDate = a.CreateDate.ToString("yyyy-MM-dd"),
                BeginDate = a.BeginDate.ToString("yyyy-MM-dd"),
                EndDate = a.EndDate.ToString("yyyy-MM-dd"),
                EnumTaskStatus = a.EnumTaskStatus,
                TaskSpecification = a.TaskSpecification
            });
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
    }
}
