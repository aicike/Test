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
            string[] color = new string[] { "#6D7278", "#B55610", "#349933", "#CC3333", "#2C3144", "#B12E3D", "#FFE44C", "#41547F", "#ff0000", "#FFE44C", "#41547F", "#ff0000" };
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
                if (string.IsNullOrEmpty(item.Image) == false)
                {
                    img.AppendFormat("\"{0}\",", Url.Content(item.Image));
                }
                else
                {
                    img.AppendFormat("\"{0}\",", "");
                }
            }
            var img_value = img.Remove(img.Length - 1, 1).ToString();
            ViewBag.Img = img_value;

            #region 中奖率情况

            ////中奖情况计算
            Random r = new Random();
            Lottery_dish_detail detail = ControllerRandomExtract(list, r, 1)[0];
            int index=0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == detail.ID) {
                    index = i;
                    break;
                }
            }
            ViewBag.WinningIndex = index+1;
            ViewBag.Winning = detail;

            #endregion


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

        #region 受控随机抽取

        /// <summary>
        /// 随机抽取
        /// </summary>
        /// <param name="rand">随机数生成器</param>
        /// <param name="Count">随机抽取个数</param>
        /// <returns></returns>
        public List<Lottery_dish_detail> ControllerRandomExtract(List<Lottery_dish_detail> datas, Random rand, int Count = 1)
        {
            List<Lottery_dish_detail> result = new List<Lottery_dish_detail>();
            if (rand != null)
            {
                //临时变量
                Dictionary<Lottery_dish_detail, double> dict = new Dictionary<Lottery_dish_detail, double>(datas.Count);

                //为每个项算一个随机数并乘以相应的权值
                for (int i = datas.Count - 1; i >= 0; i--)
                {
                    dict.Add(datas[i], rand.Next(100) * datas[i].Ratio);
                }

                //排序
                List<KeyValuePair<Lottery_dish_detail, double>> listDict = SortByValue(dict);

                //拷贝抽取权值最大的前Count项
                foreach (KeyValuePair<Lottery_dish_detail, double> kvp in listDict.GetRange(0, Count))
                {
                    result.Add(kvp.Key);
                }
            }
            return result;
        }

        /// <summary>
        /// 排序集合
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        private List<KeyValuePair<Lottery_dish_detail, double>> SortByValue(Dictionary<Lottery_dish_detail, double> dict)
        {
            List<KeyValuePair<Lottery_dish_detail, double>> list = new List<KeyValuePair<Lottery_dish_detail, double>>();

            if (dict != null)
            {
                list.AddRange(dict);

                list.Sort(
                  delegate(KeyValuePair<Lottery_dish_detail, double> kvp1, KeyValuePair<Lottery_dish_detail, double> kvp2)
                  {
                      return (int)(kvp2.Value - kvp1.Value);
                  });
            }
            return list;
        }
        #endregion

    }
}
