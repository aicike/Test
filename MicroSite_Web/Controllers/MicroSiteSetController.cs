using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;

namespace MicroSite_Web.Controllers
{
    public class MicroSiteSetController : ManageAccountController
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.WeiURL = SystemConst.MicroSiteHostName + "." + SystemConst.WebUrl + "/MicroMall/ShopIndex?AMID=" + LoginAccount.CurrentAccountMainID;
            ViewBag.HostName = LoginAccount.HostName;
            var model = Factory.Get<IMicroSiteSetInfoModel>(SystemConst.IOC_Model.MicroSiteSetInfoModel);
            var entity = model.GetByAccountMainID(LoginAccount.CurrentAccountMainID);
            return View(entity);
        }

        [HttpPost]
        public ActionResult Index(MicroSiteSetInfo setInfo)
        {
            var model = Factory.Get<IMicroSiteSetInfoModel>(SystemConst.IOC_Model.MicroSiteSetInfoModel);
            setInfo.AccountMainID = LoginAccount.CurrentAccountMainID;
            var result = model.Edit(setInfo);
            if (result.HasError)
            {
                return JavaScript(string.Format("JMessage('{0}',true)", result.Error));
            }
            else {
                return JavaScript(string.Format("JMessage('{0}')","设置成功。"));
            }
        }
    }
}
