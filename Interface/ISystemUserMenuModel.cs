using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ISystemUserMenuModel : IBaseModel<SystemUserMenu>
    {
        List<SystemUserMenu> GetMenuByRoleID(int systemUserRoleID, int? parentSystemUserMenuID = null);
        List<SystemUserMenu> GetAllMenuByRoleID(int systemUserRoleID);
        List<SystemUserMenu> List_Cache();
        bool CheckHasPermissions(int systemUserRoleID, string action, string controller, string area);
    }
}
