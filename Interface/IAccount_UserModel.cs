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
        Result ChangeGroup(int userID,int accountID, int groupID);


        bool ChickUserInAccount(int SID, int UID);
    }
}