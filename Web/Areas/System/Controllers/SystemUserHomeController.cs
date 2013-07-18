using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.System.Controllers
{
    public class SystemUserHomeController : ManageSystemUserController
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
