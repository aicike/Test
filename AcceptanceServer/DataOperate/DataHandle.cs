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
            string sql = string.Format(@"insert into [Message](SystemStatus,TextContent,EnumMessageSendDirectionID,EnumMessageTypeID,FromAccountID,FromUserID,ToAccountID,ToUserID,IsReceive,SendTime,ReceiveTime,fileUrl,LibraryImageTextsID) 
                                         values({0},'{1}',{2},{3},{4},{5},{6},{7},'false',getdate(),getdate(),'{8}',{9}) select @@IDENTITY as TID",
                                        0, mg.TextContent, mg.EnumMessageSendDirectionID, mg.EnumMessageTypeID,
                                        mg.FromAccountID == null ? "null" : mg.FromAccountID.Value.ToString(),
                                        mg.FromUserID == null ? "null" : mg.FromUserID.Value.ToString(),
                                        mg.ToAccountID == null ? "null" : mg.ToAccountID.Value.ToString(),
                                        mg.ToUserID == null ? "null" : mg.ToUserID.Value.ToString(),
                                        mg.FileUrl,mg.LibraryImageTextsID == null?"null":mg.LibraryImageTextsID.ToString()
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
            string sql = string.Format("insert into PendingMessages(SystemStatus,FromAccountID,FromUserID,ToAccountID,ToUserID,SendTime,Content,MessageID,EnumMessageTypeID,MSD,fileUrl,LibraryImageTextsID) values(0,{0},{1},{2},{3},getdate(),'{4}',{5},{6},'{7}','{8}',{9})",
                                        pm.FromAccountID == null ? "null" : pm.FromAccountID.Value.ToString(),
                                        pm.FromUserID == null ? "null" : pm.FromUserID.Value.ToString(),
                                        pm.ToAccountID == null ? "null" : pm.ToAccountID.Value.ToString(),
                                        pm.ToUserID == null ? "null" : pm.ToUserID.Value.ToString(),
                                        pm.Content,pm.MessageID,pm.EnumMessageTypeID,pm.MSD,pm.FileUrl,
                                        pm.LibraryImageTextsID == null ? "null" : pm.LibraryImageTextsID.ToString());

            return SqlHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取未读消息
        /// </summary>
        /// <param name="AoU"></param>
        /// <param name="AoUID"></param>
        /// <returns></returns>
        public static DataSet GetUnreadMessage(string AoU, string AoUID)
        {
            string sql = "";
            //用户
            if (AoU == "u")
            {
                sql = string.Format(@"select a.FromAccountID,max(a.sendTime) as SendTime,count(a.touserid) as cnt,
                                (select EnumMessageTypeID from PendingMessages where touserid= {0} and sendTime =max(a.sendtime)) as EID,
                                (select Content from PendingMessages where touserid= {0} and sendTime =max(a.sendtime)) as Content
                                 from PendingMessages a where a.toUserID = {0} group by a.FromAccountID", AoUID);
            }
            //售楼代表
            else
            {
                sql = string.Format(@"select a.fromUserID,max(a.sendTime) as SendTime,count(a.toaccountid) as cnt,
                                    (select EnumMessageTypeID from PendingMessages where toaccountid= {0} and sendTime =max(a.sendtime)) as EID,
                                    (select Content from PendingMessages where toaccountid= {0} and sendTime =max(a.sendtime)) as Content
                                     from PendingMessages a where a.toaccountid = {0} group by a.fromUserID", AoUID);
            }

            return SqlHelper.ExecuteDataset(sql);
        
        }
    }
}
