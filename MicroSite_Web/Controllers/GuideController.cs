using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Controllers;
using Poco;
using Injection;
using Interface;

namespace MicroSite_Web.Controllers
{
    public class GuideController : ManageAccountController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string SetAccountMain(AccountMain accountMain)
        {
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var count = accountMainModel.List().Count();
            if (count > 0)
            {
                //return "";
            }
            else
            {

            }
            return "<script>alert('false')</script>";
        }

    }
}
