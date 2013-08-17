using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

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
    }
}
