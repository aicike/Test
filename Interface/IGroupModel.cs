using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IGroupModel : IBaseModel<Group>
    {
        List<Group> GetGroupListByAccountID(int accountID, int? accountMainID = null);
        /// <summary>
        /// 添加默认分组和黑名单分组
        /// </summary>
        Result AddDefaultGroup(int accountID, int accountMainID);
        Result DeleteByAccountID(int accountID);
        Result AddGroup(string groupName, int accountID, int accountMainID);
        Result EditGroup(int groupID, string groupName, int accountID, int accountMainID);
        Result DeleteGroup(int groupID, int accountID, int accountMainID);
    }
}