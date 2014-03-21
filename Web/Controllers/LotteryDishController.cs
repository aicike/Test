using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;
using Newtonsoft.Json;

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
            if (TempData["HasError"] != null)
            {
                ViewBag.HasError = TempData["HasError"].ToString() == "true" ? 1 : 0;
                ViewBag.Msg = TempData["Msg"] == null ? "" : TempData["Msg"].ToString();
            }
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
            var items = Request.Form["hidItems"];
            var list = JsonConvert.DeserializeObject<List<Lottery_dish_detail>>(items);
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            var model = Factory.Get<ILottery_dishModel>(SystemConst.IOC_Model.Lottery_dishModel);
            lottery_dish.AccountMainID = LoginAccount.CurrentAccountMainID;
            var result = model.Add(lottery_dish, list, files);
            if (result.HasError)
            {
                TempData["HasError"] = "true";
                TempData["Msg"] = result.Error;
                return RedirectToAction("Index", "LotteryDish", new { HostName = LoginAccount.HostName });
            }
            else
            {
                TempData["HasError"] = "false";
                TempData["Msg"] = "";
            }
            return RedirectToAction("Index", "LotteryDish", new { HostName = LoginAccount.HostName });
        }

        [AllowCheckPermissions(false)]
        public ActionResult View(int id)
        {
            return View();
        }
    }
}
