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
    }
}
