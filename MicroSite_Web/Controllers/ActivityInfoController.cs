using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Injection;
using Interface;
using Controllers;
using Poco.Enum;

namespace Web.Controllers
{
    public class ActivityInfoController : ManageAccountController
    {
        //
        // GET: /ActivityInfo/
        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int? id)
        {
            var activityInfoModel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);
            var activityinfo = activityInfoModel.GetListByAMID(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "活动", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(activityinfo);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "活动 - 创建活动", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="activityinfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ActivityInfo activityinfo)
        {
            activityinfo.CreateDate = DateTime.Now;
            activityinfo.AccountID = LoginAccount.ID;
            activityinfo.AccountMainID = LoginAccount.CurrentAccountMainID;
            var activityInfoModel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);
            var result = activityInfoModel.Add(activityinfo);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "ActivityInfo", new { HostName = LoginAccount.HostName }) + "'");

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ActionId"></param>
        /// <returns></returns>
        public ActionResult Edit(int ActionId)
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "活动 - 修改活动", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            var activityInfoModel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);
            var actiovity = activityInfoModel.GetActivityByID(ActionId, LoginAccount.CurrentAccountMainID);

            return View(actiovity);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="activityinfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ActivityInfo activityinfo)
        {
            activityinfo.AccountID = LoginAccount.ID;
            var activityInfoModel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);

            var result = activityInfoModel.Edit(activityinfo);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "ActivityInfo", new { HostName = LoginAccount.HostName }) + "'");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ActionId"></param>
        /// <returns></returns>
        public ActionResult Delete(int ActionId)
        {
            var activityInfoModel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);
            var result = activityInfoModel.DeleteActivityByID(ActionId, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "ActivityInfo", new { HostName = LoginAccount.HostName }) + "'");
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <returns></returns>
        [AllowCheckPermissions(false)]
        public ActionResult SetMainStatus(int id, int Status)
        {
            var activityInfoModel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);
            var result = activityInfoModel.SetMainStatus(id, LoginAccount.CurrentAccountMainID, Status);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "ActivityInfo", new { HostName = LoginAccount.HostName }) + "'");
        }



        /// <summary>
        /// 生成软文 ajax
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client">客户端 EnumAdvertorialUType</param>
        /// <returns>OK 成功 NO失败</returns>
        [AllowCheckPermissions(false)]
        public string SetAppAdverTorial(int id, int client)
        {
            AppAdvertorial appRW = new AppAdvertorial();
            Result result = new Result();
            var AdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            //主表
            var activityInfoModel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);
            var main = activityInfoModel.GetActivityByID(id, LoginAccount.CurrentAccountMainID);
            appRW.Content = "";
            appRW.ContentURL = "http://" + SystemConst.WebUrl + "/Default/ActivityInfo?ActivityID=" + id;
            appRW.EnumAdverURLType = (int)EnumAdverURLType.Activities;
            appRW.AccountMainID = LoginAccount.CurrentAccountMainID;
            appRW.AppShowImagePath = "~/Images/ActivityInfo.png";
            appRW.MainImagPath = "~/Images/ActivityInfo.png";
            appRW.MinImagePath = "~/Images/ActivityInfo.png";
            appRW.Depict = main.Title;
            appRW.EnumAdverTorialType = (int)EnumAdverTorialType.url;
            appRW.EnumAdvertorialUType = client;
            appRW.IssueDate = DateTime.Now;
            appRW.Sort = 0;
            appRW.stick = 0;
            appRW.SystemStatus = 0;
            appRW.Title = main.Title;

            result = AdvertorialModel.Add(appRW);
            if (result.HasError == true)
            {
                return "No";
            }
            else
            {
                return "OK";
            }
        }


        /// <summary>
        /// 展示报表人
        /// </summary>
        /// <param name="id"></param>
        /// <param name="AID"></param>
        /// <returns></returns>
        public ActionResult ShowRegistration(int? id, int AID)
        {
            var ActivityInfoParticipatorModel = Factory.Get<IActivityInfoParticipatorModel>(SystemConst.IOC_Model.ActivityInfoParticipatorModel);
            var ActivityInfoParticipator = ActivityInfoParticipatorModel.GetAIPList(AID, LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 50);
            return View(ActivityInfoParticipator);
        }
    }
}
