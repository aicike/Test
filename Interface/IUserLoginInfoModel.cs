using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Poco.WebAPI_Poco;

namespace Interface
{
    public interface IUserLoginInfoModel : IBaseModel<UserLoginInfo>
    {
        Result App_Login(App_UserLoginInfo app_UserLoginInfo);

        Result Register(App_UserLoginInfo userLoginInfo);

        bool ExistEmail(string email, int? userLoginInfoID = null);

        bool ExistPhone(string phone, int? userLoginInfoID = null);

        UserLoginInfo GetByUserID(int userID);

        UserLoginInfo GetByClientID(string clientID);

        /// <summary>
        /// 找回密码
        /// </summary>
        Result FindPwd(string email);

        /// <summary>
        /// 找回密码_检查激活码
        /// </summary>
        Result FindPwd_CheckCode(string code);

        /// <summary>
        /// 找回密码_修改密码
        /// </summary>
        Result FindPwd_ChangePwd(string code, string pwd);
    }
}
