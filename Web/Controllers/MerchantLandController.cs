using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using Injection;
using Poco;
using Interface;
using Common;

namespace Web.Controllers
{
    /// <summary>
    /// 商户
    /// </summary>
    public class MerchantLandController : BaseController, System.Web.SessionState.IRequiresSessionState
    {
        /// <summary>
        /// 商户登陆界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string BackUrl)
        {
            //读取展示图片
            DataTable dt = new DataTable();
            dt.Columns.Add("path");
            string loginPageShow = HttpContext.Server.MapPath("~/File/LoginPageShow/Merchant");
            DirectoryInfo TheFolder = new DirectoryInfo(loginPageShow);
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                DataRow row = dt.NewRow();
                row["path"] = "~/File/LoginPageShow/Merchant/" + NextFile.Name;
                dt.Rows.Add(row);
            }
            if (dt.Rows.Count <= 0)
            {
                DataRow row = dt.NewRow();
                row["path"] = "~/File/LoginPageShow/Default/Default.jpg";
                dt.Rows.Add(row);
            }
            ViewBag.ShowPage = dt;

            ViewBag.BackUrl = BackUrl;
            return View();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MerchantLogin(string phone_email, string password)
        {
            var merchantModel = Factory.Get<IMerchantModel>(SystemConst.IOC_Model.MerchantModel);
            var result = merchantModel.Login(phone_email, password);
            if (result.HasError)
            {
                return JavaScript("LandWaitFor('login','WaitImg',2);" + AlertJS_NoTag(new Dialog(result.Error)));
            }
            var url = Url.RouteUrl("Default", new { action = "Index", controller = "SystemUserHome" });
            return JavaScript("window.location.href='" + url + "'");
        }

        /// <summary>
        /// 商户注册界面
        /// </summary>
        /// Iserror  0 正常 1错误
        /// <returns></returns>
        public ActionResult Register(int? Iserror,string Error)
        {
            if (Iserror.HasValue)
            {
                ViewBag.IsError = Iserror.Value;
                ViewBag.Error = Error;
            }
            else
            {
                ViewBag.IsError = 0;
            }

            return View();
        }
        /// <summary>
        /// 商户注册界面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(Merchant merchant)
        {
            merchant.LogoImagePath = "~/Images/logo.png";
            merchant.LogoShow = "~/Images/logo.png";
            merchant.LoginPwd = DESEncrypt.Encrypt(merchant.LoginPwd);
            merchant.LoginPwdPage = DESEncrypt.Encrypt(merchant.LoginPwdPage);
            
            var merchantModel = Factory.Get<IMerchantModel>(SystemConst.IOC_Model.MerchantModel);
            var result = merchantModel.Add(merchant);
            if (result.HasError)
            {
                return RedirectToAction("Register", "MerchantLand", new { Iserror = 1, Error = result.Error });
            }

            Merchant entity = new Merchant();
            entity.ID = merchant.ID;
            entity.SystemStatus = merchant.SystemStatus;
            entity.Name = merchant.Name;
            entity.Email = merchant.Email;
            entity.LoginPwd = merchant.LoginPwd;
            entity.LogoImagePath = merchant.LogoImagePath;
            entity.Phone = merchant.Phone;
            entity.Address = merchant.Address;
            Session[SystemConst.Session.LoginMerchant] = entity;
            return RedirectToAction("Index", "MerchantHome");
        }

    }
}
