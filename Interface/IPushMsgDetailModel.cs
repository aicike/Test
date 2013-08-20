using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Poco.Enum;

namespace Interface
{
    public interface IPushMsgDetailModel : IBaseModel<PushMsgDetail>
    {
        Result AddPushMsgDetail(int pushMsgID, EnumClientUserType userType, List<int> userID);
    }
}
