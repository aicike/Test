using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ISystemUserMenuOptionModel : IBaseModel<SystemUserMenuOption>
    {
        List<SystemUserMenuOption> GetAllOptionByRoleID(int systemUserRoleID);
    }
}
