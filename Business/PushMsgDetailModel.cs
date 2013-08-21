using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Poco.Enum;
using Injection;

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
            sb.Append("INSERT INTO dbo.PushMsgDetail ( SystemStatus ,PushMsgID ,EnumClientUserType ,ReceiveID ,EnumReceiveStatus) ");
            foreach (var item in userID)
            {
                sb.AppendFormat(" SELECT 0,{0},{1},{2},{3} UNION ALL", pushMsgID, (int)userType, item, (int)EnumReceiveStatus.NotReceive);
            }
            string sql = sb.Remove(sb.Length - "UNION ALL".Length, "UNION ALL".Length).ToString();
            var comModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            comModel.SqlExecute(sql);
            return result;
        }

        /// <summary>
        /// 设置推送消息已读
        /// </summary>
        public void SetPushStatusToRead(int pushID, EnumClientUserType enumClientUserType, string clientID)
        {
            string sql = String.Format("UPDATE dbo.PushMsgDetail SET ReceiveTime =GETDATE() , EnumReceiveStatus={0} WHERE PushMsgID={1} AND EnumClientUserType={2} AND ReceiveID=(SELECT EntityID FROM dbo.ClientInfo WHERE ClientID='{3}')",
               (int)EnumReceiveStatus.Receive, pushID, (int)enumClientUserType, clientID);
            var comModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            comModel.SqlExecute(sql);
        }

        /// <summary>
        /// 获取未读的推送消息
        /// </summary>
        public List<PushMsgDetail> GetNotRecivePushMessage(int accountMainID, EnumClientUserType enumClientUserType, string clientID)
        {
            int ecut = LookupFactory.GetLookupOptionIdByToken(enumClientUserType);
            string sql = "SELECT a.* FROM dbo.PushMsgDetail a INNER JOIN dbo.PushMsg b ON a.PushMsgID = b.ID INNER JOIN dbo.ClientInfo c ON a.ReceiveID = c.EntityID WHERE " +
            string.Format("c.ClientID='{0}' AND c.EnumClientUserTypeID={1} AND b.AccountMainID={2} AND a.EnumReceiveStatus={3} ORDER BY a.ID",
            clientID, ecut, accountMainID, (int)EnumReceiveStatus.NotReceive);
            var list = base.SqlQuery(sql).ToList();
            if (list != null && list.Count > 0)
            {
                var ids = list.Select(a => a.ID).ToList();
                list = List().Where(a => ids.Contains(a.ID)).OrderBy(a=>a.ID).ToList();
                //设置消息已读
                StringBuilder id_str = new StringBuilder();
                int length = ids.Count();
                for (int i = 0; i < length; i++)
                {
                    if (i + 1 >= length)
                    {
                        id_str.Append(ids[i]);
                    }
                    else
                    {
                        id_str.AppendFormat("{0},",ids[i]);
                    }
                }
                string sql2 = string.Format("UPDATE dbo.PushMsgDetail SET ReceiveTime = GETDATE() , EnumReceiveStatus={0} WHERE ID in ({1})", (int)EnumReceiveStatus.Receive, id_str.ToString());
                var comModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                comModel.SqlExecute(sql2);
            }
            return list;
        }
    }
}
