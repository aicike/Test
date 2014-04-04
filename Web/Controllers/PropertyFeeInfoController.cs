using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Injection;
using Interface;
using Poco;

namespace Web.Controllers
{
    public class PropertyFeeInfoController : ManageAccountController
    {
        //
        // GET: /PropertyFeeInfo/
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="id">分页</param>
        /// <param name="Date">缴费月份</param>
        /// <param name="Unit">单元</param>
        /// <param name="RoomNumber">房号</param>
        /// <param name="OwnerName">业主姓名</param>
        /// <param name="OwnerPhone">业主电话</param>
        /// <returns></returns>
        public ActionResult Index(int? id, string Date, string Unit, string RoomNumber, string OwnerName, string OwnerPhone)
        {
            
            if (!string.IsNullOrEmpty(Date))
            {
                ViewBag.Date = Date;
            }
            else
            {
                ViewBag.Date = "";
            }
            if (!string.IsNullOrEmpty(Unit))
            {
                ViewBag.Unit = Unit;
            }
            else
            {
                ViewBag.Unit = "";
            }
            if (!string.IsNullOrEmpty(RoomNumber))
            {
                ViewBag.RoomNumber = RoomNumber;
            }
            else
            {
                ViewBag.RoomNumber = "";
            }
            if (!string.IsNullOrEmpty(OwnerName))
            {
                ViewBag.OwnerName = OwnerName;
            }
            else
            {
                ViewBag.OwnerName = "";
            }
            if (!string.IsNullOrEmpty(OwnerPhone))
            {
                ViewBag.OwnerPhone = OwnerPhone;
            }
            else
            {
                ViewBag.OwnerPhone = "";
            }
            var propertyfeemodel = Factory.Get<IPropertyFeeInfoModel>(SystemConst.IOC_Model.PropertyFeeInfoModel);
            var propertyfee = propertyfeemodel.GetPropertyFeeInfo(LoginAccount.CurrentAccountMainID, Date, Unit, RoomNumber, OwnerName, OwnerPhone).ToPagedList(id ?? 1, 50);

            return View(propertyfee);
        }

    }
}
