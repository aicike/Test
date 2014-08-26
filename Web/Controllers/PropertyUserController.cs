using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interface;
using Poco;
using Injection;
using Controllers;
using Common;
using System.Data;

namespace Web.Controllers
{
    public class PropertyUserController : ManageAccountController
    {
        public ActionResult Index(int id)
        {
            var property_UserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
            var list = property_UserModel.GetHouseByPropertyHouseID(id, LoginAccount.CurrentAccountMainID);

            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "业主信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
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
            ViewBag.ID = id;
            return View(list);
        }

        public ActionResult Add(int id)
        {
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "添加业主信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.HostName = LoginAccount.HostName;
            ViewBag.ID = id;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Property_User property_User,int id)
        {
            var property_UserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
            property_User.AccountMainID = LoginAccount.CurrentAccountMainID;
            property_User.Property_HouseID = id;
            var result = property_UserModel.Add(property_User);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "PropertyUser", new { HostName = LoginAccount.HostName,id=property_User.Property_HouseID }) + "'");
        }

        /// <summary>
        /// 修改界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id,int hid)
        {
            var property_UserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
            var propertyHouse = property_UserModel.Get(id);
            ViewBag.HostName = LoginAccount.HostName;
            string WebTitleRemark = SystemConst.WebTitleRemark;
            string webTitle = string.Format(SystemConst.Business.WebTitle, "修改业主信息", LoginAccount.CurrentAccountMainName, WebTitleRemark);
            ViewBag.Title = webTitle;
            ViewBag.ID = hid;
            return View(propertyHouse);
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="MainHouseInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Property_User property_User,int id)
        {
            var property_UserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
            property_User.AccountMainID = LoginAccount.CurrentAccountMainID;
            property_User.Property_HouseID = id;
            var result = property_UserModel.Edit(property_User);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "PropertyUser", new { HostName = LoginAccount.HostName, id = property_User.Property_HouseID }) + "'");
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id,int hid)
        {
            var property_UserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
            var result = property_UserModel.Delete(id);
            if (result.HasError)
            {
                return Alert(new Dialog(result.Error));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "PropertyUser", new { HostName = LoginAccount.HostName, id = hid }) + "'");
        }

        /// <summary>
        /// 导入用户数据是
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        //public ActionResult Upload(HttpPostedFileBase file,int id)
        //{
        //    var result = Tool.GetXLSXInfo(file);
        //    if (result.HasError)
        //    {
        //        TempData["Msg"] = result.Error;
        //        TempData["HasError"] = 1;
        //    }
        //    #region 检查Excel数据并导入

        //    var data = result.Entity as DataTable;
        //    /* Property_House 房间信息
        //     * Property_User 用户信息
        //     */
        //    List<Property_User_Temp> puList = new List<Property_User_Temp>();
        //    foreach (DataRow item in data.Rows)
        //    {
        //        var houseShortNum = item["房号缩写"];
        //        var name = item["业主姓名"];
        //        var phone = item["业主电话"];
        //        var email = item["邮箱"];
        //        var bz = item["备注"];
        //        if (houseShortNum == null || name == null || phone == null || email == null || bz == null)
        //        {
        //            TempData["Msg"] = "您上传的业主信息表格中信息没有完善，无法导入，请先完善信息。";
        //            TempData["HasError"] = 1;
        //            return RedirectToAction("Index", "PropertyUser", new { HostName = LoginAccount.HostName,id=id });
        //        }
        //        var houseShortNum_str = houseShortNum.ToString().Trim();
        //        var name_str = name.ToString().Trim();
        //        var phone_str = phone.ToString().Trim();
        //        var email_str = email.ToString().Trim();
        //        var bz_str = bz.ToString().Trim();
        //        if (houseShortNum_str.Length <= 0 || name_str.Length <= 0 || phone_str.Length <= 0 || email_str.Length <= 0 || bz_str.Length <= 0)
        //        {
        //            TempData["Msg"] = "您上传的业主信息表格中信息没有完善，无法导入，请先完善信息。";
        //            TempData["HasError"] = 1;
        //            return RedirectToAction("Index", "PropertyUser", new { HostName = LoginAccount.HostName, id = id });
        //        }

        //        Property_User_Temp pu = new Property_User_Temp();
        //        pu.HouseShortNum = houseShortNum_str;
        //        pu.UserName = name_str;
        //        pu.Phone = phone_str;
        //        pu.Email = email_str;
        //        pu.Comment = bz_str;
        //        puList.Add(pu);
        //    }

        //    var property_UserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
        //    result = property_UserModel.AddList(puList);
        //    if (result.HasError)
        //    {
        //        TempData["Msg"] = string.Format("导入失败。[{0}]", result.Error);
        //        TempData["HasError"] = 1;
        //        return RedirectToAction("Index", "PropertyUser", new { HostName = LoginAccount.HostName, id = id });
        //    }
        //    #endregion
        //    TempData["HasError"] = 0;
        //    return RedirectToAction("Index", "PropertyUser", new { HostName = LoginAccount.HostName, id = id });
        //}
    }
}
