using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;
using Poco.Enum;

namespace Interface
{
    public interface IAccount_UserModel : IBaseModel<Account_User>
    {
        Result ChangeGroup(int userID, int accountID, int groupID);

        bool ChickUserInAccount(int SID, int UID);

        /// <summary>
        /// 用户和Account关系绑定
        /// </summary>
        Result BindUser_Account(int accountID, int userID);

        /// <summary>
        /// 修改用户所属的销售代表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="AmiAccountID">目标代表ID</param>
        /// <param name="groupID">目标代表默认分组</param>
        /// <returns></returns>
        int UpdUserTooAccount(int userID, int AmiAccountID, int groupID);

        int GetBindAccountID(int userID, int accountMainID);
    }
}