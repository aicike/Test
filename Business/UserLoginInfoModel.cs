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
        public Result Login(string loginNameOrEmail, string pwd)
        {
            Result result = new Result();
            pwd = DESEncrypt.Encrypt(pwd);
            var user = List().Where(a => a.Email.Equals(loginNameOrEmail, StringComparison.CurrentCultureIgnoreCase) && a.LoginPwd == pwd).FirstOrDefault();
            if (user == null)
            {
                result.Error = "邮箱或密码错误，登录失败。";
            }
            result.Entity = user;
            return result;
        }

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
    }
}
