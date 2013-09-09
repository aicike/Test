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
            string sql = string.Format(@"insert into [Message](SystemStatus,TextContent,EnumMessageSendDirectionID,EnumMessageTypeID,FromAccountID,FromUserID,ToAccountID,ToUserID,IsReceive,SendTime,ReceiveTime,fileUrl,LibraryImageTextsID,ConversationID) 
                                         values({0},'{1}',{2},{3},{4},{5},{6},{7},'false',getdate(),getdate(),'{8}',{9},{10}) select @@IDENTITY as TID",
                                        0, mg.TextContent, mg.EnumMessageSendDirectionID, mg.EnumMessageTypeID,
                                        mg.FromAccountID == null ? "null" : mg.FromAccountID.Value.ToString(),
                                        mg.FromUserID == null ? "null" : mg.FromUserID.Value.ToString(),
                                        mg.ToAccountID == null ? "null" : mg.ToAccountID.Value.ToString(),
                                        mg.ToUserID == null ? "null" : mg.ToUserID.Value.ToString(),
                                        mg.FileUrl,mg.LibraryImageTextsID == null?"null":mg.LibraryImageTextsID.ToString(),
                                        mg.ConversationID
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
            string sql = string.Format("insert into PendingMessages(SystemStatus,FromAccountID,FromUserID,ToAccountID,ToUserID,SendTime,Content,MessageID,EnumMessageTypeID,MSD,fileUrl,LibraryImageTextsID,ConversationID) values(0,{0},{1},{2},{3},getdate(),'{4}',{5},{6},'{7}','{8}',{9},{10})",
                                        pm.FromAccountID == null ? "null" : pm.FromAccountID.Value.ToString(),
                                        pm.FromUserID == null ? "null" : pm.FromUserID.Value.ToString(),
                                        pm.ToAccountID == null ? "null" : pm.ToAccountID.Value.ToString(),
                                        pm.ToUserID == null ? "null" : pm.ToUserID.Value.ToString(),
                                        pm.Content,pm.MessageID,pm.EnumMessageTypeID,pm.MSD,pm.FileUrl,
                                        pm.LibraryImageTextsID == null ? "null" : pm.LibraryImageTextsID.ToString(),pm.ConversationID);

            return SqlHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 获取所有消息与未读消息数量
        /// </summary>
        /// <param name="AoU"></param>
        /// <param name="AoUID"></param>
        /// <returns></returns>
        public static DataSet GetUnreadMessage(string AoU, string AoUID)
        {
            //获取用户会话ID
            DataTable dt = GetUserConversationID(AoU, AoUID).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string id = "";
                foreach (DataRow row in dt.Rows)
                {
                    id += row["ID"] + ",";
                }
                id = id.TrimEnd(',');
                string sql = "";
                //用户

                if (AoU == "u")
                {
                    sql = string.Format(@"select ConversationID as [SID],Max(SendTime) as SendTime,
                                    (select count(isreceive)  from dbo.[Message] where ConversationID in ({0}) and ToUserID ={1} and IsReceive='false' ) as Messagecnt,
                                    (select  CASE WHEN fromaccountid <> 0 THEN fromaccountid ELSE fromuserid END  from dbo.[Message] where SendTime = Max(a.SendTime) ) as FromID,
                                    (select TextContent  from dbo.[Message] where SendTime = Max(a.SendTime)) as Content,
                                    (select EnumMessageSendDirectionID  from dbo.[Message] where SendTime = Max(a.SendTime)) as MSD,
                                    (select EnumMessageTypeID  from dbo.[Message] where SendTime = Max(a.SendTime)) as EID
                                    from dbo.[Message] a  where ConversationID in ({0}) and ToUserID ={1} group by ConversationID"
                                        , id, AoUID);
                }
                //售楼代表
                else
                {
                    sql = string.Format(@"seselect ConversationID as [SID],Max(SendTime) as SendTime,
                                    (select count(isreceive)  from dbo.[Message] where ConversationID in ({0}) and ToAccountID ={1} and IsReceive='false' ) as Messagecnt,
                                    (select  CASE WHEN fromaccountid <> 0 THEN fromaccountid ELSE fromuserid END  from dbo.[Message] where SendTime = Max(a.SendTime) ) as FromID,
                                    (select TextContent  from dbo.[Message] where SendTime = Max(a.SendTime)) as Content,
                                    (select EnumMessageSendDirectionID  from dbo.[Message] where SendTime = Max(a.SendTime)) as MSD,
                                    (select EnumMessageTypeID  from dbo.[Message] where SendTime = Max(a.SendTime)) as EID
                                    from dbo.[Message] a  where ConversationID in ({0}) and ToAccountID ={1} group by ConversationID"
                                        , id, AoUID);
                }

                return SqlHelper.ExecuteDataset(sql);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取用户所有会话ID
        /// </summary>
        /// <param name="AoU"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        public static DataSet GetUserConversationID(string AoU ,string UID)
        {
            string sql = "";
            if (AoU == "s")
            {
                sql = string.Format("select ID from dbo.[Conversation] where (ctype='0' and User1ID='{0}') or (ctype='1' and User1ID='{0}') or(ctype ='1' and User2ID = '{0}')", UID);
            }
            else
            {
                sql = string.Format("select ID from dbo.[Conversation] where (ctype='0' and User2ID='{0}') or (ctype='2' and User1ID='{0}') or(ctype ='2' and User2ID = '{0}')", UID);
            }
            return SqlHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 删除未读消息并修改消息状态
        /// </summary>
        /// <param name="SID"></param>
        /// <param name="AoU"></param>
        /// <param name="ToUID"></param>
        /// <returns></returns>
        public static int UpandDelMessType(int SID,string AoU, int ToUID)
        {
            string sql = "";
            if (AoU == "s")
            {
                sql = string.Format("update [Message] set IsReceive = 'true' where ConversationID={0} and IsReceive='false' and ToAccountID= {1} "
                                    + "delete dbo.PendingMessages where conversationID={0} and ToAccountID ={1}", SID, ToUID);
            }
            else
            {
                sql = string.Format("update [Message] set IsReceive = 'true' where ConversationID={0} and IsReceive='false' and ToUserID= {1} "
                                    + "delete dbo.PendingMessages where conversationID={0} and ToUserID ={1}", SID, ToUID);
            }
            return SqlHelper.ExecuteNonQuery(sql);
        }
    }
}
