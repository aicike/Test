using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Poco.Enum;
using System.Web;

namespace Interface
{
    public interface ISystemUserModel : IBaseModel<SystemUser>
    {
        Result Login(string email, string pwd);

        Result ChangeStatus(int ID, EnumAccountStatus status);

        Result Add(SystemUser user, HttpPostedFileBase HeadImagePathFile);

        Result Edit(SystemUser user, HttpPostedFileBase HeadImagePathFile);
    }
}
