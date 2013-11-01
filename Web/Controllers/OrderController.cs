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
            Stutas.Add(new SelectListItem { Text = "撤销", Value = ((int)Poco.Enum.EnumOrderStatus.Revoke).ToString()  });
            ViewData["Stutas"] = Stutas;

            if (string.IsNullOrEmpty(status))
            {
                status = "all";
            }

            var orderModel = Factory.Get<IOrderModel>(SystemConst.IOC_Model.OrderModel);
            var list = orderModel.GetList(LoginAccount.CurrentAccountMainID, daybyday.HasValue ? daybyday.Value : 7, orderNum, PhoneNum, status).ToPagedList(id ?? 1, 15);


            ViewBag.HostName = LoginAccount.HostName;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "订单管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;

            return View(list);
        }

        
    }
}
