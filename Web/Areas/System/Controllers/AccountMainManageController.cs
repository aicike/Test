using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interface;
using Injection;
using Web.Controllers;
using Poco;
using Poco.Enum;
using Common;

namespace Web.Areas.System.Controllers
{
    public class AccountMainManageController : ManageSystemUserController
    {
        public ActionResult Index(int? id)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var list = accountMainModel.List_Permission(LoginSystemUser.ID).ToPagedList(id ?? 1, 15);
            return View(list);
        }

        public ActionResult AddAccountMain()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAccountMain(AccountMain accountMain, HttpPostedFileBase LogoImagePathFile, HttpPostedFileBase AndroidPathFile, HttpPostedFileBase AndroidSellPathFile,HttpPostedFileBase AppLogoImageFile)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var result = accountMainModel.Add(accountMain, LogoImagePathFile, LoginSystemUser.ID, AndroidPathFile, AndroidSellPathFile, AppLogoImageFile);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            return RedirectToAction("Index", "AccountMainManage");
         

        }

        public ActionResult EditAccountMain(int id)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var entity = accountMainModel.Get_Permission(id, LoginSystemUser.ID);
            return View(entity);
        }

        [HttpPost]
        public ActionResult EditAccountMain(AccountMain accountMain, HttpPostedFileBase LogoImagePathFile, HttpPostedFileBase AndroidPathFile, HttpPostedFileBase AndroidSellPathFile, HttpPostedFileBase AppLogoImageFile)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var result = accountMainModel.Edit_Permission(accountMain, LogoImagePathFile, AndroidPathFile, AndroidSellPathFile, AppLogoImageFile, LoginSystemUser.ID);
            if (result.HasError)
            {
                throw new ApplicationException(result.Error);
            }
            return RedirectToAction("Index", "AccountMainManage");
            
        }

        public ActionResult DeleteAccountMain(int id)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var result = accountMainModel.Delete_Permission(id, LoginSystemUser.ID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + ViewBag.RawUrl + "'");
        }

        public ActionResult SetAccountMainStatus(int accountMainID, string status)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            EnumAccountStatus accountStatus = (EnumAccountStatus)Enum.Parse(typeof(EnumAccountStatus), status);
            var result = accountMainModel.ChangeStatus_Permission(accountMainID, accountStatus, LoginSystemUser.ID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + ViewBag.RawUrl + "'");
        }

        public ActionResult ViewAccountMain(int id)
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var entity = accountMainModel.Get_Permission(id, LoginSystemUser.ID);
            double usedCapacity = accountMainModel.GetFileLimitUseInfo(id);
            double availableCapacity = entity.FileLimit - usedCapacity;
            ViewBag.AvailableCapacity = availableCapacity.ToString("f2");
            ViewBag.StyleWidth = Convert.ToInt32(usedCapacity / entity.FileLimit * 200);
            return View(entity);
        }

        public string GetHostName(string value)
        {
            return ChineseSpell.GetFirstCharSpell(value);
        }
    }
}
