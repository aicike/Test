using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IMenuModel : IBaseModel<Menu>
    {
        List<Menu> GetMenuByRoleID(List<int> roleIDs, int? parentMenuID = null);
        List<Menu> GetAllMenuByRoleID(int roleID);
        List<Menu> List_Cache();
        bool CheckHasPermissions(List<int> roleID, string action, string controller, string area);
        void ReSetCache();
    }
}