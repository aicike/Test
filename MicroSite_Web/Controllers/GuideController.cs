using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;

namespace MicroSite_Web.Controllers
{
    public class GuideController : ManageAccountController
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
