using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Controllers;

namespace Web.Controllers
{
    public class HolidayController : ManageAccountController
    {
        //
        // GET: /Holiday/

        public ActionResult Index(int? id)
        {

            var holidModel = Factory.Get<IHolidayModel>(SystemConst.IOC_Model.HolidayModel);
            var holidlist = holidModel.GetListByAMID(LoginAccount.CurrentAccountMainID).ToPagedList(id ?? 1, 15);

            return View(holidlist);
        }

    }
}
