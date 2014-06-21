﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IMenuModel : IBaseModel<Menu>
    {
        List<Menu> GetMenuByRoleID(List<int> roleIDs, int? parentMenuID = null);
        List<Menu> GetMenuByRoleID_NoCache(List<int> roleIDs, int? parentMenuID = null);
        List<Menu> GetMenuForOrganization();
        /// <summary>
        /// 超级管理员获取所有菜单
        /// </summary>
        /// <returns></returns>
        List<Menu> GetMenuBySuperAdmin();
        List<Menu> GetAllMenuByRoleID(int roleID);
        /// <summary>
        /// 全部菜单，无级别限制
        /// </summary>
        /// <param name="systemUserRoleID"></param>
        /// <param name="parentSystemUserMenuID"></param>
        /// <returns></returns>
        List<Menu> GetAllMenuByRoleIDs(List<int> roleIDs);
        List<Menu> List_Cache();
        bool CheckHasPermissions(List<int> roleID, string action, string controller, string area);
        void ReSetCache();

        List<Menu> MicroSite_GetAllMenuByRoleID();


        /// <summary>
        /// 检查有没有相应的菜单权限
        /// </summary>
        /// <param name="roleIDs">角色ID</param>
        /// <param name="menuToken">菜单token</param>
        /// <returns></returns>
        bool CheckHasPermissions(List<int> roleIDs, string menuToken);

        int GetMenuIDByToken(string token);
    }
}