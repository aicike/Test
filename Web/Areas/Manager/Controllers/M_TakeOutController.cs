using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.Manager.Controllers
{
    public class M_TakeOutController : ManageSystemUserController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
