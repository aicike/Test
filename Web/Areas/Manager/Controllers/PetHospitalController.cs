using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.Manager.Controllers
{
    /// <summary>
    /// 宠物医院
    /// </summary>
    public class PetHospitalController : ManageSystemUserController
    {
        //
        // GET: /Manager/PetHospital/

        public ActionResult Index()
        {
            return View();
        }

    }
}
