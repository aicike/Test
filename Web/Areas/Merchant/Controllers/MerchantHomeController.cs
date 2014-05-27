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
        /// <returns></returns>
        public ActionResult MerchantsInfo(int ?Error)
        {
            ViewBag.Title = "商户平台 - 商户信息 -" + SystemConst.PlatformName;
            var merchantModel = Factory.Get<IMerchantModel>(SystemConst.IOC_Model.MerchantModel);
            var merchant = merchantModel.Get(LoginMerchant.ID);
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
            return View();
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
            return com.CheckIsUnique_Merchant("Merchant", "Phone", phone.Trim()).ToString(); ;
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
            return com.CheckIsUnique_Merchant("Merchant", "Email", email.Trim()).ToString(); ;
        }
    }
}
