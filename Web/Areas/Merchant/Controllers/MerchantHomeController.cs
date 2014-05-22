using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;

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
        public ActionResult MerchantsInfo()
        {
            ViewBag.Title = "商户平台 - 商户信息 -" + SystemConst.PlatformName;
            return View();
        }

    }
}
