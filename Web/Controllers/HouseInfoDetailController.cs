using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Controllers;
using Business;
using Poco.Enum;

namespace Web.Controllers
{
    public class HouseInfoDetailController : ManageAccountController
    {
        //房屋管理
        // GET: /HouseInfoDetail/
        /// <summary>
        /// 列表页
        /// </summary>
        /// <param name="Hid">楼盘ID</param>
        /// <param name="Did">单元ID</param>
        /// <param name="index"></param>
        /// <returns></returns>
        public ActionResult Index(int Hid,int Did, int? index)
        {
            //房屋数据
            var hounsesInfoDetailModel = Factory.Get<IAccountMainHouseInfoDetailModel>(SystemConst.IOC_Model.AccountMainHouseInfoDetailModel);
            var list = hounsesInfoDetailModel.GetList(Did).ToPagedList(index ?? 1, 15);
            ViewBag.HostName = LoginAccount.HostName;
            ViewBag.HID = Hid;
            ViewBag.DID = Did;
            //楼盘数据
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var Hounse = hounsesModel.Get(Hid);
            ViewBag.HostHTitle = Hounse.HName;
            //单元数据
            var hounsesInfoModel = Factory.Get<IAccountMainHouseInfo>(SystemConst.IOC_Model.AccountMainHouseInfoModel);
            var HounseInfo = hounsesInfoModel.Get(Did);
            ViewBag.HostDTitle = HounseInfo.Building + " " + HounseInfo.Cell;
            return View(list);
        }

        /// <summary>
        /// 添加页面
        /// </summary>
        /// <param name="id">单元ID</param>
        /// <param name="Hid">楼盘ID</param>
        /// <returns></returns>
        public ActionResult Add(int id,int Hid)
        {
            ViewBag.HID = Hid;
            ViewBag.DID = id;
            //楼盘数据
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var Hounse = hounsesModel.Get(Hid);
            ViewBag.HostHTitle = Hounse.HName;
            //单元数据
            var hounsesInfoModel = Factory.Get<IAccountMainHouseInfo>(SystemConst.IOC_Model.AccountMainHouseInfoModel);
            var HounseInfo = hounsesInfoModel.Get(id);
            ViewBag.HostDTitle = HounseInfo.Building + " " + HounseInfo.Cell;
            //地址前缀
            ViewBag.HostName = LoginAccount.HostName;
        
            //户型
            var hounsesTypeModel = Factory.Get<IAccountMainHouseTypeModel>(SystemConst.IOC_Model.AccountMainHouseTypeModel);
            var HousesType = hounsesTypeModel.GetList(Hid);
            var selectList = new SelectList(HousesType, "ID", "HName");
            List<SelectListItem> newHtypeList = new List<SelectListItem>();
            newHtypeList.Add(new SelectListItem { Text = "请选择", Value = "0", Selected = true });
            newHtypeList.AddRange(selectList);
            ViewData["HousesTypeS"] = newHtypeList;
            //售出状态
            var LookupOption = LookupFactory.GetLookupOptionList(typeof(EnumSoldState));
            var selectListSt = new SelectList(LookupOption, "ID", "Value");
            List<SelectListItem> newStatusList = new List<SelectListItem>();
            //newHtypeList.Add(new SelectListItem { Text = "请选择", Value = "select", Selected = true });
            newStatusList.AddRange(selectListSt);
            ViewData["HousesStatus"] = newStatusList;

            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="MainHouseInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(AccountMainHouseInfoDetail MainHouseInfoDetail,int Hid,int Did)
        {
            var hounsesInfoDetailModel = Factory.Get<IAccountMainHouseInfoDetailModel>(SystemConst.IOC_Model.AccountMainHouseInfoDetailModel);
            MainHouseInfoDetail.AccountMainHouseInfoID = Did;
            var result = hounsesInfoDetailModel.Add(MainHouseInfoDetail);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "HouseInfoDetail", new { HostName = LoginAccount.HostName, Hid = Hid, Did = Did }) + "'");
        }

