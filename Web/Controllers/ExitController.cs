using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ExitController : Controller
    {
        /// <summary>
        /// 平台退出方法
        /// </summary>
        /// <returns></returns>
        [NoClientCache]
        public ActionResult Index()
        {
            var type = Request.QueryString["type"];
            Session.Clear();
            Session.Abandon();
            ViewBag.Type = type;
            return View();
        }

        [HttpPost]
        [NoClientCache]
        public ActionResult Index(string type)
        {
            return RedirectToAction("Exit", "Exit", new { Area = "", type = type });
        }

        [NoClientCache]
        public ActionResult Exit(string type)
        {
            if (type == "sysUser")
            {
                return RedirectToAction("SystemLogin", "Login", new { Area = "" });
            }
            else if (type == "user")
            {
                return RedirectToAction("UserLogin", "Login", new { Area = "" });
            }
            ViewBag.ExitUrl = Url.Action("UserLogin", "Login", new { Area = "" });
            return View();
        }
    }
}
