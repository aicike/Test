using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Interface;
using Injection;
using Business;
using System.IO;

namespace Web.Controllers
{
    public class AjaxController : BaseController, IController
    {
        public ActionResult ProvinceSelectList()
        {
            IProvinceModel provinceModel = Factory.Get<IProvinceModel>(SystemConst.IOC_Model.ProvinceModel);
            return Json(provinceModel.GetProvinceList().Select(a => new { a.ID, a.Name }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CitySelectList(int provinceID)
        {
            ICityModel cityModel = Factory.Get<ICityModel>(SystemConst.IOC_Model.CityModel);
            return Json(cityModel.GetCityList(provinceID).Select(a => new { a.ID, a.Name }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DistrictSelectList(int cityID)
        {
            IDistrictModel districtModel = Factory.Get<IDistrictModel>(SystemConst.IOC_Model.DistrictModel);
            return Json(districtModel.GetDistrictList(cityID).Select(a => new { a.ID, a.Name }), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckIsUniqueAccountMain()
        {
            if (IsCheck("/System/AccountMainManage/AddAccountMain"))
            {
                var name = Request.QueryString[0];
                CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                var result = model.CheckIsUnique("AccountMain", "Name", name);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckIsUniqueAccountEmail()
        {
            if (IsCheck("/System/SystemUserHome/AddAccount"))
            {
                string email = Request.QueryString[0];
                CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                var result = model.CheckIsUnique("Account", "Email", email);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckIsUniqueAccountMain_HostName()
        {
            if (IsCheck("/System/AccountMainManage/AddAccountMain", "/System/AccountMainManage/EditAccountMain"))
            {
                string hostName = Request.QueryString[0];
                CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                var result = model.CheckIsUnique("AccountMain", "HostName", hostName);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckHousesRepeat()
        {
            if (Request.UrlReferrer.LocalPath.IndexOf("HousesMange/Add/")>=0)
            {
                string HName = Request.QueryString[0];
                CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                var result = model.CheckIsUnique("AccountMainHouses", "HName", HName);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private bool IsCheck(params string[] path)
        {
            string url = Request.UrlReferrer.LocalPath;
            return path.Contains(url);
        }
    }
}
