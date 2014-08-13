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
    public class PropertyUserController : ManageAccountController
    {
        public ActionResult Index(int phID)
        {
            var property_UserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
            var list = property_UserModel.GetHouseByPropertyHouseID(phID, LoginAccount.CurrentAccountMainID);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "业主信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }

    }
}
