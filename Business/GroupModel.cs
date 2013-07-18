using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;
using Injection.Transaction;
using Poco.Enum;

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
                //检查是否可以删除
                //User_Group
                var account_UserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
                var isCanDelete = !account_UserModel.List().Any(a => a.AccountID == accountID && a.SystemStatus == (int)EnumSystemStatus.Active);
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
            //更改用户分组
            int defaultGroupID =GetGroupListByAccountID(accountID,accountMainID).Where(a => a.IsDefaultGroup).SingleOrDefault().ID;
            string sql = string.Format("UPDATE dbo.Account_User SET groupID={0} WHERE GroupID={1}", defaultGroupID, groupID);
            commonModel.SqlExecute(sql);
            result = CompleteDelete(groupID);
            return result;
        }
    }
}
