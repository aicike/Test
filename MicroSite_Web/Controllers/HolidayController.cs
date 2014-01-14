using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Controllers;

namespace Web.Controllers
{
    public class HolidayController : ManageAccountController
    {
        //
        // GET: /Holiday/

        public ActionResult Index(int? id)
        {

            var holidModel = Factory.Get<IHolidayModel>(SystemConst.IOC_Model.HolidayModel);
            var holidlist = holidModel.GetListByAMID(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);


            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "产品管理-节假日管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(holidlist);
        }


        public ActionResult Add()
        {
            ViewBag.TDate = DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "产品管理-节假日管理-添加", LoginAccount.CurrentAccountMainName, WebTitleRemark);
     
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(Holiday holiday)
        {

            holiday.AccountMainID = LoginAccount.CurrentAccountMainID;
            var holidModel = Factory.Get<IHolidayModel>(SystemConst.IOC_Model.HolidayModel);
            Result result = holidModel.Add(holiday);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Holiday") + "'");

        }

        public ActionResult Edit(int id)
        {
            var holidModel = Factory.Get<IHolidayModel>(SystemConst.IOC_Model.HolidayModel);
            var holid = holidModel.Get(id);

            ViewBag.TDate = holid.HoliDayValue;
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "产品管理-节假日管理-修改", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            return View(holid);
        }

        [HttpPost]
        public ActionResult Edit(Holiday holiday)
        {
            var holidModel = Factory.Get<IHolidayModel>(SystemConst.IOC_Model.HolidayModel);
            Result result = holidModel.Edit(holiday);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "Holiday") + "'");
        }


        public ActionResult Delete(int id)
        {
            var holidModel = Factory.Get<IHolidayModel>(SystemConst.IOC_Model.HolidayModel);
            Result result = holidModel.CompleteDelete(id);

            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Holiday") + "'");
        }
    }
}
