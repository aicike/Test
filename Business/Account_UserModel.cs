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
    }
}
