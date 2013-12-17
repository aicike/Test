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
    public class MenuModel : BaseModel<Menu>, IMenuModel
    {
        public List<Menu> List_Cache()
        {
            var obj = CacheModel.GetCache<List<Menu>>(SystemConst.Cache.Menu);
            if (obj != null)
            {
                return obj;
            }
            var list = base.List().ToList();
            CacheModel.SetCache(SystemConst.Cache.Menu, list);
            return list;
        }

        /// <summary>
        /// 有级别限制
        /// </summary>
        /// <param name="systemUserRoleID"></param>
        /// <param name="parentSystemUserMenuID"></param>
        /// <returns></returns>
        public List<Menu> GetMenuByRoleID(int roleID, int? parentMenuID = null)
        {
            List<Menu> list = List_Cache();
            list = list.Where(a => a.RoleMenus.Any(b => b.SystemStatus == (int)EnumSystemStatus.Active &&
               b.RoleID == roleID) && a.ParentMenuID == parentMenuID).OrderBy(a => a.Order).ToList();
            return list;
        }

        /// <summary>
        /// 全部菜单，无级别限制
        /// </summary>
        /// <param name="systemUserRoleID"></param>
        /// <param name="parentSystemUserMenuID"></param>
        /// <returns></returns>
        public List<Menu> GetAllMenuByRoleID(int roleID)
        {
            List<Menu> list = List_Cache();
            list = list.Where(a => a.RoleMenus.Any(b => b.SystemStatus == (int)EnumSystemStatus.Active &&
               b.RoleID == roleID)).OrderBy(a => a.Order).ToList();
            return list;
        }

        public bool CheckHasPermissions(int roleID, string action, string controller, string area)
        {
            //判断有没有权限操作当前菜单(Contorller)
            IRoleMenuModel roleMenuModel = Factory.Get<IRoleMenuModel>(SystemConst.IOC_Model.RoleMenuModel);
            var menu = roleMenuModel.List_Cache().Where(a => a.RoleID == roleID &&
                                                       ((area != null && a.Menu.Area.Equals(area, StringComparison.CurrentCultureIgnoreCase)) || (area == null && a.Menu.Area == null)) &&
                                                       a.Menu.Controller.Equals(controller, StringComparison.CurrentCultureIgnoreCase)).Select(a => a.Menu).FirstOrDefault();

            if (menu == null) { return false; }

            //判断有没有权限操作当前功能(Action)
            IRoleOptionModel roleOptionModel = Factory.Get<IRoleOptionModel>(SystemConst.IOC_Model.RoleOptionModel);
            var result = roleOptionModel.List_Cache().Any(a => a.RoleID == roleID &&
                                                         a.MenuOption.MenuID == menu.ID &&
                                                         a.MenuOption.Action.Equals(action, StringComparison.CurrentCultureIgnoreCase));
            return result;
        }


        public void ReSetCache()
        {
            var list = base.List().ToList();
            CacheModel.SetCache(SystemConst.Cache.Menu, list);
        }
    }
}
