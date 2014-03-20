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
    public class LotteryDishController : ManageAccountController
    {
        public ActionResult Index(int? id)
        {
            var model = Factory.Get<ILottery_dishModel>(SystemConst.IOC_Model.Lottery_dishModel);
            var list = model.List(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "大转盘", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }

        public ActionResult Add()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "大转盘", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.HostName = LoginAccount.HostName;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Lottery_dish lottery_dish)
        {
            var model = Factory.Get<ILottery_dishModel>(SystemConst.IOC_Model.Lottery_dishModel);
            lottery_dish.AccountMainID = LoginAccount.CurrentAccountMainID;
            var result = model.Add(lottery_dish);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "LotteryDish", new { HostName = LoginAccount.HostName}) + "'");
        }
    }
}
