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
    public class HousesMangeController : ManageAccountController
    {
        //
        // GET: /HousesMange/

        public ActionResult Index(int? index)
        {
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var list = hounsesModel.GetList(LoginAccount.CurrentAccountMainID).ToPagedList(index ?? 1, 15);
            ViewBag.HostName = LoginAccount.HostName;
            return View(list);
        }

        public ActionResult Select(int id)
        {
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var Hounses = hounsesModel.Get(id);
            return View(Hounses);
        }

        public ActionResult Add()
        {
            ViewBag.HostName = LoginAccount.HostName;
            return View();
        }

        [HttpPost]
        public ActionResult Add(AccountMainHouses Hounses)
        {
            Hounses.AccountMainID = LoginAccount.CurrentAccountMainID;
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var result = hounsesModel.Add(Hounses);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "HousesMange", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Edit(int id)
        {
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var Hounses = hounsesModel.Get(id);
            ViewBag.HostName = LoginAccount.HostName;
            return View(Hounses);
        }

        [HttpPost]
        public ActionResult Edit(AccountMainHouses Hounses)
        {
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var result = hounsesModel.Edit(Hounses);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "HousesMange", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Delete(int id)
        {
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var result = hounsesModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "HousesMange", new { HostName = LoginAccount.HostName }) + "'");
        }
    }
}
