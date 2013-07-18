using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ISystemUserModel : IBaseModel<SystemUser>
    {
        Result Login(string email, string pwd);
    }
}
