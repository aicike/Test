using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Controllers;
using Common;
using System.Data;

namespace Web.Controllers
{
    public class PropertyHouseController : ManageAccountController
    {
        public ActionResult Index(int? id)
        {
            List<PropertyComplexEntity> objs = new List<PropertyComplexEntity>();
            var list = objs.AsQueryable().ToPagedList(id ?? 1, 100);

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
            return View(list);
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
             * UserLoginInfo 用户信息
             * 
             */
            List<Property_House> Property_House_list = new List<Property_House>();
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
                Property_House_list.Add(ph);
            }


            #endregion




            //TempData["HasError"] = 0;
            return RedirectToAction("Index", "PropertyHouse", new { HostName = LoginAccount.HostName });
        }
    }
}
