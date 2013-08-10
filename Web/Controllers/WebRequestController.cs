﻿using System;
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
        /// <summary>
        /// 每次打开应用时，提交的clientID
        /// </summary>
        [HttpPost]
        public void PostClientID(string clientID, int? userID)
        {
            var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
            clientInfoModel.PostClientID(clientID, userID);
        }

        /// <summary>
        /// 检查邮箱是否已存在
        /// </summary>
        [HttpPost]
        public string CheckEmailOnRegister(string email, int? userLoginInfoID)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            return Newtonsoft.Json.JsonConvert.SerializeObject(userLoginInfoModel.CheckEmailOnRegister(email));
        }

        /// <summary>
        /// 获取销售人员信息
        /// </summary>
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

        /// <summary>
        /// 获取项目列表信息
        /// </summary>
        public string GetProjectList(int accountMainID)
        {
            var accountMainHousesModel = Factory.Get<IAccountMainHousesModel>(SystemConst.IOC_Model.AccountMainHousesModel);
            var list = accountMainHousesModel.GetList(accountMainID).OrderBy(a => a.ID).Select(a => new App_AccountMainHouse
            {
                ID = a.ID,
                HName = a.HName
            }).ToList();
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }
    }
}