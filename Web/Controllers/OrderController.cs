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
    public class OrderController : ManageAccountController
    {
        //
        // GET: /Order/

        public ActionResult Index(int? id, int? daybyday, string orderNum, string PhoneNum, string status)
        {


            List<SelectListItem> Stutas = new List<SelectListItem>();
            Stutas.Add(new SelectListItem { Text = "全部", Value = "all", Selected = true });
            Stutas.Add(new SelectListItem { Text = "进行中", Value = ((int)Poco.Enum.EnumOrderStatus.Proceed).ToString() });
            Stutas.Add(new SelectListItem { Text = "已完成", Value = ((int)Poco.Enum.EnumOrderStatus.Complete).ToString() });
            Stutas.Add(new SelectListItem { Text = "取消", Value = ((int)Poco.Enum.EnumOrderStatus.Cancel).ToString() });
            Stutas.Add(new SelectListItem { Text = "撤销", Value = ((int)Poco.Enum.EnumOrderStatus.Revoke).ToString() });
            ViewData["Stutas"] = Stutas;

            if (string.IsNullOrEmpty(status))
            {
                status = "all";
                ViewBag.Status = "all";
            }
            else
            {
                ViewBag.Status = status;
            }
            if (daybyday.HasValue)
            {
                ViewBag.daybyday = daybyday.Value;
            }
            else
            {
                ViewBag.daybyday = "7";
            }
            if (!string.IsNullOrEmpty(orderNum))
            {
                ViewBag.orderNum = orderNum;
            }
            else
            {
                ViewBag.orderNum = "";
            }
            if (!string.IsNullOrEmpty(PhoneNum))
            {
                ViewBag.PhoneNum = PhoneNum;
            }
            else
            {
                ViewBag.PhoneNum = "";
            }



            var orderModel = Factory.Get<IOrderModel>(SystemConst.IOC_Model.OrderModel);
            var list = orderModel.GetList(LoginAccount.CurrentAccountMainID, daybyday.HasValue ? daybyday.Value : 7, orderNum, PhoneNum, status).ToPagedList(id ?? 1, 15);


            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "订单管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(list);
        }


        public ActionResult Cancel(int id)
        {
            var orderModel = Factory.Get<IOrderModel>(SystemConst.IOC_Model.OrderModel);
            Result result = orderModel.SetOrderStatus(id, (int)Poco.Enum.EnumOrderStatus.Revoke);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Order", new { HostName = LoginAccount.HostName }) + "'");
        }


        [AllowCheckPermissions(false)]
        public ActionResult OrderInfo(int id)
        {
            var orderModel = Factory.Get<IOrderModel>(SystemConst.IOC_Model.OrderModel);
            var order = orderModel.Get(id);
            ViewBag.Oname = getOrderUserName(order.OrderUserID,order.OrderUserType);

            var mint = GetMintermediate(id);
            ViewBag.TypeCount = mint.MTypeCount;

            ViewBag.orderdetail = GetOrderDetail(id).ToList();
            return View(order);
        }



        public OrderMIntermediate GetMintermediate(int OrderID)
        {
            var orderMinterModel = Factory.Get<IOrderMIntermediateModel>(SystemConst.IOC_Model.OrderMIntermediateModel);
            var minter = orderMinterModel.GetMintByOrderID(OrderID);
            return minter;
        }


        public string getOrderUserName(int userid, int userType)
        {
            string name = "";
            if (userType == (int)Poco.Enum.EnumClientUserType.Account)
            {
                var getuser = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
                name = getuser.Get(userid).Name;
            }


            return name;

        }


        public IQueryable<OrderDetail> GetOrderDetail(int OrderID)
        {
            var orderDetail = Factory.Get<IOrderDetailModel>(SystemConst.IOC_Model.OrderDetailModel);

            return orderDetail.GetOrderDetailByOrderID(OrderID);
        }
    }
}
