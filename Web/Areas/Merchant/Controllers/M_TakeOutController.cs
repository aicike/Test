using Interface.MerchantInterface;
using Injection;
using Poco;
using Poco.MerchantPoco;
using Poco.Enum;
using System.Web.Mvc;
using Controllers;
using System;
using System.Web;

namespace Web.Areas.Merchant.Controllers
{
    public class M_TakeOutController : ManageMerchantController
    {
        public ActionResult Index(int? id)
        {
            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);

            var list = takeOutModel.ListByMerchantID(LoginMerchant.ID).ToPagedList(id ?? 1, 15);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "周边外卖", "", WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.Menu = 1;
            return View(list);
        }

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "周边外卖-添加", "", WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.Menu = 1;
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="MainHouseInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(M_TakeOut M_TakeOut)
        {
            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            M_TakeOut.CreatDate = DateTime.Now;
            M_TakeOut.EnumDataStatus = (int)EnumDataStatus.None;
            M_TakeOut.IsPublish = false;
            M_TakeOut.MerchantID = LoginMerchant.ID;

            string hidCommunity = Request.Form["hidCommunity"];
            if (hidCommunity == null || hidCommunity.Length == 0)
            {
                return JavaScript("isCommit = true;" + AlertJS_NoTag(new Dialog("请选择小区。")));
            }
            var ids= hidCommunity.ConvertToIntArray(',');
            var result = takeOutModel.Add(M_TakeOut, ids);
            if (result.HasError)
            {
                return JavaScript("isCommit = true;" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_TakeOut", new { Area = "Merchant" }) + "'");
        }

        ///// <summary>
        ///// 修改界面
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public ActionResult Edit(int id, int hid)
        //{
        //    var hounsesInfoModel = Factory.Get<IAccountMainHouseInfo>(SystemConst.IOC_Model.AccountMainHouseInfoModel);
        //    var HouseInfo = hounsesInfoModel.Get(id);
        //    var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
        //    var Hounse = hounsesModel.Get(hid);
        //    ViewBag.HostTitle = Hounse.HName;
        //    ViewBag.HostName = LoginAccount.HostName;
        //    string WebTitleRemark = SystemConst.WebTitleRemark;
        //    string webTitle = string.Format(SystemConst.Business.WebTitle, "项目管理-修改单元", LoginAccount.CurrentAccountMainName, WebTitleRemark);
        //    ViewBag.Title = webTitle;
        //    return View(HouseInfo);
        //}

        ///// <summary>
        ///// 修改信息
        ///// </summary>
        ///// <param name="MainHouseInfo"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult Edit(AccountMainHouseInfo MainHouseInfo)
        //{
        //    var hounsesInfoModel = Factory.Get<IAccountMainHouseInfo>(SystemConst.IOC_Model.AccountMainHouseInfoModel);
        //    var result = hounsesInfoModel.Edit(MainHouseInfo);
        //    if (result.HasError)
        //    {
        //        return Alert(new Dialog(result.Error));
        //    }
        //    return JavaScript("window.location.href='" + Url.Action("Index", "HouseInfo", new { HostName = LoginAccount.HostName, houseId = MainHouseInfo.AccountMainHousessID }) + "'");
        //}

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id, int Hid)
        {
            var takeOutModel = Factory.Get<IM_TakeOutModel>(SystemConst.IOC_Model.M_TakeOutModel);
            var result = takeOutModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "M_TakeOut", new { Area = "Merchant" }) + "'");
        }

    }
}
