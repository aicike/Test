﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Interface;
using Injection;
using Business;
using System.IO;
using Common;
using System.Drawing;

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
            if (IsCheck("/System/AccountManage/AddAccount", "/Account/Add"))
            {
                string email = Request.QueryString[0];
                CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                var result = model.CheckIsUnique("Account", "Email", email);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CheckIsUniqueSystemUserEmail()
        {
            if (IsCheck("/System/SystemUser/Add"))
            {
                string email = Request.QueryString[0];
                CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                var result = model.CheckIsUnique("SystemUser", "Email", email);
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
            if (Request.UrlReferrer.LocalPath.IndexOf("HousesMange/Add/") >= 0)
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
            bool v = false;
            foreach (var item in path)
            {
                v = url.Contains(item);
                if (v == true)
                {
                    break;
                }
            }
            return v;
        }

        /// <summary>
        /// 唯一验证
        /// </summary>
        /// <param name="AccountMainID">售楼部ID</param>
        /// <param name="tableName">表名字</param>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <param name="id">id</param>
        /// <returns> false 代表已存在</returns>
        [HttpPost]
        public string OnlyValidationAccount(string AccountMainID, string tableName, string field, string value, int? id = null)
        {
            CommonModel cm = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            if (id.HasValue)
            {
                return cm.CheckIsUniqueAccount(AccountMainID, tableName, field, value, id).ToString();
            }
            else
            {
                return cm.CheckIsUniqueAccount(AccountMainID, tableName, field, value).ToString();
            }
        }

        /// <summary>
        /// 唯一验证
        /// </summary>
        /// <param name="tableName">表名字</param>
        /// <param name="field">字段名</param>
        /// <param name="value">值</param>
        /// <param name="id">id</param>
        /// <returns> false 代表已存在</returns>
        [HttpPost]
        public string OnlyValidation( string tableName, string field, string value, int? id = null)
        {
            CommonModel cm = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            if (id.HasValue)
            {
                return cm.CheckIsUnique( tableName, field, value, id).ToString();
            }
            else
            {
                return cm.CheckIsUnique( tableName, field, value).ToString();
            }
        }

        /// <summary>
        /// 上传文件到临时文件夹中
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string UpImg()
        {
            if (Request.Files.Count > 0)
            {
                string Path = Tool.GetTemporaryPath();
                CommonModel com = new CommonModel();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var LastName = token + com.CreateRandom("", 5) + Request.Files[0].FileName.GetFileSuffix();
                //图片显示界面
                var ImagePath = Path + "\\" + LastName;
                var mapePath = Server.MapPath(Path) + "\\" + LastName;
                int dataLengthToRead = (int)Request.Files[0].InputStream.Length;//获取下载的文件总大小
                byte[] buffer = new byte[dataLengthToRead];

                int r = Request.Files[0].InputStream.Read(buffer, 0, dataLengthToRead);//本次实际读取到字节的个数
                Stream tream = new MemoryStream(buffer);
                Image img = Image.FromStream(tream);

                Tool.SuperGetPicThumbnail(img, mapePath, 70, 1280, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);

                return Url.Content(ImagePath);

            }
            else
            {
                return "false";
            }
        }
    }
}
