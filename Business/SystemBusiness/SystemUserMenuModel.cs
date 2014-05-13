using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Poco.Enum;
using Common;
using Injection;

namespace Business
{
    public class SystemUserMenuModel : BaseModel<SystemUserMenu>, ISystemUserMenuModel
    {
        public List<SystemUserMenu> List_Cache()
        {
            var obj = CacheModel.GetCache<List<SystemUserMenu>>(SystemConst.Cache.SystemUserMenu);
            if (obj != null)
            {
                return obj;
            }
            var list = base.List(true).ToList();
            CacheModel.SetCache(SystemConst.Cache.SystemUserMenu, list);
            return list;
        }

        /// <summary>
        /// 有级别限制
        /// </summary>
        /// <param name="systemUserRoleID"></param>
        /// <param name="parentSystemUserMenuID"></param>
        /// <returns></returns>
        public List<SystemUserMenu> GetMenuByRoleID(int systemUserRoleID, int? parentSystemUserMenuID = null)
        {
            List<SystemUserMenu> list = List_Cache();
            if (systemUserRoleID == 1)
            {
                list = list.Where(a => a.ParentMenuID == parentSystemUserMenuID).OrderBy(a => a.Order).ToList();
            }
            else
            {
                list = list.Where(a => a.SystemUserRole_SystemUserMenus.Any(b => b.SystemStatus == (int)EnumSystemStatus.Active &&
                   b.SystemUserRoleID == systemUserRoleID) && a.ParentMenuID == parentSystemUserMenuID).OrderBy(a => a.Order).ToList();
            }
            return list;
        }

        /// <summary>
        /// 全部菜单，无级别限制
        /// </summary>
        /// <param name="systemUserRoleID"></param>
        /// <param name="parentSystemUserMenuID"></param>
        /// <returns></returns>
        public List<SystemUserMenu> GetAllMenuByRoleID(int systemUserRoleID)
        {
            List<SystemUserMenu> list = List_Cache();
            list = list.Where(a => a.SystemUserRole_SystemUserMenus.Any(b => b.SystemStatus == (int)EnumSystemStatus.Active &&
               b.SystemUserRoleID == systemUserRoleID)).OrderBy(a => a.Order).ToList();
            return list;
        }

        public bool CheckHasPermissions(int systemUserRoleID, string action, string controller, string area)
        {
            if (systemUserRoleID == 1)
            {
                return true;
            }
            //判断有没有权限操作当前菜单(Contorller)
            ISystemUserRole_SystemUserMenuModel systemUserRole_SystemUserMenuModel = Factory.Get<ISystemUserRole_SystemUserMenuModel>(SystemConst.IOC_Model.SystemUserRole_SystemUserMenuModel);
            var menu = systemUserRole_SystemUserMenuModel.List().Where(a => a.SystemUserRoleID == systemUserRoleID &&
                                                                                   a.SystemUserMenu.Area.Equals(area, StringComparison.CurrentCultureIgnoreCase) &&
                                                                                   a.SystemUserMenu.Controller.Equals(controller, StringComparison.CurrentCultureIgnoreCase)).Select(a=>a.SystemUserMenu).SingleOrDefault();

            if (menu == null) { return false; }

            //判断有没有权限操作当前功能(Action)
            ISystemUserRole_OptionModel systemUserRole_OptionModel = Factory.Get<ISystemUserRole_OptionModel>(SystemConst.IOC_Model.SystemUserRole_OptionModel);
            var result = systemUserRole_OptionModel.List().Any(a => a.SystemUserRoleID == systemUserRoleID &&
                                                                                   a.SystemUserMenuOption.SystemUserMenuID == menu.ID &&
                                                                                   a.SystemUserMenuOption.Action.Equals(action, StringComparison.CurrentCultureIgnoreCase));
            return result;
        }
    }
}
