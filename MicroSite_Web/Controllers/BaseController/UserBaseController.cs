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
        protected User LoginUser
        {
            get
            {
                var user = Session[SystemConst.Session.LoginUser] as User;
                return user;
            }
            set { Session[SystemConst.Session.LoginUser] = value; }
        }
        
        public static int GetAccountMainID()
        {
            var accountMainID = CacheModel.GetCache_Struct<int>(SystemConst.Cache.AccountMainID);
            if (accountMainID != 0)
            {
                return accountMainID;
            }
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            if (SystemConst.IsIntegrationWebProject)
            {
                throw new Exception("平台集成的微网站项目，为获取到amid。");
            }
            else
            {
                accountMainID = accountMainModel.List().Select(a => a.ID).FirstOrDefault();
                if (accountMainID == 0) {
                    throw new Exception("该网站还未配置基本信息，无法使用");
                }
                CacheModel.SetCache(SystemConst.Cache.AccountMainID, accountMainID);
                return accountMainID;
            }
        }
    }
}