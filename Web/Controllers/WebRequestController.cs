using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;

namespace Web.Controllers
{
    public class WebRequestController : Controller
    {
        [HttpPost]
        public void PostClientID(string clientID, int? userID)
        {
            var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
            clientInfoModel.PostClientID(clientID, userID);
        }
    }
}
