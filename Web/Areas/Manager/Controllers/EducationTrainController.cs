using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.Manager.Controllers
{
    /// <summary>
    /// 教育培训
    /// </summary>
    public class EducationTrainController : ManageSystemUserController
    {
        //
        // GET: /Manager/EducationTrain/

        public ActionResult Index()
        {
            return View();
        }

    }
}
