﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection;
using Poco.Enum;
using Common;
using Injection.Transaction;

namespace Business
{
    public class UserModel : BaseModel<User>, IUserModel
    {
        public IQueryable<User> GetUserByAccountID(int accountID, int groupID)
        {
            return List().Where(a => a.Account_Users.Any(b => b.AccountID == accountID && b.SystemStatus == (int)EnumSystemStatus.Active && b.GroupID == groupID));
        }

        public new Result Add(User user)
        {
            user.AccountStatusID = LookupFactory.GetLookupOptionIdByToken(EnumAccountStatus.Enabled);
            return base.Add(user);
        }


    }
}
