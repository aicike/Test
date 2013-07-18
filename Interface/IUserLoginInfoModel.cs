using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IUserLoginInfoModel : IBaseModel<UserLoginInfo>
    {
        Result Login(string loginNameOrEmail, string pwd);

        Result Register(UserLoginInfo userLoginInfo);
    }
}
