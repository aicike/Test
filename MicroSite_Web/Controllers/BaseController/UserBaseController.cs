using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Poco;
using Common;
using Injection;
using Interface;

namespace MicroSite_Web.Controllers
{
    public class UserBaseController : System.Web.Mvc.BaseController
    {
        public static int GetAccountMainID()
        {
            var accountMainID = CacheModel.GetCache_Struct<int>(SystemConst.Cache.AccountMainID);
            if (accountMainID!=0)
            {
                return accountMainID;
            }
            var accountMainModel= Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            accountMainID = accountMainModel.List().Select(a=>a.ID).FirstOrDefault();
            CacheModel.SetCache(SystemConst.Cache.AccountMainID, accountMainID);
            return accountMainID;
        }
    }
}