        /// <summary>
        /// 修改页面
        /// </summary>
        /// <param name="id">房屋ID</param>
        /// <param name="Did">单元ID</param>
        /// <param name="Hid">楼盘ID</param>
        /// <returns></returns>
        public ActionResult Edit(int id,int Did, int Hid)
        {
            ViewBag.HID = Hid;
            ViewBag.DID = Did;
            //楼盘数据
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var Hounse = hounsesModel.Get(Hid);
            ViewBag.HostHTitle = Hounse.HName;
            //单元数据
            var hounsesInfoModel = Factory.Get<IAccountMainHouseInfo>(SystemConst.IOC_Model.AccountMainHouseInfoModel);
            var HounseInfo = hounsesInfoModel.Get(Did);
            ViewBag.HostDTitle = HounseInfo.Building + " " + HounseInfo.Cell;
            //地址前缀
            ViewBag.HostName = LoginAccount.HostName;

            //户型
            var hounsesTypeModel = Factory.Get<IAccountMainHouseTypeModel>(SystemConst.IOC_Model.AccountMainHouseTypeModel);
            var HousesType = hounsesTypeModel.GetList(Hid);
            var selectList = new SelectList(HousesType, "ID", "HName");
            List<SelectListItem> newHtypeList = new List<SelectListItem>();
            newHtypeList.Add(new SelectListItem { Text = "请选择", Value = "0", Selected = true });
            newHtypeList.AddRange(selectList);
            ViewData["HousesTypeS"] = newHtypeList;
            //售出状态
            var LookupOption = LookupFactory.GetLookupOptionList(typeof(EnumSoldState));
            var selectListSt = new SelectList(LookupOption, "ID", "Value");
            List<SelectListItem> newStatusList = new List<SelectListItem>();
            //newHtypeList.Add(new SelectListItem { Text = "请选择", Value = "select", Selected = true });
            newStatusList.AddRange(selectListSt);
            ViewData["HousesStatus"] = newStatusList;
            //房屋数据
            var hounsesInfoDetailModel = Factory.Get<IAccountMainHouseInfoDetailModel>(SystemConst.IOC_Model.AccountMainHouseInfoDetailModel);
            var HouseInfoDetail = hounsesInfoDetailModel.Get(id);
            return View(HouseInfoDetail);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="MainHouseInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(AccountMainHouseInfoDetail MainHouseInfoDetail, int Did, int Hid)
        {
            var hounsesInfoDetailModel = Factory.Get<IAccountMainHouseInfoDetailModel>(SystemConst.IOC_Model.AccountMainHouseInfoDetailModel);
            MainHouseInfoDetail.AccountMainHouseInfoID = Did;
            var result = hounsesInfoDetailModel.Edit(MainHouseInfoDetail);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "HouseInfoDetail", new { HostName = LoginAccount.HostName, Hid = Hid, Did = Did }) + "'");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id,int Did, int Hid)
        {
            var hounsesInfoDetailModel = Factory.Get<IAccountMainHouseInfoDetailModel>(SystemConst.IOC_Model.AccountMainHouseInfoDetailModel);
            var result = hounsesInfoDetailModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "HouseInfoDetail", new { HostName = LoginAccount.HostName, Hid = Hid, Did = Did }) + "'");
        }
        /// <summary>
        /// 详细信息页
        /// </summary>
        /// <param name="id">房屋ID</param>
        /// <param name="Did">单元ID</param>
        /// <param name="Hid">楼盘ID</param>
        /// <returns></returns>
        public ActionResult Select(int id, int Hid, int Did)
        {
            ViewBag.HID = Hid;
            ViewBag.DID = Did;
            //楼盘数据
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var Hounse = hounsesModel.Get(Hid);
            ViewBag.HostHTitle = Hounse.HName;
            //单元数据
            var hounsesInfoModel = Factory.Get<IAccountMainHouseInfo>(SystemConst.IOC_Model.AccountMainHouseInfoModel);
            var HounseInfo = hounsesInfoModel.Get(Did);
            ViewBag.HostDTitle = HounseInfo.Building + " " + HounseInfo.Cell;
            ViewBag.HostDs = HounseInfo.Building;
            ViewBag.HostDYs = HounseInfo.Cell;
            //地址前缀
            ViewBag.HostName = LoginAccount.HostName;
            //房屋数据
            var hounsesInfoDetailModel = Factory.Get<IAccountMainHouseInfoDetailModel>(SystemConst.IOC_Model.AccountMainHouseInfoDetailModel);
            var HouseInfoDetail = hounsesInfoDetailModel.Get(id);
            return View(HouseInfoDetail);
        }
    }
}
