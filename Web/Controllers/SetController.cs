using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;

namespace Web.Controllers
{
    public class SetController : ManageAccountController
    {
        //设置页
        // GET: /Set/

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.HostName = LoginAccount.HostName;
            return View();
        }

    }
}
