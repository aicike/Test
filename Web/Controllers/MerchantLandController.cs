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
        public ActionResult MerchantLogin(Merchant merchant)
        {
            var merchantModel = Factory.Get<IMerchantModel>(SystemConst.IOC_Model.MerchantModel);
            var result = merchantModel.Login(merchant.Phone, merchant.LoginPwdPage);
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
        /// <returns></returns>
        public ActionResult Register(string BackUrl)
        {


            return View();
        }


    }
}
