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
            return List_Cache().Where(a => a.RoleOptions
                                           .Any(b => b.SystemStatus == (int)EnumSystemStatus.Active &&
                                                     b.RoleID == roleID)).ToList();
        }

        public List<MenuOption> List_Cache()
        {
            var obj = CacheModel.GetCache<List<MenuOption>>(SystemConst.Cache.MenuOption);
            if (obj != null)
            {
                return obj;
            }
            var list = base.List().ToList();
            CacheModel.SetCache(SystemConst.Cache.MenuOption, list);
            return list;
        }


        public void ReSetCache()
        {
            var list = base.List().ToList();
            CacheModel.SetCache(SystemConst.Cache.MenuOption, list);
        }
    }
}
