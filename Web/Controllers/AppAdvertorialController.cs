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
    public class AppAdvertorialController : ManageAccountController
    {
        //
        // GET: /AppAdvertorial/

        public ActionResult Index(int? id)
        {
            ViewBag.HostName = LoginAccount.HostName;

            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var list = AppAdvertorialModel.GetList(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);


            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-App动态软文", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }

        public ActionResult Add()
        {
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-App动态软文-添加项目", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(AppAdvertorial appAdver, HttpPostedFileBase HousShowImagePathFile, int w, int h, int x1, int y1, int tw, int th)
        {
            appAdver.AccountMainID = LoginAccount.CurrentAccountMainID;
            appAdver.Sort = 0;
            appAdver.IssueDate = DateTime.Now;
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            Result result = AppAdvertorialModel.AddAppAdvertorial(appAdver, HousShowImagePathFile,w,h,x1,y1,tw,th);
          

            return RedirectToAction("Index", "AppAdvertorial");
        }

        public ActionResult Edit(int id)
        {
            ViewBag.HostName = LoginAccount.HostName;
          
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "设置-App动态软文-添加项目", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var AppAdvertorial = AppAdvertorialModel.Get(id);
            ViewBag.stick = AppAdvertorial.stick;
            return View(AppAdvertorial);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(AppAdvertorial appadver, HttpPostedFileBase HousShowImagePathFile, int w, int h, int x1, int y1, int tw, int th)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            Result result = AppAdvertorialModel.EditAppAdvertorial(appadver, HousShowImagePathFile, w, h, x1, y1, tw, th);
            if (result.HasError)
            {
                return JavaScript(" isCommit = true;" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            return RedirectToAction("Index", "AppAdvertorial");
        }

        public ActionResult Delete(int id)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var result = AppAdvertorialModel.DelAppAdvertorial(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "AppAdvertorial", new { HostName = LoginAccount.HostName }) + "'");
        }

        //校验是否可以置顶
        [HttpPost]
        [AllowCheckPermissions(false)]
        public string chickStick(int ID)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var AppAdvertorial = AppAdvertorialModel.GetList(LoginAccount.CurrentAccountMainID);
            if (AppAdvertorial.Where(a => a.stick == 1).Count() >= 5)
            {
                return "No";
            }
            else
            {
                return "Yes";
            }
            
        }
        //校验修改是否可以置顶
        [HttpPost]
        [AllowCheckPermissions(false)]
        public string chickUpdStick(int ID)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var AppAdvertorial = AppAdvertorialModel.GetList(LoginAccount.CurrentAccountMainID);
            if (AppAdvertorial.Where(a => a.stick == 1&&a.ID!=ID).Count() >= 5)
            {
                return "No";
            }
            else
            {
                return "Yes";
            }

        }

        //置顶 isok =1 置顶 0 取消置顶
        [AllowCheckPermissions(false)]
        [HttpPost]
        public ActionResult Stick(int AdvertorialID,int isok,int Sort)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);

            AppAdvertorialModel.EditAppAdvertorialStick(AdvertorialID, isok,LoginAccount.CurrentAccountMainID,Sort);

            return RedirectToAction("Index", "AppAdvertorial", new { HostName = LoginAccount.HostName });
        }

        //排序
        [AllowCheckPermissions(false)]
        [HttpPost]
        public ActionResult Sort(int AdvertorialID,int Sort,int Type)
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);

            AppAdvertorialModel.EditAppAdvertorialSort(AdvertorialID, LoginAccount.CurrentAccountMainID, Sort, Type);

            return RedirectToAction("Index", "AppAdvertorial", new { HostName = LoginAccount.HostName });
        }

        //预览
        [AllowCheckPermissions(false)]
        public ActionResult Preview()
        {
            var AppAdvertorialModel = Factory.Get<IAppAdvertorialModel>(SystemConst.IOC_Model.AppAdvertorialModel);
            var list = AppAdvertorialModel.GetList(LoginAccount.CurrentAccountMainID);
            ViewBag.TitleImg = list.Where(a => a.stick == 1).ToPagedList(1, 5);
            ViewBag.ListImg = list.Where(a => a.stick == 0).ToPagedList(1, 5);

            return View();
        }

    }
}
