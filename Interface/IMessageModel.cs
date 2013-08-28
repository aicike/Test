using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IMessageModel :IBaseModel<Message>
    {

        IQueryable<Message> GetList(int SID,int UID);

        IQueryable<Message> GetHistoryList(int SID, int UID);

        int UpAndDelData(int SID, int UID);

    }
}
