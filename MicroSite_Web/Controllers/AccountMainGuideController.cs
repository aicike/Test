using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Injection;
using Interface;
using Poco;
using Controllers;
using Business;

namespace Web.Controllers
{
    public class AccountMainGuideController : ManageAccountController
    {
        //
        // GET: /AccountMainGuide/
        [AllowCheckPermissions(false)]
        public ActionResult Index()
        {
            AccountMainGuideModel AMG = new AccountMainGuideModel();
            bool isUntreated;
            DataTable dt = AMG.getUntreated(LoginAccount.CurrentAccountMainID,out isUntreated);
            foreach (DataRow row in dt.Rows)
            {
                if (row["Status"] == "1" || row["Status"] == "3")
                {
                    row["Conntroller"] = Url.Action(row["View"].ToString(), row["Conntroller"].ToString());
                }
            }
            ViewBag.Dt = dt;
            return View();
        }

    }
}
