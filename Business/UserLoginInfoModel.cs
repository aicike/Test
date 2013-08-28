using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;
using Poco.Enum;
using Common;
using Injection.Transaction;
using Poco.WebAPI_Poco;

namespace Business
{
    public class UserLoginInfoModel : BaseModel<UserLoginInfo>, IUserLoginInfoModel
    {
        [Transaction]
        public Result Register(App_UserLoginInfo userLoginInfo)
        {
            Result result = new Result();
            //检查邮箱是否通过，是否可以注册
            if (string.IsNullOrEmpty(userLoginInfo.Email) == false)
            {
                bool isExist = List().Any(a => a.Email.Equals(userLoginInfo.Email, StringComparison.CurrentCultureIgnoreCase));
                if (isExist)
                {
                    result.Error = "该邮箱已存在,不能创建账号.";
                    return result;
                }
            }
            //添加用户登录信息UserLoginInfo
            UserLoginInfo userlogin = new UserLoginInfo();
            userlogin.LoginPwd = DESEncrypt.Encrypt(userLoginInfo.Pwd);
            userlogin.LoginPwdPage = "000000";
            userlogin.Name = userLoginInfo.Name;
            userlogin.Email = userLoginInfo.Email;
            result = base.Add(userlogin);
            if (result.HasError)
            {
                return result;
            }
            //添加用户User
            User user = new User();
            user.UserLoginInfoID = userlogin.ID;
            user.Name = userlogin.Name;
            user.AccountMainID = userLoginInfo.AccountMainID;
            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            result = userModel.Add(user);
            if (result.HasError)
            {
                return result;
            }
            //添加用户和Account关系
            if (userLoginInfo.AccountID != null && userLoginInfo.AccountID.HasValue)
            {
                var account_UserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
                result= account_UserModel.BindUser_Account(userLoginInfo.AccountID.Value, user.ID);
            }
            if (result.HasError)
            {
                return result;
            }
            //添加用户和ClientInfo信息
            var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
            var client = clientInfoModel.List().Where(a => a.ClientID.Equals(userLoginInfo.ClientID)).FirstOrDefault();
            int enumClientSystemTypeID = LookupFactory.GetLookupOptionIdByToken((EnumClientSystemType)userLoginInfo.EnumClientSystemType);
            int enumClientUserTypeID = LookupFactory.GetLookupOptionIdByToken((EnumClientUserType)userLoginInfo.EnumClientUserType);
            if (client == null)
            {
                //数据库中没有client信息，需要新增
                ClientInfo clientInfo = new ClientInfo();
                clientInfo.EnumClientSystemTypeID = enumClientSystemTypeID;
                clientInfo.SetupTiem = DateTime.Now;
                clientInfo.EnumClientUserTypeID = enumClientUserTypeID;
                clientInfo.ClientID = userLoginInfo.ClientID;
                clientInfo.EntityID = user.ID;
                result = clientInfoModel.Add(clientInfo);
                if (result.HasError)
                {
                    return result;
                }
            }
            else
            {
                //数据库中有client信息，需要绑定
                client.EnumClientSystemTypeID = enumClientSystemTypeID;
                client.EnumClientUserTypeID = enumClientUserTypeID;
                client.EntityID = user.ID;
                result = clientInfoModel.Edit(client);
                if (result.HasError)
                {
                    return result;
                }
            }
            result.Entity = user.ID;
            return result;
        }

        public Result App_Login(Poco.WebAPI_Poco.App_UserLoginInfo app_UserLoginInfo)
        {
            Result result = new Result();
            var pwd = DESEncrypt.Encrypt(app_UserLoginInfo.Pwd);

            var accountStatus = EnumAccountStatus.Enabled.ToString();

            var user = List().Where(a => a.Email.Equals(app_UserLoginInfo.Email, StringComparison.CurrentCultureIgnoreCase) && a.LoginPwd == pwd
                && a.Users.Any(b => b.SystemStatus == (int)EnumSystemStatus.Active && b.AccountStatus.Token == accountStatus)).FirstOrDefault();
            if (user == null)
            {
                result.Error = "邮箱或密码错误，登录失败。";
            }
            user.CurrenRelatedUser = user.Users.Where(a => a.AccountMainID == app_UserLoginInfo.AccountMainID).FirstOrDefault();

            UserLoginInfo newUser = new UserLoginInfo()
            {
                ID = user.ID,
            };
            result.Entity = newUser;
            return result;
        }


        public Result CheckEmailOnRegister(string email, int? userLoginInfoID = null)
        {
            Result result = new Result();
            if (userLoginInfoID != null && userLoginInfoID.HasValue && userLoginInfoID.Value > 0)
            {
                var exist = List().Any(a => a.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase) && a.ID != userLoginInfoID.Value);
                if (exist)
                {
                    result.Error = "该邮箱已存在。";
                }
            }
            else
            {
                var exist = List().Any(a => a.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
                if (exist)
                {
                    result.Error = "该邮箱已存在。";
                }
            }
            return result;
        }
    }
}
