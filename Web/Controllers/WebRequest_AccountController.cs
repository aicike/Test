﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.WebAPI_Poco;
using Poco.Enum;
using Common;

namespace Web.Controllers
{
    public class WebRequest_AccountController : Controller
    {
        /// <summary>
        /// 售楼部App登录
        /// </summary>
        public string AccountLogin(int accountMainID, string email, string pwd)
        {
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var result = accountModel.LoginApp(email, pwd);
            if (result.HasError)
            {
                result = new Result() { Error = result.Error };
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            var account = result.Entity as Account;
            string hostUrl = SystemConst.WebUrlIP;
            result = new Result()
            {
                Entity = new App_Account()
                {
                    ID = account.ID,
                    Name = account.Name,
                    HeadImagePath = hostUrl + Url.Content(account.HeadImagePath),
                    Phone = account.Phone,
                    Email = account.Email,
                    Pwd = pwd
                }
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 根据ID获取售楼部账号信息
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public string GetAccountLogin(int aid)
        {
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = accountModel.Get(aid);
            Result result = new Result()
            {
                Entity = new App_Account()
                {
                    ID = account.ID,
                    Name = account.Name,
                    HeadImagePath = SystemConst.WebUrlIP + Url.Content(account.HeadImagePath),
                    Phone=account.Phone,
                    Email = account.Email,
                    Pwd = account.LoginPwd
                }
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 修改售楼部账号信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public string SetAccountInfo(int aid, string field, string value)
        {
            Result result = new Result();
            var um = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = um.Get(aid);
            if (account == null)
            {
                result.Error = "请求错误，账号不存在或不可用。";
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            switch (field)
            {
                case "name":
                    account.Name = value;
                    break;
                case "phone":
                    account.Phone = value;
                    break;
                case "email":
                    account.Email = value;
                    break;
                case "pwd":
                    account.LoginPwd = DESEncrypt.Encrypt(value);
                    break;
                case "headimg":
                    account.HeadImagePath = value.Replace(SystemConst.WebUrlIP,"~");
                    break;
            }
            account.LoginPwdPage = "000000";
            account.LoginPwdPageCompare = "000000";
            result = um.Edit(account);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取全部用户
        /// </summary>
        public string GetUserList(int accountMainID, int accountID)
        {
            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var list = userModel.GetUserListByAccountID(accountMainID, accountID);
            List<App_User> users = new List<App_User>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    users.Add(new App_User()
                    {
                        ID = item.ID,
                        Name = item.UserLoginInfo.Name,
                        NameNote = item.Name,
                        HeadImagePath = string.IsNullOrEmpty(item.UserLoginInfo.HeadImagePath) ? (SystemConst.WebUrlIP + Url.Content(item.UserLoginInfo.HeadImagePath.DefaultHeadImage())) : (SystemConst.WebUrlIP + Url.Content(item.UserLoginInfo.HeadImagePath)),
                        GN = item.Account_Users.FirstOrDefault().Group.GroupName
                    });
                }
            }
            Result result = new Result();
            result.Entity = users;
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取全部用户(带分组)
        /// </summary>
        public string GetUserGroupList(int accountMainID, int accountID)
        {
            var groupModel = Factory.Get<IGroupModel>(SystemConst.IOC_Model.GroupModel);
            var groups = groupModel.GetGroupListByAccountID(accountID, accountMainID);
            List<App_Group> groupList = new List<App_Group>();
            if (groups != null && groups.Count > 0)
            {
                foreach (var item in groups)
                {
                    groupList.Add(new App_Group()
                    {
                        ID = item.ID,
                        GN = item.GroupName,
                        UL = item.Account_Users.Where(a => a.SystemStatus == (int)EnumSystemStatus.Active)
                        .Select(a => new App_User()
                        {
                            ID = a.UserID,
                            Name = a.User.UserLoginInfo.Name,
                            NameNote = a.User.Name,
                            HeadImagePath = string.IsNullOrEmpty(a.User.UserLoginInfo.HeadImagePath) ? (SystemConst.WebUrlIP + Url.Content(a.User.UserLoginInfo.HeadImagePath.DefaultHeadImage())) : a.User.UserLoginInfo.HeadImagePath,
                            GN = item.GroupName
                        }).ToList()
                    });
                }
            }
            Result result = new Result();
            result.Entity = groupList;
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        public string GetUserInfo(int userID)
        {
            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var user = userModel.Get(userID);
            Result result = new Result();
            if (user != null)
            {
                App_User appuser = new App_User();
                appuser.ID = user.ID;
                appuser.Name = user.UserLoginInfo.Name ?? "";
                appuser.Phone = user.UserLoginInfo.Phone ?? "";
                appuser.Email = user.UserLoginInfo.Email ?? "";
                appuser.NameNote = user.Name ?? "";
                appuser.HeadImagePath = string.IsNullOrEmpty(user.UserLoginInfo.HeadImagePath) ? (SystemConst.WebUrlIP + Url.Content(user.UserLoginInfo.HeadImagePath.DefaultHeadImage())) : user.UserLoginInfo.HeadImagePath;
                appuser.Pwd = Common.DESEncrypt.Decrypt(user.UserLoginInfo.LoginPwd);
                result.Entity = appuser;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
    }
}
