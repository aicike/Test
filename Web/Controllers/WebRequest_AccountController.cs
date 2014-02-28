using System;
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
using Business;

namespace Web.Controllers
{
    public class WebRequest_AccountController : Controller
    {
        /// <summary>
        /// 售楼部App登录
        /// </summary>
        public string AccountLogin(string email, string pwd)
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
            string HeadPath = "";
            if (!string.IsNullOrEmpty(account.HeadImagePath))
            {
                HeadPath = hostUrl + Url.Content(account.HeadImagePath ?? "~/Images/default_avatar.png");
            }
            result = new Result()
            {
                Entity = new App_Account()
                {
                    ID = account.ID,
                    Name = account.Name,
                    HeadImagePath = HeadPath,
                    Phone = account.Phone,
                    Email = account.Email,
                    Pwd = pwd,
                    AccountMainID =account.CurrentAccountMainID
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
            string HeadPath = "";
            if (!string.IsNullOrEmpty(account.HeadImagePath))
            {
                HeadPath = SystemConst.WebUrlIP + Url.Content(account.HeadImagePath ?? "~/Images/default_avatar.png");
            }
            Result result = new Result()
            {
                Entity = new App_Account()
                {
                    ID = account.ID,
                    Name = account.Name,
                    HeadImagePath = HeadPath,
                    Phone = account.Phone,
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
            CommonModel model = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            switch (field)
            {
                case "name":
                    account.Name = value;
                    break;
                case "phone":

                    var isOk = model.CheckIsUnique("Account", "Phone", value, aid);
                    if (isOk == false)
                    {
                        result.Error = "该电话已被其他账号使用。";
                        result.HasError = true;
                        return Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    }


                    account.Phone = value;
                    break;
                case "email":

                    var EmailisOk = model.CheckIsUnique("Account", "Email", value, aid);
                    if (EmailisOk == false)
                    {
                        result.Error = "该电话已被其他账号使用。";
                        result.HasError = true;
                        return Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    }
                    account.Email = value;
                    break;
                case "pwd":
                    account.LoginPwd = DESEncrypt.Encrypt(value);
                    break;
                case "headimg":
                    account.HeadImagePath = value.Replace(SystemConst.WebUrlIP, "~");
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
                        HeadImagePath = string.IsNullOrEmpty(item.UserLoginInfo.HeadImagePath) ? (SystemConst.WebUrlIP + Url.Content(item.UserLoginInfo.HeadImagePath.DefaultHeadImage() ?? "~/Images/default_avatar.png")) : (SystemConst.WebUrlIP + Url.Content(item.UserLoginInfo.HeadImagePath ?? "~/Images/default_avatar.png")),
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
                            HeadImagePath = string.IsNullOrEmpty(a.User.UserLoginInfo.HeadImagePath) ? (SystemConst.WebUrlIP + Url.Content(a.User.UserLoginInfo.HeadImagePath.DefaultHeadImage() ?? "~/Images/default_avatar.png")) : (SystemConst.WebUrlIP + Url.Content(a.User.UserLoginInfo.HeadImagePath ?? "~/Images/default_avatar.png")),
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
                appuser.HeadImagePath = string.IsNullOrEmpty(user.UserLoginInfo.HeadImagePath) ? "" : (SystemConst.WebUrlIP + Url.Content(user.UserLoginInfo.HeadImagePath ?? "~/Images/default_avatar.png"));
                appuser.Pwd = Common.DESEncrypt.Decrypt(user.UserLoginInfo.LoginPwd);
                result.Entity = appuser;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 修改用户备注
        /// </summary>
        /// <returns></returns>
        public string ChangeUserNameNote(int userID, string nameNote)
        {
            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var user = userModel.Get(userID);
            Result result = null;
            if (user != null)
            {
                user.Name = nameNote;
                result = userModel.Edit(user);
            }
            else
            {
                result = new Result();
                result.Error = "未找到用户信息，请稍后再试。";
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

    }
}
