using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAutoMessage_ReplyModel : IBaseModel<AutoMessage_Reply>
    {
        AutoMessage_Reply GetByAccountID(int accountID);
    }
}
