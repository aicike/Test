﻿using System;
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
    public class HouseTypeController : ManageAccountController
    {
        //户型管理
        // GET: /HouseType/
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <param name="id">楼盘ID</param>
        /// <param name="index"></param>
        /// <returns></returns>
        public ActionResult Index(int id,int? index)
        {
            var hounsesTypeModel = Factory.Get<IAccountMainHouseTypeModel>(SystemConst.IOC_Model.AccountMainHouseTypeModel);
            var list = hounsesTypeModel.GetList(id).ToPagedList(index ?? 1, 15);
            ViewBag.HostName = LoginAccount.HostName;
            ViewBag.HID = id;
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var Hounse = hounsesModel.Get(id);
            ViewBag.HostTitle = Hounse.HName;
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
            AccountMainHouseType HounsesType = new AccountMainHouseType();
            HounsesType.AccountMainHousesID = id;

            return View(HounsesType);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="MainHouseInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(AccountMainHouseType MainHouseType, HttpPostedFileBase HousTypeImagePathFile)
        {
            var hounsesTypeModel = Factory.Get<IAccountMainHouseTypeModel>(SystemConst.IOC_Model.AccountMainHouseTypeModel);
            var result = hounsesTypeModel.AddInfo(MainHouseType, LoginAccount.CurrentAccountMainID, HousTypeImagePathFile);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return RedirectToAction("Index", "HouseType", new { id = MainHouseType.AccountMainHousesID });
        }

        /// <summary>
        /// 修改界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id, int hid)
        {
            var hounsesTypeModel = Factory.Get<IAccountMainHouseTypeModel>(SystemConst.IOC_Model.AccountMainHouseTypeModel);
            var HouseInfo = hounsesTypeModel.Get(id);
            var hounsesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var Hounse = hounsesModel.Get(hid);
            ViewBag.HostTitle = Hounse.HName;
            ViewBag.HostName = LoginAccount.HostName;
            return View(HouseInfo);
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="MainHouseInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(AccountMainHouseType MainHouseType, HttpPostedFileBase HousTypeImagePathFile)
        {
            var hounsesTypeModel = Factory.Get<IAccountMainHouseTypeModel>(SystemConst.IOC_Model.AccountMainHouseTypeModel);
            var result = hounsesTypeModel.EditInfo(MainHouseType,LoginAccount.CurrentAccountMainID,HousTypeImagePathFile);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return RedirectToAction("Index", "HouseType", new { id = MainHouseType.AccountMainHousesID });
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id, int Hid)
        {
            var hounsesTypeModel = Factory.Get<IAccountMainHouseTypeModel>(SystemConst.IOC_Model.AccountMainHouseTypeModel);
            var result = hounsesTypeModel.DeleteInfo(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }

            return JavaScript("window.location.href='" + Url.Action("Index", "HouseType", new { HostName = LoginAccount.HostName, id = Hid }) + "'");
        }
    }
}