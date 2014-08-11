using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Controllers;
using Injection;
using Interface;

namespace Web.Controllers
{
    public class RentalHouseController : ManageAccountController
    {
        //
        // GET: /RentalHouse/

        public ActionResult Index(int? id)
        {
            var rentalhouseModel = Factory.Get<IRentalHouseModel>(SystemConst.IOC_Model.RentalHouseModel);
            var list = rentalhouseModel.GetList(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 20);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-房屋租赁", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.AMID = LoginAccount.CurrentAccountMainID;
            return View(list);
        }

        public ActionResult Add()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-房屋租赁-添加信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(RentalHouse rentalhouse, int w, int h, int x1, int y1, int tw, int th)
        {
            rentalhouse.AccountMainID = LoginAccount.CurrentAccountMainID;
            var rentalhouseModel = Factory.Get<IRentalHouseModel>(SystemConst.IOC_Model.RentalHouseModel);
            rentalhouseModel.AddInfo(rentalhouse, w, h, x1, y1, tw, th);
            return RedirectToAction("Index", "RentalHouse");

        }


        public ActionResult Edit(int id)
        {
            var rentalhouseModel = Factory.Get<IRentalHouseModel>(SystemConst.IOC_Model.RentalHouseModel);
            var rentalhouse = rentalhouseModel.GetInfo(id, LoginAccount.CurrentAccountMainID);
            ViewBag.status = rentalhouse.Stauts;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-房屋租赁-修改信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(rentalhouse);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(RentalHouse rentalhouse, int w, int h, int x1, int y1, int tw, int th)
        {
            var rentalhouseModel = Factory.Get<IRentalHouseModel>(SystemConst.IOC_Model.RentalHouseModel);
            Result result = rentalhouseModel.EditInfo(rentalhouse, w, h, x1, y1, tw, th);

            return RedirectToAction("Index", "RentalHouse");
        }

        public ActionResult Delete(int id)
        {
            var rentalhouseModel = Factory.Get<IRentalHouseModel>(SystemConst.IOC_Model.RentalHouseModel);
            var result = rentalhouseModel.Delete(id, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "RentalHouse") + "'");
        }
    }
}
