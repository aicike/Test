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
        public ActionResult Index(int? id, string houseNum, string userName, string userPhone)
        {
            var propertyUserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);

            var list = propertyUserModel.GetListByAccountMainID(LoginAccount.CurrentAccountMainID);//.ToPagedList(id ?? 1, 100);
            if (string.IsNullOrEmpty(houseNum) == false && houseNum.Length > 0)
            {
                list = list.Where(a => a.Property_House.RoomNumber.Contains(houseNum));
            }
            if (string.IsNullOrEmpty(userName) == false && userName.Length > 0)
            {
                list = list.Where(a => a.UserName.Contains(userName));
            }
            if (string.IsNullOrEmpty(userPhone) == false && userPhone.Length > 0)
            {
                list = list.Where(a => a.Phone.Contains(userPhone));
            }
            var pageList= list.ToPagedList(id ?? 1, 100);
            //List<PropertyComplexEntity> objs = new List<PropertyComplexEntity>();
            //var list = objs.AsQueryable().ToPagedList(id ?? 1, 100);

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
             * 
             */
            List<Property_User> Property_User_list = new List<Property_User>();
            foreach (DataRow item in data.Rows)
            {
                var buildingNum = item["楼号"];
                var cellNum = item["单元"];
                var houseNum = item["房号"];
                var userName = item["业主姓名"];
                var userPhone = item["业主电话"];
                if (buildingNum == null || cellNum == null || houseNum == null || userName == null || userPhone == null)
                {
                    TempData["Msg"] = "您上传的物业用户信息表格中信息没有完善，无法导入，请先完善信息。";
                    TempData["HasError"] = 1;
                    return RedirectToAction("Index", "PropertyHouse", new { HostName = LoginAccount.HostName });
                }
                var buildingNum_str = buildingNum.ToString().Trim();
                var cellNum_str = cellNum.ToString().Trim();
                var houseNum_str = houseNum.ToString().Trim();
                var userName_str = userName.ToString().Trim();
                var userPhone_str = userPhone.ToString().Trim();
                if (buildingNum_str.Length <= 0 || cellNum_str.Length <= 0 || houseNum_str.Length <= 0 || userName_str.Length <= 0 || userPhone_str.Length <= 0)
                {
                    TempData["Msg"] = "您上传的物业用户信息表格中信息没有完善，无法导入，请先完善信息。";
                    TempData["HasError"] = 1;
                    return RedirectToAction("Index", "PropertyHouse", new { HostName = LoginAccount.HostName });
                }
                Property_House ph = new Property_House();
                ph.BuildingNum = buildingNum_str;
                ph.CellNum = cellNum_str;
                ph.RoomNumber = houseNum_str;
                ph.AccountMainID = LoginAccount.CurrentAccountMainID;
                Property_User pu = new Property_User();
                pu.AccountMainID = LoginAccount.CurrentAccountMainID;
                pu.UserName = userName_str;
                pu.Phone = userPhone_str;
                pu.Property_House = ph;
                Property_User_list.Add(pu);
            }
            var propertyUserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
            result = propertyUserModel.AddList(Property_User_list);
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
    }
}
