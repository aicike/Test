using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;

namespace Business
{
    public class AutoMessage_ReplyModel : BaseModel<AutoMessage_Reply>, IAutoMessage_ReplyModel
    {
        public AutoMessage_Reply GetByAccountID(int accountID)
        {
            return List().Where(a => a.AccountID == accountID).FirstOrDefault();
        }
    }
}
