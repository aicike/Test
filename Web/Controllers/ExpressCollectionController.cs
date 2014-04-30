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
    public class ExpressCollectionController : ManageAccountController
    {
        //
        // GET: /ExpressCollection/

        public ActionResult Index(int? id, string OddNumber, string phone)
        {

            var expresscollectionModel = Factory.Get<IExpressCollectionModel>(SystemConst.IOC_Model.ExpressCollectionModel);
            var expresscollection = expresscollectionModel.GetListByAMID(LoginAccount.CurrentAccountMainID, OddNumber, phone).ToPagedList(id ?? 1, 30);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-快递代收", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            ViewBag.PageID = id ?? 1;
            return View(expresscollection);
        }

        public ActionResult Add()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-快递代收-添加快递单", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ExpressCollection ec)
        {
            ec.EnumExpressStatus = (int)EnumExpressStatus.Not;
            ec.EntryDate = DateTime.Now;
            ec.AccountMainID = LoginAccount.CurrentAccountMainID;
            var expresscollectionModel = Factory.Get<IExpressCollectionModel>(SystemConst.IOC_Model.ExpressCollectionModel);
            var result = expresscollectionModel.Add(ec);
            return JavaScript("window.location.href='" + Url.Action("Index", "ExpressCollection", new { HostName = LoginAccount.HostName }) + "'");
        }



        public ActionResult Edit(int id)
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "物业管理-快递代收-修改快递单", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            var expresscollectionModel = Factory.Get<IExpressCollectionModel>(SystemConst.IOC_Model.ExpressCollectionModel);
            var item = expresscollectionModel.GetInfoBuID(LoginAccount.CurrentAccountMainID, id);
            return View(item);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ExpressCollection ec)
        {
            var expresscollectionModel = Factory.Get<IExpressCollectionModel>(SystemConst.IOC_Model.ExpressCollectionModel);
            expresscollectionModel.Edit(ec);
            return JavaScript("window.location.href='" + Url.Action("Index", "ExpressCollection", new { HostName = LoginAccount.HostName }) + "'");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ActionId"></param>
        /// <returns></returns>
        public ActionResult Delete(int ID)
        {
            var expresscollectionModel = Factory.Get<IExpressCollectionModel>(SystemConst.IOC_Model.ExpressCollectionModel);
            expresscollectionModel.CompleteDelete(ID);
            return JavaScript("window.location.href='" + Url.Action("Index", "ExpressCollection", new { HostName = LoginAccount.HostName }) + "'");
        }

        /// <summary>
        /// 更改状态 改为当前相反的状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ThisStatus">当前状态</param>
        /// <returns></returns>
        [HttpPost]
        public string SetStatus(int ID, int ThisStatus)
        {
            var expresscollectionModel = Factory.Get<IExpressCollectionModel>(SystemConst.IOC_Model.ExpressCollectionModel);
            string titel = "";
            if (ThisStatus == (int)EnumExpressStatus.Not)
            {
                ThisStatus = (int)EnumExpressStatus.Havereceived;
                titel = "已领取";
            }
            else
            {
                ThisStatus = (int)EnumExpressStatus.Not;
                titel = "未领取";
            }
            Result result = expresscollectionModel.UpdStatus(ID,LoginAccount.CurrentAccountMainID,ThisStatus);
            if (result.HasError)
            {
                return "false";
            }
            else
            {
                return "true," + ThisStatus + "," + titel;
            }

        }
    }
}
