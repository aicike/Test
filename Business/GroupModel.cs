using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;
using Injection.Transaction;

namespace Business
{
    public class GroupModel : BaseModel<Group>, IGroupModel
    {
        public List<Group> GetGroupListByAccountID(int accountID, int? accountMainID = null)
        {
            if (accountMainID != null && accountMainID.HasValue)
            {
                return List().Where(a => a.AccountID == accountID && a.AccountMainID == accountMainID).OrderBy(a => a.ID).ToList();
            }
            else
            {
                return List().Where(a => a.AccountID == accountID).OrderBy(a => a.ID).ToList();
            }
        }

        public Result AddDefaultGroup(int accountID, int accountMainID)
        {
            Group group = new Group();
            group.GroupName = SystemConst.Business.DefaultGroup;
            group.AccountID = accountID;
            group.AccountMainID = accountMainID;
            group.IsDefaultGroup = true;
            group.IsCanDelete = false;
            return Add(group);
        }

        [Transaction]
        public Result DeleteByAccountID(int accountID)
        {
            Result result = new Result();
            try
            {
                var groupIDs = List().Where(a => a.AccountID == accountID).Select(a => a.ID).ToList();

                //检查是否可以删除
                //User_Group
                var userGroupModel = Factory.Get<IUser_GroupModel>(SystemConst.IOC_Model.User_GroupModel);
                var isCanDelete = !userGroupModel.List().Any(a => groupIDs.Contains(a.GroupID));
                if (isCanDelete == false)
                {
                    result.Error = "该组下存在用户，无法删除";
                    return result;
                }
                CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                string deleteGroupSQL = "UPDATE dbo.[Group] SET SystemStatus=1 WHERE AccountID=" + accountID;
                commonModel.SqlExecute(deleteGroupSQL);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }


        public Result AddGroup(string groupName, int accountID, int accountMainID)
        {
            Group group = new Group();
            group.GroupName = groupName.Trim();
            group.AccountID = accountID;
            group.AccountMainID = accountMainID;
            group.IsDefaultGroup = false;
            group.IsCanDelete = true;
            var result = Add(group);
            result.Entity = group;
            return result;
        }

        public Result EditGroup(int groupID, string groupName, int accountID, int accountMainID)
        {
            var result = new Result();
            var group = Get(groupID);
            if (group.AccountID != accountID || group.AccountMainID != accountMainID)
            {
                result.Error = SystemConst.Notice.NotAuthorized;
                return result;
            }
            group.GroupName = groupName.Trim();
            group.AccountID = accountID;
            group.AccountMainID = accountMainID;
            result = Edit(group);
            result.Entity = group;
            return result;
        }

        [Transaction]
        public Result DeleteGroup(int groupID, int accountID, int accountMainID)
        {
            var result = new Result();
            var group = Get(groupID);
            if (group.AccountID != accountID || group.AccountMainID != accountMainID)
            {
                result.Error = SystemConst.Notice.NotAuthorized;
                return result;
            }
            CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            //获取该分组下所有user，并转移到未分组中
            var userIDs = group.User_Groups.Select(a => a.UserID).ToArray();
            if (userIDs.Length > 0)
            {
                //删除旧关系
                string deleteUserGroupSql = "DELETE dbo.User_Group WHERE GroupID=" + groupID;
                commonModel.SqlExecute(deleteUserGroupSql);
                //添加新关系
                int defaultGroupID = List().Where(a => a.IsDefaultGroup).SingleOrDefault().ID;
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO dbo.User_Group( SystemStatus, UserID, GroupID )");
                foreach (var item in userIDs)
                {
                    sb.AppendFormat(" SELECT  0, {0}, {1} UNION ALL", item, defaultGroupID);
                }
                string addUserGroupSql = sb.ToString();
                addUserGroupSql = addUserGroupSql.Remove(addUserGroupSql.Length - " UNION ALL".Length);
                commonModel.SqlExecute(deleteUserGroupSql);
            }
            result = CompleteDelete(groupID);
            return result;
        }
    }
}
