using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Merchant.Controllers
{
    public class MerchantHomeController : Controller
    {
        //
        // GET: /Merchant/MerchantHome/

        public ActionResult Index()
        {
            return View();
        }

    }
}
