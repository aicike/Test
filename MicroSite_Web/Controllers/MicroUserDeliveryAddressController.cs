using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;

namespace MicroSite_Web.Controllers
{
    public class MicroUserDeliveryAddressController : UserBaseController
    {
        public ActionResult Index(int amid, int userID)
        {
            var model = Factory.Get<IUserDeliveryAddressModel>(SystemConst.IOC_Model.UserDeliveryAddressModel);
            var list = model.GetListByUserID(userID, amid);
            ViewBag.AMID = amid;
            ViewBag.UserID = userID;
            return View(list);
        }

        [HttpGet]
        public ActionResult Add(int amid, int userID)
        {
            ViewBag.AMID = amid;
            ViewBag.UserID = userID;
            return View();
        }

        [HttpPost]
        public string Add(int amid, int userID, UserDeliveryAddress uda)
        {
            var model = Factory.Get<IUserDeliveryAddressModel>(SystemConst.IOC_Model.UserDeliveryAddressModel);
            uda.AccountMainID = amid;
            uda.UserID = userID;
            var result = model.Add(uda);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
    }
}
