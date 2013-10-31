using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;

namespace Web.Controllers
{
    public class OrderController : ManageAccountController
    {
        //
        // GET: /Order/

        public ActionResult Index()
        {
            return View();
        }

    }
}
