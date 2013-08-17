using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IPendingMessagesModel : IBaseModel<PendingMessages>
    {
        string SendMessageCount(int SID);
    }
}
