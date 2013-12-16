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

        Result App_LoginForTempLogin(Poco.WebAPI_Poco.App_UserLoginInfo app_UserLoginInfo);


        /// <summary>
        /// 添加ClientInfo，[User]，UserLoginInfo（第一次注册）
        /// </summary>
        /// <param name="userLoginInfo"></param>
        /// <returns></returns>
        Result Register(App_UserLoginInfo userLoginInfo);

        /// <summary>
        /// 只添加ClientInfo，[User]，不添加UserLoginInfo（已与一个售楼部账号绑定，需要绑定另外售楼部账号时注册）
        /// </summary>
        /// <param name="userLoginInfo"></param>
        /// <returns></returns>
        Result Register2(App_UserLoginInfo userLoginInfo,int userLoginInfoID);

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

        //根据电话号查询用户
        UserLoginInfo getUserByPhone(string phone);

    }
}
