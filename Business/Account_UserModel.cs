using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;

namespace Business
{
    public class Account_UserModel : BaseModel<Account_User>, IAccount_UserModel
    {
        public Result ChangeGroup(int userID, int accountID, int groupID)
        {
            var entity = List().Where(a => a.UserID == userID && a.AccountID == accountID).FirstOrDefault();
            if (entity == null)
            {
                throw new ApplicationException(SystemConst.Notice.NotAuthorized);
            }
            entity.GroupID = groupID;
            return Edit(entity);
        }

        /// <summary>
        /// 判断当前用户是否属于该销售代表
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        public bool ChickUserInAccount(int SID, int UID)
        {
            var axcountUser = List().Where(a => a.AccountID == SID && a.UserID == UID);
            if (axcountUser == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 用户和Account关系绑定
        /// </summary>
        public Result BindUser_Account(int accountID, int userID)
        {
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = accountModel.Get(accountID);
            var group = account.Groups.Where(a => a.IsDefaultGroup == true).FirstOrDefault();
            Account_User accountUser = new Account_User();
            accountUser.AccountID = accountID;
            accountUser.UserID = userID;
            accountUser.GroupID = group.ID;
            return Add(accountUser);
        }
    }
}
