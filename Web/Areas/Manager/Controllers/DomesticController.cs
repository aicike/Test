using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.Manager.Controllers
{
    /// <summary>
    /// 家政服务
    /// </summary>
    public class DomesticController : ManageSystemUserController
    {
        //
        // GET: /Manager/Domestic/

        public ActionResult Index()
        {
            return View();
        }

    }
}
