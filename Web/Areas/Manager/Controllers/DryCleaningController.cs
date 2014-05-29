using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.Manager.Controllers
{
    /// <summary>
    /// 干洗服务
    /// </summary>
    public class DryCleaningController : ManageSystemUserController
    {
        //
        // GET: /Manager/DryCleaning/

        public ActionResult Index()
        {
            return View();
        }

    }
}
