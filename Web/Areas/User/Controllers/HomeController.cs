using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;

namespace Web.Areas.User.Controllers
{
    public class HomeController : ManageAccountController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
