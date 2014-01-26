using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IRoleMenuModel : IBaseModel<RoleMenu>
    {
        List<RoleMenu> List_Cache();
        void BindPermission(int? accountMainID,int roleID, int[]menuIDs, int[] menuOptionIDs,int []serviceIDs);
        void ReSetCache();
    }
}
