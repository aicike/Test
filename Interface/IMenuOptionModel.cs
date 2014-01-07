using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IMenuOptionModel : IBaseModel<MenuOption>
    {
        List<MenuOption> GetAllOptionByRoleID(int roleID);

        List<MenuOption> List_Cache();
        void ReSetCache();
    }
}
