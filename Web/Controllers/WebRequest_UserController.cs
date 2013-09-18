﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.WebAPI_Poco;
using Common;

namespace Web.Controllers
{
    public class WebRequest_UserController : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        public string UserLogin(string email, string loginPwd, int accountMainID)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var result = userLoginInfoModel.App_Login(new App_UserLoginInfo() { Email = email, Pwd = loginPwd, AccountMainID = accountMainID });
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
        /// <summary>
        /// 注册
        /// </summary>
        [HttpPost]
        public string UserRegister(App_UserLoginInfo userLoginInfo)
        {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var result = userLoginInfoModel.Register(userLoginInfo);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
        /// <summary>
        /// 每次打开应用时，提交的clientID
        /// </summary>
        [HttpPost]
        public string PostClientID(string clientID, int accountMainID, int? userID)
        {
            var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
            var result = clientInfoModel.PostClientID(clientID, accountMainID, userID);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取App登录页logo
        /// </summary>
        public string GetAppLogo(int accountMainID)
        {
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            string path = SystemConst.WebUrl + (accountMainModel.Get(accountMainID).LogoImageThumbnailPath).Replace("~", "");
            return path;
        }

        /// <summary>
        /// 检查邮箱是否已存在
        /// </summary>
        [HttpPost]
        public string CheckEmailOnRegister(string email, int? userLoginInfoID)
        {
            Result result = new Result();
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            if (userLoginInfoModel.ExistEmail(email, userLoginInfoID))
            {
                result.Error = "已存在相同邮箱。";
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取销售人员信息
        /// </summary>
        [HttpPost]
        public string GetAccountList(int accountMainID)
        {
            var account_AccountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);

            string hostUrl = SystemConst.WebUrl;

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

        /// <summary>
        /// 绑定User和Account
        /// </summary>
        public string BindUserAccount(int accountID, int userID)
        {
            var account_UserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
            var result = account_UserModel.BindUser_Account(accountID, userID);
            result.Entity = accountID;
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取绑定的AccountID
        /// </summary>
        public string GetBindAccountID(int userID, int accountMainID)
        {
            var account_UserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
            return account_UserModel.GetBindAccountID(userID, accountMainID) + "";
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public string SetUserInfo(int userID, string field, string value)
        {
            Result result = new Result();
            var um = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var user = um.Get(userID);
            if (user == null)
            {
                result.Error = "请求错误，用户不存在或不可用。";
            }
            var ulim = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var userLoginInfo = ulim.Get(user.UserLoginInfoID);
            switch (field)
            {
                case "name":
                    userLoginInfo.Name = value;
                    break;
                case "phone":
                    userLoginInfo.Phone = value;
                    break;
                case "email":
                    userLoginInfo.Email = value;
                    break;
                case "pwd":
                    userLoginInfo.LoginPwd = DESEncrypt.Encrypt(value);
                    break;
                case "headimg":
                    userLoginInfo.HeadImagePath = value;
                    break;
            }
            userLoginInfo.LoginPwdPage = "000000";
            result = ulim.Edit(userLoginInfo);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取售楼部坐标
        /// </summary>
        public string GetCoordinate(int accountMainID)
        {
            Result result = new Result();
            var AccountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var accountmain = AccountMainModel.Get(accountMainID);
            App_Coordinate ac = new App_Coordinate();
            if (string.IsNullOrEmpty(accountmain.Lat))
            {
                result.Error = "售楼部尚未定位坐标";
            }
            else
            {
                ac.Lat = accountmain.Lat;
                ac.Lng = accountmain.Lng;
                result.Entity = ac;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        public string FindPwd(string email) {
            var userLoginInfoModel = Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var result = userLoginInfoModel.FindPwd(email);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
    }
}
