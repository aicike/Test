using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IMessageGroupChatModel : IBaseModel<MessageGroupChat>
    {
        int GetMessageGroupCnt(int UserID, int UserType);
    }
}
