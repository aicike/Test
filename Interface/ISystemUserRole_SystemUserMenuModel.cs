using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ISystemUserRole_SystemUserMenuModel : IBaseModel<SystemUserRole_SystemUserMenu>
    {
        void BindPermission(int systemUserRoleID, int[] systemUserMenuIDs, int[] sytemUserMenuOptionIDs);
    }
}
