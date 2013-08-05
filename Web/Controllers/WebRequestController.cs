using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.WebAPI_Poco;

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

        [HttpPost]
        public string CheckEmailOnRegister(string email, int? userLoginInfoID)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            return Newtonsoft.Json.JsonConvert.SerializeObject(userLoginInfoModel.CheckEmailOnRegister(email));
        }

        [HttpPost]
        public string GetAccountList(int accountMainID)
        {
            var account_AccountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);

            string hostUrl = string.Format("http://{0}:{1}", Request.Url.Host, Request.Url.Port);

            var accountList = account_AccountMainModel.GetAccountListByAccountMainID(accountMainID).Select(a => new App_Account
            {
                ID = a.ID,
                Name = a.Name,
                HeadImagePath = hostUrl + Url.Content(a.HeadImagePath),
                Email = a.Email
            });
            return Newtonsoft.Json.JsonConvert.SerializeObject(accountList);
        }
    }
}
