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
    public class SystemUserRole_SystemUserMenuModel : BaseModel<SystemUserRole_SystemUserMenu>, ISystemUserRole_SystemUserMenuModel
    {
        [Transaction]
        public void BindPermission(int systemUserRoleID, int[] systemUserMenuIDs, int[] sytemUserMenuOptionIDs)
        {
            CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            //删除原绑定的菜单
            string deleteMenuListSQL = "DELETE dbo.SystemUserRole_SystemUserMenu WHERE SystemUserRoleID=" + systemUserRoleID;
            commonModel.SqlExecute(deleteMenuListSQL);
            //绑定新菜单
            if (systemUserMenuIDs != null && systemUserMenuIDs.Length > 0)
            {
                StringBuilder addMenuListStringBuilder = new StringBuilder("INSERT dbo.SystemUserRole_SystemUserMenu ( SystemStatus ,SystemUserRoleID , SystemUserMenuID ) ");
                foreach (var item in systemUserMenuIDs)
                {
                    addMenuListStringBuilder.Append(string.Format(" SELECT 0,{0},{1} UNION ALL", systemUserRoleID, item));
                }
                var addMenuListSQL = addMenuListStringBuilder.ToString();
                addMenuListSQL = addMenuListSQL.Remove(addMenuListSQL.Length - " UNION ALL".Length);
                commonModel.SqlExecute(addMenuListSQL);
            }
            //删除原绑定的功能
            string deleteOptionListSQL = "DELETE dbo.SystemUserRole_Option WHERE SystemUserRoleID=" + systemUserRoleID;
            commonModel.SqlExecute(deleteOptionListSQL);
            //绑定新功能
            if (systemUserMenuIDs != null && sytemUserMenuOptionIDs.Length > 0)
            {
                var systemUserRole_OptionModel = Factory.Get<ISystemUserRole_OptionModel>(SystemConst.IOC_Model.SystemUserRole_OptionModel);
                StringBuilder addOptionListStringBuilder = new StringBuilder("INSERT dbo.SystemUserRole_Option ( SystemStatus ,SystemUserRoleID , SystemUserMenuOptionID ) ");
                foreach (var item in sytemUserMenuOptionIDs)
                {
                    addOptionListStringBuilder.Append(string.Format(" SELECT 0,{0},{1} UNION ALL", systemUserRoleID, item));
                }
                var addOptionListSQL = addOptionListStringBuilder.ToString();
                addOptionListSQL = addOptionListSQL.Remove(addOptionListSQL.Length - " UNION ALL".Length);
                commonModel.SqlExecute(addOptionListSQL);
            }
            CacheModel.ClearCache(SystemConst.Cache.SystemUserMenu);
        }
    }
}
