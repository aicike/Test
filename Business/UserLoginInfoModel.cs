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

namespace Business
{
    public class UserLoginInfoModel : BaseModel<UserLoginInfo>, IUserLoginInfoModel
    {
        [Transaction]
        public Result Register(UserLoginInfo userLoginInfo)
        {
            userLoginInfo.LoginPwd = DESEncrypt.Encrypt(userLoginInfo.LoginPwd);
            Result result = base.Add(userLoginInfo);
            if (result.HasError == false)
            {
                User user = new User();
                user.UserLoginInfoID = userLoginInfo.ID;
                var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
                result = userModel.Add(user);
            }
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
                ID=user.ID,
            };
            result.Entity = newUser;
            return result;
        }
    }
}
