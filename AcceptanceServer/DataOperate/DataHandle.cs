using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Data;

namespace AcceptanceServer.DataOperate
{
    public static class DataHandle
    {
        /// <summary>
        /// 存储聊天记录
        /// </summary>
        /// <returns></returns>
        public static DataSet InsertChatRecord(Message mg)
        {
            string sql = string.Format(@"insert into [Message](SystemStatus,TextContent,EnumMessageSendDirectionID,EnumMessageTypeID,FromAccountID,FromUserID,ToAccountID,ToUserID,IsReceive,SendTime,ReceiveTime) 
                                         values({0},'{1}',{2},{3},{4},{5},{6},{7},'false',getdate(),getdate()) select @@IDENTITY as TID",
                                        0, mg.TextContent, mg.EnumMessageSendDirectionID, mg.EnumMessageTypeID,
                                        mg.FromAccountID == null ? "null" : mg.FromAccountID.Value.ToString(),
                                        mg.FromUserID == null ? "null" : mg.FromUserID.Value.ToString(),
                                        mg.ToAccountID == null ? "null" : mg.ToAccountID.Value.ToString(),
                                        mg.ToUserID == null ? "null" : mg.ToUserID.Value.ToString()
                                       );

            return SqlHelper.ExecuteDataset(sql);
        }


        /// <summary>
        /// 存储离线数据
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public static int InsertOffLineData(PendingMessages pm)
        {
            string sql = string.Format("insert into PendingMessages(SystemStatus,FromAccountID,FromUserID,ToAccountID,ToUserID,SendTime,Content,MessageID,EnumMessageTypeID,MSD) values(0,{0},{1},{2},{3},getdate(),'{4}',{5},{6},'{7}')",
                                        pm.FromAccountID == null ? "null" : pm.FromAccountID.Value.ToString(),
                                        pm.FromUserID == null ? "null" : pm.FromUserID.Value.ToString(),
                                        pm.ToAccountID == null ? "null" : pm.ToAccountID.Value.ToString(),
                                        pm.ToUserID == null ? "null" : pm.ToUserID.Value.ToString(),
                                        pm.Content,pm.MessageID,pm.EnumMessageTypeID,pm.MSD);

            return SqlHelper.ExecuteNonQuery(sql);
        }
    }
}
