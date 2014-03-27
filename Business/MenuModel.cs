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
                List<Menu> newList = new List<Menu>(obj.ConvertAll<Menu>(a =>
                {
                    return new Menu
                    {
                        ID = a.ID,
                        SystemStatus = a.SystemStatus,
                        Token = a.Token,
                        Name = a.Name,
                        ShowName = a.ShowName,
                        Area = a.Area,
                        Controller = a.Controller,
                        Action = a.Action,
                        Order = a.Order,
                        ParentMenuID = a.ParentMenuID,
                        IsAppMenu = a.IsAppMenu,
                        Level = a.Level,
                        ParentMenu = a.ParentMenu,
                        ServiceID = a.ServiceID,
                        Service = a.Service,
                        Menus = a.Menus,
                        MenuOptions = a.MenuOptions,
                        RoleMenus = a.RoleMenus
                    };
                }));
                return newList;
            }
            var list = base.List(true).ToList();
            CacheModel.SetCache(SystemConst.Cache.Menu, list);
            return list;
        }

        /// <summary>
        /// 有级别限制
        /// </summary>
        /// <param name="systemUserRoleID"></param>
        /// <param name="parentSystemUserMenuID"></param>
        /// <returns></returns>
        public List<Menu> GetMenuByRoleID(List<int> roleIDs, int? parentMenuID = null)
        {
            List<Menu> list = List_Cache();
            list = list.Where(a => a.RoleMenus.Any(b => b.SystemStatus == (int)EnumSystemStatus.Active &&
              roleIDs.Contains(b.RoleID)) && a.ParentMenuID == parentMenuID).OrderBy(a => a.Order).ToList();
            foreach (var item in list)
            {
                item.Menus = item.Menus.Where(a => a.RoleMenus.Any(b => b.SystemStatus == (int)EnumSystemStatus.Active && roleIDs.Contains(b.RoleID))).OrderBy(a => a.Order).ToList();
            }
            return list;
        }

        /// <summary>
        /// 有级别限制(不用缓存)
        /// </summary>
        /// <param name="systemUserRoleID"></param>
        /// <param name="parentSystemUserMenuID"></param>
        /// <returns></returns>
        public List<Menu> GetMenuByRoleID_NoCache(List<int> roleIDs, int? parentMenuID = null)
        {
            List<Menu> list = base.List(true).ToList();
            list = list.Where(a => a.RoleMenus.Any(b => b.SystemStatus == (int)EnumSystemStatus.Active &&
              roleIDs.Contains(b.RoleID)) && a.ParentMenuID == parentMenuID).OrderBy(a => a.Order).ToList();
            foreach (var item in list)
            {
                item.Menus = item.Menus.Where(a => a.RoleMenus.Any(b => b.SystemStatus == (int)EnumSystemStatus.Active && roleIDs.Contains(b.RoleID))).OrderBy(a => a.Order).ToList();
            }
            return list;
        }


        public List<Menu> GetMenuForOrganization()
        {
            List<Menu> list = List_Cache();
            list = list.Where(a => a.RoleMenus.Any(b => b.SystemStatus == (int)EnumSystemStatus.Active &&
              a.ID == 21)).OrderBy(a => a.Order).ToList();
            foreach (var item in list)
            {
                item.Menus = item.Menus.Where(a => a.RoleMenus.Any(b => b.SystemStatus == (int)EnumSystemStatus.Active)).OrderBy(a => a.Order).ToList();
            }
            return list;
        }


        /// <summary>
        /// 超级管理员获取所有菜单
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetMenuBySuperAdmin()
        {
            List<Menu> list = List_Cache().Where(a => a.ParentMenuID == null).OrderBy(a => a.Order).ToList();
            foreach (var item in list)
            {
                item.Menus = item.Menus.OrderBy(a => a.Order).ToList();
            }
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

        public List<Menu> MicroSite_GetAllMenuByRoleID()
        {
            List<Menu> list = List_Cache().OrderBy(a => a.Order).ToList();
            return list;
        }

        public bool CheckHasPermissions(List<int> roleID, string action, string controller, string area)
        {
            //判断有没有权限操作当前菜单(Contorller)
            IRoleMenuModel roleMenuModel = Factory.Get<IRoleMenuModel>(SystemConst.IOC_Model.RoleMenuModel);

            var menu = roleMenuModel.List_Cache().Where(a => roleID.Contains(a.RoleID) &&
                                                       ((area != null && a.Menu.Area != null && a.Menu.Area.Equals(area, StringComparison.CurrentCultureIgnoreCase)) || (area == null && a.Menu.Area == null)) &&
                                                       (a.Menu.Controller != null && a.Menu.Controller.Equals(controller, StringComparison.CurrentCultureIgnoreCase))).Select(a => a.Menu).FirstOrDefault();

            if (menu == null) { return false; }

            ////判断有没有权限操作当前功能(Action)
            //IRoleOptionModel roleOptionModel = Factory.Get<IRoleOptionModel>(SystemConst.IOC_Model.RoleOptionModel);
            //var result = roleOptionModel.List_Cache().Any(a => roleID.Contains(a.RoleID) &&
            //                                             a.MenuOption.MenuID == menu.ID &&
            //                                             a.MenuOption.Action.Equals(action, StringComparison.CurrentCultureIgnoreCase));
            return true;
        }

        /// <summary>
        /// 检查有没有相应的菜单权限
        /// </summary>
        /// <param name="roleIDs">角色ID</param>
        /// <param name="menuToken">菜单token</param>
        /// <returns></returns>
        public bool CheckHasPermissions(List<int> roleIDs, string menuToken)
        {
            int[] menuIDArray = List().Where(a => menuToken == a.Token).Select(a => a.ID).ToArray();
            return GetMenuByRoleID(roleIDs).Any(a => menuIDArray.Contains(a.ID));
        }

        public void ReSetCache()
        {
            var list = base.List().ToList();
            CacheModel.SetCache(SystemConst.Cache.Menu, list);
        }


    }
}
