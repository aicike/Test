using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Controllers;
using Common;
using System.Data;
using Injection;
using Interface;

namespace Web.Controllers
{
    public class PropertyHouseController : ManageAccountController
    {
        public ActionResult Index(int? id, string houseShortNo)
        {
            var property_HouseModel = Factory.Get<IProperty_HouseModel>(SystemConst.IOC_Model.Property_HouseModel);
            var list = property_HouseModel.List(true).Where(a => a.AccountMainID == LoginAccount.CurrentAccountMainID);

            if (!string.IsNullOrEmpty(houseShortNo))
            {
                var tempValue = houseShortNo.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (tempValue.Length == 3)
                {
                    string BuildingNum = tempValue[0];
                    string CellNum = tempValue[1];
                    string RoomNumber = tempValue[2];
                    list = list.Where(a => a.BuildingNum == BuildingNum && a.CellNum == CellNum && a.RoomNumber == RoomNumber);
                }
            }
            var pageList = list.ToPagedList(id ?? 1, 100);
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "房屋信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            //提示消息
            if (TempData["Msg"] != null)
            {
                var msg = TempData["Msg"].ToString();
                ViewBag.Msg = msg;
                ViewBag.HasError = 1;
            }
            if (TempData["HasError"] != null)
            {
                ViewBag.HasError = TempData["HasError"].ToString();
            }
            return View(pageList);
        }

        public ActionResult _Index(int? id, string houseNum)
        {
            var property_HouseModel = Factory.Get<IProperty_HouseModel>(SystemConst.IOC_Model.Property_HouseModel);
            var pageList = property_HouseModel.List(true).Where(a => a.AccountMainID == LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 100);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "房屋信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            //提示消息
            if (TempData["Msg"] != null)
            {
                var msg = TempData["Msg"].ToString();
                ViewBag.Msg = msg;
                ViewBag.HasError = 1;
            }
            if (TempData["HasError"] != null)
            {
                ViewBag.HasError = TempData["HasError"].ToString();
            }
            return View(pageList);
        }

        /// <summary>
        /// 导入用户数据是
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public ActionResult Upload(HttpPostedFileBase file)
        {
            var result = Tool.GetXLSXInfo(file);
            if (result.HasError)
            {
                TempData["Msg"] = result.Error;
                TempData["HasError"] = 1;
            }
            #region 检查Excel数据并导入

            var data = result.Entity as DataTable;
            /* Property_House 房间信息
             * Property_User 用户信息
             */
            List<Property_House> phlist = new List<Property_House>();
            foreach (DataRow item in data.Rows)
            {
                var buildingNum = item["楼号"];
                var cellNum = item["单元"];
                var houseNum = item["房号"];
                if (buildingNum == null || cellNum == null || houseNum == null)
                {
                    TempData["Msg"] = "您上传的房屋信息表格中信息没有完善，无法导入，请先完善信息。";
                    TempData["HasError"] = 1;
                    return RedirectToAction("Index", "PropertyHouse", new { HostName = LoginAccount.HostName });
                }
                var buildingNum_str = buildingNum.ToString().Trim();
                var cellNum_str = cellNum.ToString().Trim();
                var houseNum_str = houseNum.ToString().Trim();
                if (buildingNum_str.Length <= 0 || cellNum_str.Length <= 0 || houseNum_str.Length <= 0)
                {
                    TempData["Msg"] = "您上传的房屋信息表格中信息没有完善，无法导入，请先完善信息。";
                    TempData["HasError"] = 1;
                    return RedirectToAction("Index", "PropertyHouse", new { HostName = LoginAccount.HostName });
                }
                Property_House ph = new Property_House();
                ph.BuildingNum = buildingNum_str;
                ph.CellNum = cellNum_str;
                ph.RoomNumber = houseNum_str;
                ph.AccountMainID = LoginAccount.CurrentAccountMainID;
                phlist.Add(ph);
                //Property_User pu = new Property_User();
                //pu.AccountMainID = LoginAccount.CurrentAccountMainID;
                //pu.UserName = userName_str;
                //pu.Phone = userPhone_str;
                //pu.Property_House = ph;
                //Property_User_list.Add(pu);
            }

            var propertyHouseModel = Factory.Get<IProperty_HouseModel>(SystemConst.IOC_Model.Property_HouseModel);
            result = propertyHouseModel.AddList(phlist);
            if (result.HasError)
            {
                TempData["Msg"] = string.Format("导入失败。[{0}]", result.Error);
                TempData["HasError"] = 1;
                return RedirectToAction("Index", "PropertyHouse", new { HostName = LoginAccount.HostName });
            }
            #endregion
            TempData["HasError"] = 0;
            return RedirectToAction("Index", "PropertyHouse", new { HostName = LoginAccount.HostName });
        }

        public ActionResult Add()
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "添加房屋信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.HostName = LoginAccount.HostName;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Property_House property_House)
        {
            var property_HouseModel = Factory.Get<IProperty_HouseModel>(SystemConst.IOC_Model.Property_HouseModel);
            //property_User.AccountMainID = LoginAccount.CurrentAccountMainID;
            //property_User.Property_House.AccountMainID = LoginAccount.CurrentAccountMainID;
            //var result = propertyUserModel.Add(property_User);
            property_House.AccountMainID = LoginAccount.CurrentAccountMainID;
            var result = property_HouseModel.Add(property_House);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "PropertyHouse", new { HostName = LoginAccount.HostName }) + "'");
        }


        /// <summary>
        /// 修改界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var property_HouseModel = Factory.Get<IProperty_HouseModel>(SystemConst.IOC_Model.Property_HouseModel);
            var propertyHouse = property_HouseModel.Get(id);
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "修改房屋信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            return View(propertyHouse);
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="MainHouseInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Property_House property_House)
        {
            var property_HouseModel = Factory.Get<IProperty_HouseModel>(SystemConst.IOC_Model.Property_HouseModel);
            property_House.AccountMainID = LoginAccount.CurrentAccountMainID;
            var result = property_HouseModel.Edit(property_House);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "PropertyHouse", new { HostName = LoginAccount.HostName }) + "'");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var property_HouseModel = Factory.Get<IProperty_HouseModel>(SystemConst.IOC_Model.Property_HouseModel);
            var result = property_HouseModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "PropertyHouse", new { HostName = LoginAccount.HostName }) + "'");
        }
    }
}
