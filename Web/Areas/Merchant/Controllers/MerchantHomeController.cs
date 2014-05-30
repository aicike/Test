using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Injection;
using Interface;
using Business;

namespace Web.Areas.Merchant.Controllers
{
    public class MerchantHomeController : ManageMerchantController
    {
        //
        // GET: /Merchant/MerchantHome/

        public ActionResult Index()
        {
            ViewBag.Title = "商户平台 - 首页 -" + SystemConst.PlatformName;
            return View();
        }

        /// <summary>
        /// 商户信息界面
        /// </summary>
        /// Error 1 ：成功 2：失败
        /// <returns></returns>
        public ActionResult MerchantsInfo(int ?Error)
        {
            ViewBag.Title = "商户平台 - 商户信息 -" + SystemConst.PlatformName;
            var merchantModel = Factory.Get<IMerchantModel>(SystemConst.IOC_Model.MerchantModel);
            var merchant = merchantModel.Get(LoginMerchant.ID);
            if (Error.HasValue)
            {
                ViewBag.Error = Error.Value;
            }
            else {
                ViewBag.Error = "0";
            }



            return View(merchant);
        }

        /// <summary>
        /// 保存商户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UPDMerchantsInfo(Poco.Merchant merchant, HttpPostedFileBase LogoImagePathFile)
        {
            var merchantModel = Factory.Get<IMerchantModel>(SystemConst.IOC_Model.MerchantModel);
            var result = merchantModel.EditInfo(merchant, LogoImagePathFile);
            if (result.HasError)
            {

                return RedirectToAction("MerchantsInfo", "MerchantHome", new { Error = "2" });
            }
            else
            {

                return RedirectToAction("MerchantsInfo", "MerchantHome", new { Error = "1" });
            }

        }

        /// <summary>
        /// 电话唯一验证
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost]
        public string ChickUniquePhone(string phone)
        {
            CommonModel com = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            return com.CheckIsUnique_Merchant("Merchant", "Phone", phone.Trim(),LoginMerchant.ID).ToString(); ;
        }

        /// <summary>
        /// 邮箱唯一验证
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        [HttpPost]
        public string ChickUniqueEmail(string email)
        {
            CommonModel com = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            return com.CheckIsUnique_Merchant("Merchant", "Email", email.Trim(), LoginMerchant.ID).ToString(); ;
        }
    }
}
