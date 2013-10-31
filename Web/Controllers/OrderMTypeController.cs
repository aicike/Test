using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Injection;
using Interface;

namespace Web.Controllers
{
    public class OrderMTypeController : ManageAccountController
    {
        //
        // GET: /OrderMType/

        public ActionResult Index(int? id)
        {

            var orderModel = Factory.Get<IOrderMTypeModel>(SystemConst.IOC_Model.OrderMTypeModel);
            var list = orderModel.GetList(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);

            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "订单类型管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }


        public ActionResult Add()
        {
            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "订单类型管理-添加订单类型", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View();
        }

        [HttpPost]
        public ActionResult Add(OrderMType orderMType)
        {
            var orderModel = Factory.Get<IOrderMTypeModel>(SystemConst.IOC_Model.OrderMTypeModel);
            orderMType.AccountMainID = LoginAccount.CurrentAccountMainID;
            Result result = orderModel.Add(orderMType);
            if (result.HasError)
            {
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "OrderMType", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Edit(int id)
        {
            var orderModel = Factory.Get<IOrderMTypeModel>(SystemConst.IOC_Model.OrderMTypeModel);
            var order = orderModel.Get(id);

            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "订单类型管理-修改订单类型", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(OrderMType orderMType)
        {
            var orderModel = Factory.Get<IOrderMTypeModel>(SystemConst.IOC_Model.OrderMTypeModel);
            Result result = orderModel.Edit(orderMType);
            if (result.HasError)
            {
                return JavaScript( AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "OrderMType", new { HostName = LoginAccount.HostName }) + "'");
        }

        public ActionResult Delete(int id)
        {
            var orderModel = Factory.Get<IOrderMTypeModel>(SystemConst.IOC_Model.OrderMTypeModel);
            Result result = orderModel.CompleteDelete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "OrderMType", new { HostName = LoginAccount.HostName }) + "'");
        }
    }
}
