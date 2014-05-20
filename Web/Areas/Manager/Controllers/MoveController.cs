using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Areas.Manager.Controllers
{
    public class MoveController : ManageSystemUserController
    {
        //
        // GET: /Manager/Move/

        public ActionResult Index()
        {
            return View();
        }

    }
}
