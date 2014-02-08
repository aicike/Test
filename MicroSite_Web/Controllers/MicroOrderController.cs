using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Controllers;

namespace MicroSite_Web.Controllers
{
    public class MicroOrderController : ManageAccountController
    {
        //
        // GET: /MicroOrder/

        public ActionResult Index(int? id, int? daybyday, string orderNum, string PhoneNum, string Ostatus, string UserName, string Pname)
        {

            List<SelectListItem> Stutas = new List<SelectListItem>();
            Stutas.Add(new SelectListItem { Text = "全部", Value = "all", Selected = true });
            Stutas.Add(new SelectListItem { Text = "待付款", Value = ((int)Poco.Enum.EnumOrderStatus.WaitPayMent).ToString() });
            Stutas.Add(new SelectListItem { Text = "已付款", Value = ((int)Poco.Enum.EnumOrderStatus.Payment).ToString() });
            Stutas.Add(new SelectListItem { Text = "已发货", Value = ((int)Poco.Enum.EnumOrderStatus.Shipped).ToString() });
            Stutas.Add(new SelectListItem { Text = "已完成", Value = ((int)Poco.Enum.EnumOrderStatus.Complete).ToString() });
            Stutas.Add(new SelectListItem { Text = "交易关闭", Value = ((int)Poco.Enum.EnumOrderStatus.Cancel).ToString() });
            ViewData["Stutas"] = Stutas;

            if (string.IsNullOrEmpty(Ostatus))
            {
                Ostatus = "all";
                ViewBag.Ostatus = "all";
            }
            else
            {
                ViewBag.Ostatus = Ostatus;
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
            if (!string.IsNullOrEmpty(UserName))
            {
                ViewBag.UserName = UserName;
            }
            else
            {
                ViewBag.UserName = "";
            }
            if (!string.IsNullOrEmpty(Pname))
            {
                ViewBag.Pname = Pname;
            }
            else
            {
                ViewBag.Pname = "";
            }

            var orderModel = Factory.Get<IOrderModel>(SystemConst.IOC_Model.OrderModel);
            var order = orderModel.Micro_GetList(LoginAccount.CurrentAccountMainID, daybyday.HasValue ? daybyday.Value : 7, orderNum, PhoneNum, Ostatus, UserName, Pname).ToPagedList(id ?? 1, 20); ;

            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "订单管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;


            return View(order);
        }


        /// <summary>
        /// 查看详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowCheckPermissions(false)]
        public ActionResult OrderInfo(int id)
        {
            var orderModel = Factory.Get<IOrderModel>(SystemConst.IOC_Model.OrderModel);
            var order = orderModel.Get(id);

            ViewBag.orderdetail = GetOrderDetail(id).ToList();
            return View(order);
        }

        //或去订单产品信息
        public IQueryable<OrderDetail> GetOrderDetail(int OrderID)
        {
            var orderDetail = Factory.Get<IOrderDetailModel>(SystemConst.IOC_Model.OrderDetailModel);

            return orderDetail.GetOrderDetailByOrderID(OrderID);
        }
    }
}
