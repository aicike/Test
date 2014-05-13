using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;

namespace Web.Areas.Merchant.Controllers
{
    public class M_TakeOutController : ManageMerchantController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
