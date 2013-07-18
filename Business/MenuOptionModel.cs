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
    public class MenuOptionModel : BaseModel<MenuOption>, IMenuOptionModel
    {
        public List<MenuOption> GetAllOptionByRoleID(int roleID)
        {
            return base.List().Where(a => a.RoleOptions
                                           .Any(b => b.SystemStatus == (int)EnumSystemStatus.Active &&
                                                     b.RoleID == roleID)).ToList();
        }
    }
}
