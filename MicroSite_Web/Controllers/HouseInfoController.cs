using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Controllers;

namespace Web.Controllers
{
    public class HouseInfoController : ManageAccountController
    {
        //单元管理
        // GET: /HouseInfo/

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="id"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public ActionResult Index(int? id,int houseId)
        {
            var hounsesInfoModel = Factory.Get<IAccountMainHouseInfo>(SystemConst.IOC_Model.AccountMainHouseInfoModel);
            var list = hounsesInfoModel.GetListByAccountMainHouseID(houseId).ToPagedList(id ?? 1, 15);
            ViewBag.HostName = LoginAccount.HostName;
            ViewBag.HID = houseId;
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var Hounse = hounsesModel.Get(houseId);
            ViewBag.HostTitle = Hounse.HName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "项目管理-单元管理", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(list);
        }

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add(int id)
        {
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var Hounse = hounsesModel.Get(id);
            ViewBag.HostTitle = Hounse.HName;
            ViewBag.HostName = LoginAccount.HostName;
            AccountMainHouseInfo HounsesInfo = new AccountMainHouseInfo();
            HounsesInfo.AccountMainHousessID = id;

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "项目管理-添加单元", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(HounsesInfo);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="MainHouseInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(AccountMainHouseInfo MainHouseInfo)
        {
            var hounsesInfoModel = Factory.Get<IAccountMainHouseInfo>(SystemConst.IOC_Model.AccountMainHouseInfoModel);
            var result = hounsesInfoModel.Add(MainHouseInfo);
            if (result.HasError)
            {
                return JavaScript("isCommit = true;" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "HouseInfo", new { HostName = LoginAccount.HostName, houseId = MainHouseInfo.AccountMainHousessID }) + "'");
        }

        /// <summary>
        /// 修改界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id,int hid)
        {
            var hounsesInfoModel = Factory.Get<IAccountMainHouseInfo>(SystemConst.IOC_Model.AccountMainHouseInfoModel);
            var HouseInfo = hounsesInfoModel.Get(id);
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var Hounse = hounsesModel.Get(hid);
            ViewBag.HostTitle = Hounse.HName;
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "项目管理-修改单元", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(HouseInfo);
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="MainHouseInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(AccountMainHouseInfo MainHouseInfo)
        {
            var hounsesInfoModel = Factory.Get<IAccountMainHouseInfo>(SystemConst.IOC_Model.AccountMainHouseInfoModel);
            var result = hounsesInfoModel.Edit(MainHouseInfo);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "HouseInfo", new { HostName = LoginAccount.HostName, houseId = MainHouseInfo.AccountMainHousessID }) + "'");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id,int Hid)
        {
            var hounsesInfoModel = Factory.Get<IAccountMainHouseInfo>(SystemConst.IOC_Model.AccountMainHouseInfoModel);
            var result = hounsesInfoModel.DelteAll(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "HouseInfo", new { HostName = LoginAccount.HostName, houseId = Hid }) + "'");
        }

    }
}
