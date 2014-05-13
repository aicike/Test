using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class MessageGroupChatModel : BaseModel<MessageGroupChat>, IMessageGroupChatModel
    {
        public int GetMessageGroupCnt(int UserID, int UserType)
        {
            return List().Where(a => a.UserID == UserID && a.UserType == UserType).Count();
        }
    }
}
