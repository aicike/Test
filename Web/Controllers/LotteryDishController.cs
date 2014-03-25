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
using System.Text;

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
            var model = Factory.Get<ILottery_dishModel>(SystemConst.IOC_Model.Lottery_dishModel);
            var entity = model.Get(id);
            var list = entity.Lottery_dish_details.ToList();
            //名称
            StringBuilder names = new StringBuilder();
            foreach (var item in list)
            {
                names.AppendFormat("\"{0}\",", item.Name);
            }
            var name_value = names.Remove(names.Length - 1, 1).ToString();
            ViewBag.Name = name_value;
            //颜色
            string[] color = new string[] { "#6D7278", "#B55610", "#349933", "#CC3333", "#2C3144", "#B12E3D", "#FFE44C", "#41547F", "#ff0000" };
            StringBuilder colors = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                colors.AppendFormat("\"{0}\",", color[i]);
            }
            var color_value = colors.Remove(colors.Length - 1, 1).ToString();
            ViewBag.Color = color_value;
            //图片
            StringBuilder img = new StringBuilder();
            foreach (var item in list)
            {
                img.AppendFormat("\"{0}\",", Url.Content(item.Image));
            }
            var img_value = img.Remove(img.Length - 1, 1).ToString();
            ViewBag.Img = img_value;
            return View(entity);
        }

        public ActionResult Edit(int id)
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "大转盘", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.HostName = LoginAccount.HostName;
            var model = Factory.Get<ILottery_dishModel>(SystemConst.IOC_Model.Lottery_dishModel);
            var entity = model.Get(id);
            return View(entity);
        }

        [HttpPost]
        public ActionResult Edit(Lottery_dish lottery_dish)
        {
            var items = Request.Form["hidItems"];
            var list = JsonConvert.DeserializeObject<List<Lottery_dish_detail>>(items);
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            var model = Factory.Get<ILottery_dishModel>(SystemConst.IOC_Model.Lottery_dishModel);
            lottery_dish.AccountMainID = LoginAccount.CurrentAccountMainID;
            var result = model.Edit(lottery_dish, list, files);
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
    }
}
