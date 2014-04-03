using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;

namespace MicroSite_Web.Controllers
{
    public class MicroSiteSetController : ManageAccountController
    {
        public ActionResult Index()
        {
            ViewBag.HostName = LoginAccount.HostName;
            return View();
        }

    }
}
