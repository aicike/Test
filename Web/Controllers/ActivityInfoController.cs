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
        public ActionResult Add(ActivityInfo activityinfo, int w, int h, int x1, int y1, int tw, int th)
        {
            activityinfo.CreateDate = DateTime.Now;
            activityinfo.AccountID = LoginAccount.ID;
            activityinfo.AccountMainID = LoginAccount.CurrentAccountMainID;
            var activityInfoModel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);
            var result = activityInfoModel.AddActivity(activityinfo, x1, y1, w, h, tw, th);
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
            ViewBag.EndDate = actiovity.EnrollEndDate.ToString("yyyy-MM-dd");
            ViewBag.StratDate = actiovity.ActivityStratDate;
            return View(actiovity);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="activityinfo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ActivityInfo activityinfo, int w, int h, int x1, int y1, int tw, int th)
        {
            activityinfo.AccountID = LoginAccount.ID;
            var activityInfoModel = Factory.Get<IActivityInfoModel>(SystemConst.IOC_Model.ActivityInfoModel);

            var result = activityInfoModel.EditActivity(activityinfo,x1,y1,w,h,tw,th);
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
            appRW.ContentURL = SystemConst.WebUrlIP + "/Default/ActivityInfo?ActivityID_token=" + id.TokenEncrypt();
            appRW.EnumAdverURLType = (int)EnumAdverURLType.Activities;
            appRW.AccountMainID = LoginAccount.CurrentAccountMainID;
            if (string.IsNullOrEmpty(main.AppShowImagePath))
            {
                appRW.AppShowImagePath = "~/Images/ActivityInfo_show.jpg";
            }
            else
            {
                appRW.AppShowImagePath = main.AppShowImagePath;
            }
            if (string.IsNullOrEmpty(main.MainImagPath))
            {
                appRW.MainImagPath = "~/Images/ActivityInfo.jpg";
            }
            else
            {
                appRW.MainImagPath = main.MainImagPath;
            }
            if (string.IsNullOrEmpty(main.MinImagePath))
            {
                appRW.MinImagePath = "~/Images/ActivityInfo_MINI.jpg";
            }
            else
            {
                appRW.MinImagePath = main.MinImagePath;
            }
            appRW.Depict = main.Remarks;
            appRW.EnumAdverTorialType = (int)EnumAdverTorialType.url;
            appRW.EnumAdvertorialUType = client;
            appRW.IssueDate = DateTime.Now;
            appRW.Sort = 0;
            appRW.stick = 0;
            appRW.SystemStatus = 0;
            appRW.Title = main.Title;
            appRW.UrlID = main.ID;

            result = AdvertorialModel.Add(appRW);
            
            
            if (result.HasError == true)
            {
                return "No";
            }
            else
            {
                activityInfoModel.Update_GenerateType(id, client, 1);
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

        /// <summary>
        /// 展示签到人
        /// </summary>
        /// <param name="id"></param>
        /// <param name="AID"></param>
        /// <returns></returns>
        public ActionResult ShowSignIn(int? id, int AID)
        {
            var ActivitySingInModel = Factory.Get<IActivityInfoSignInModel>(SystemConst.IOC_Model.ActivityInfoSignInModel);
            var ActivityInfoParticipator = ActivitySingInModel.GetAIPList(AID, LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 50);
            return View(ActivityInfoParticipator);
        }
    }
}
