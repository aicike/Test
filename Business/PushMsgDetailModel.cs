using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Poco.Enum;

namespace Business
{
    public class PushMsgDetailModel : BaseModel<PushMsgDetail>, IPushMsgDetailModel
    {
        [Transaction]
        public Result AddPushMsgDetail(int pushMsgID, Poco.Enum.EnumClientUserType userType, List<int> userID)
        {
            Result result = new Result();
            if (userID == null || userID.Count == 0)
            {
                result.Error = "没有发送对象，操作终止。";
                return result;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [Aicike].[dbo].[PushMsgDetail] ([SystemStatus],[PushMsgID],[EnumClientUserType],[ReceiveID],[ReceiveTime],[EnumReceiveStatus]) ");
            foreach (var item in userID)
            {
                sb.AppendFormat("SELECT 0,{0},{1},{2},NULL,{3} UNION ALL ", pushMsgID, (int)userType, item, (int)EnumReceiveStatus.NotReceive);
            }
            string sql = sb.Remove(sb.Length - "UNION ALL ".Length, "UNION ALL ".Length).ToString();
            base.SqlExecute(sql);
            return result;
        }
    }
}
