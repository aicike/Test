using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;
using Poco.Enum;

namespace Web.Controllers
{
    public class AdvertisingController : ManageAccountController
    {
        //
        // GET: /Advertising/

        public ActionResult Index(int ? id)
        {
            ViewBag.HostName = LoginAccount.HostName;
            ViewBag.AMID = LoginAccount.CurrentAccountMainID;
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var list = AppAdvertorialModel.GetList(LoginAccount.CurrentAccountMainID, (int)EnumAdvertorialUType.UserEnd, (int)EnumAdverClass.Advertising).ToPagedList(id ?? 1, 15);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-广而告之", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(list);
        }


        public ActionResult Add()
        {
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-广而告之-添加", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(AppAdvertorial appAdver, int w, int h, int x1, int y1, int tw, int th)
        {
            appAdver.stick = 0;
            appAdver.EnumAdverTorialType = 0;
            appAdver.AccountMainID = LoginAccount.CurrentAccountMainID;
            appAdver.Sort = 0;
            appAdver.IssueDate = DateTime.Now;
            appAdver.EnumAdvertorialUType = (int)EnumAdvertorialUType.UserEnd;
            appAdver.BrowseCnt = 0;
            appAdver.EnumAdverClass = (int)EnumAdverClass.Advertising;
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            Result result = AppAdvertorialModel.AddAppAdvertorial(appAdver, w, h, x1, y1, tw, th);
            return RedirectToAction("Index", "Advertising");
        }
    }
}
