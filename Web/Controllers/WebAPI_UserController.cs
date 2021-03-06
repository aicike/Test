﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Poco;
using Interface;
using Injection;
using Poco.Enum;
using Poco.WebAPI_Poco;

namespace Web.Controllers
{
    public class WebAPI_UserController : ApiController
    {
        public IList<AccountMain> GetAllProducts()
        {
            IAccountMainModel accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            return accountMainModel.List().ToList().Select(a => new AccountMain() { Name = a.Name }).ToList();
        }

        [HttpGet]
        public Result GetLogin(App_UserLoginInfo user)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var result = userLoginInfoModel.App_Login(user);
            return result;
        }

        [HttpPost]
        public Result PostRegister(App_UserLoginInfo user)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var result = userLoginInfoModel.Register(user);
            return result;
        }
    }
}