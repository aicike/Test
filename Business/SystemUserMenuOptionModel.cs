using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Poco.Enum;
using Common;

namespace Business
{
    public class SystemUserMenuOptionModel : BaseModel<SystemUserMenuOption>, ISystemUserMenuOptionModel
    {
        public List<SystemUserMenuOption> GetAllOptionByRoleID(int systemUserRoleID)
        {
            return base.List(true).Where(a => a.SystemUserRole_Options
                                           .Any(b => b.SystemStatus == (int)EnumSystemStatus.Active &&
                                                     b.SystemUserRoleID == systemUserRoleID)).ToList();
        }
    }
}
