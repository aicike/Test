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

    }
}
