using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IWebNoticeModel : IBaseModel<WebNotice>
    {
        Result Add(int menuID, int accountMainID);
        List<int> GetMenuIDByAccountMainID(int accountMainID);
        void ClearWebNotice(int accountMainID, string menuToken);
        int GetMenuIDByToken(string token);
    }
}
