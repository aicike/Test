using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.Manager.Controllers
{
    /// <summary>
    /// 家教
    /// </summary>
    public class TutorController : ManageSystemUserController
    {
        //
        // GET: /Manager/M_Tutor/

        public ActionResult Index()
        {
            return View();
        }

    }
}
