using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Poco.Enum;
using Common;
using Injection;
using Injection.Transaction;

namespace Business
{
    public class RoleMenuModel : BaseModel<RoleMenu>, IRoleMenuModel
    {
        public List<RoleMenu> List_Cache()
        {
            var obj = CacheModel.GetCache<List<RoleMenu>>(SystemConst.Cache.RoleMenu);
            if (obj != null)
            {
                return obj;
            }
            var list = base.List().ToList();
            CacheModel.SetCache(SystemConst.Cache.RoleMenu, list);
            return list;
        }

        [Transaction]
        public void BindPermission(int? accountMainID, int roleID, int[] menuIDs, int[] menuOptionIDs, int[] serviceIDs)
        {
            var roleOptionModel = Factory.Get<IRoleOptionModel>(SystemConst.IOC_Model.RoleOptionModel);
            IMenuModel menuModel = Factory.Get<IMenuModel>(SystemConst.IOC_Model.MenuModel);
            CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            if (accountMainID != null && accountMainID > 0 && accountMainID.HasValue)
            {
                var AccountMain_ServiceModel = Factory.Get<IAccountMain_ServiceModel>(SystemConst.IOC_Model.AccountMain_ServiceModel);
                //添加相关服务功能
                AccountMain_ServiceModel.Add(accountMainID.Value, serviceIDs);
            }
            //删除原绑定的菜单
            string deleteMenuListSQL = "DELETE dbo.RoleMenu WHERE RoleID=" + roleID;
            commonModel.SqlExecute(deleteMenuListSQL);
            //绑定新菜单
            if (menuIDs != null && menuIDs.Length > 0)
            {
                StringBuilder addMenuListStringBuilder = new StringBuilder("INSERT dbo.RoleMenu ( SystemStatus ,RoleID , MenuID ) ");
                foreach (var item in menuIDs)
                {
                    addMenuListStringBuilder.Append(string.Format(" SELECT 0,{0},{1} UNION ALL", roleID, item));
                }
                var addMenuListSQL = addMenuListStringBuilder.ToString();
                addMenuListSQL = addMenuListSQL.Remove(addMenuListSQL.Length - " UNION ALL".Length);
                commonModel.SqlExecute(addMenuListSQL);
            }
            //删除原绑定的功能
            string deleteOptionListSQL = "DELETE dbo.RoleOption WHERE RoleID=" + roleID;
            commonModel.SqlExecute(deleteOptionListSQL);
            //绑定新功能
            if (menuOptionIDs != null && menuIDs != null && menuOptionIDs.Length > 0)
            {
                StringBuilder addOptionListStringBuilder = new StringBuilder("INSERT dbo.RoleOption ( SystemStatus ,RoleID , MenuOptionID ) ");
                foreach (var item in menuOptionIDs)
                {
                    addOptionListStringBuilder.Append(string.Format(" SELECT 0,{0},{1} UNION ALL", roleID, item));
                }
                var addOptionListSQL = addOptionListStringBuilder.ToString();
                addOptionListSQL = addOptionListSQL.Remove(addOptionListSQL.Length - " UNION ALL".Length);
                commonModel.SqlExecute(addOptionListSQL);
            }
            CacheModel.ClearCache(SystemConst.Cache.Menu);
            CacheModel.ClearCache(SystemConst.Cache.RoleMenu);
            CacheModel.ClearCache(SystemConst.Cache.RoleOption);
            menuModel.ReSetCache();
            ReSetCache();
            roleOptionModel.ReSetCache();
        }

        public void ReSetCache()
        {
            var list = base.List().ToList();
            CacheModel.SetCache(SystemConst.Cache.RoleMenu, list);
        }
    }
}
