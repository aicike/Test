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
        [Transaction]
        public void BindPermission(int roleID, int[] menuIDs, int[] menuOptionIDs)
        {
            CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
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
            if (menuIDs != null && menuOptionIDs.Length > 0)
            {
                var roleOptionModel = Factory.Get<IRoleOptionModel>(SystemConst.IOC_Model.RoleOptionModel);
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
        }
    }
}
