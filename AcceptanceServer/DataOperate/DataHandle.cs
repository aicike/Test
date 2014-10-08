using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Data;
using System.Data.SqlClient;

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
            string sql = @"insert into [Message](SystemStatus,TextContent,EnumMessageSendDirectionID,EnumMessageTypeID,FromAccountID,FromUserID,ToAccountID,ToUserID,IsReceive,SendTime,ReceiveTime,fileUrl,LibraryImageTextsID,ConversationID,voiceMP3Url,FileLength,Ctype) 
                                         values(0,@TextContent,@EnumMessageSendDirectionID,@EnumMessageTypeID,@FromAccountID,@FromUserID,@ToAccountID,@ToUserID,'false',getdate(),getdate(),@fileUrl,@LibraryImageTextsID,@ConversationID,@voiceMP3Url,@FileLength,@Ctype) select @@IDENTITY as TID";
            SqlParameter[] sp = new SqlParameter[]{
                new SqlParameter("@TextContent",mg.TextContent == null?"" :mg.TextContent),
                new SqlParameter("@EnumMessageSendDirectionID",mg.EnumMessageSendDirectionID),
                new SqlParameter("@EnumMessageTypeID",mg.EnumMessageTypeID),
                new SqlParameter("@FromAccountID", mg.FromAccountID == null ? null : mg.FromAccountID.Value.ToString()),
                new SqlParameter("@FromUserID", mg.FromUserID == null ? null : mg.FromUserID.Value.ToString()),
                new SqlParameter("@ToAccountID",mg.ToAccountID == null ? null : mg.ToAccountID.Value.ToString()),
                new SqlParameter("@ToUserID",mg.ToUserID == null ? null : mg.ToUserID.Value.ToString()),
                new SqlParameter("@fileUrl",mg.FileUrl),
                new SqlParameter("@LibraryImageTextsID",mg.LibraryImageTextsID == null?null:mg.LibraryImageTextsID.ToString()),
                new SqlParameter("@ConversationID",mg.ConversationID),
                new SqlParameter("@voiceMP3Url",mg.voiceMP3Url),
                new SqlParameter("@FileLength",mg.FileLength),
                new SqlParameter("@Ctype",mg.CType)
                
            };

            try
            {
                //web页面通知
                if (mg.ToAccountID.HasValue && mg.ToAccountID.Value != 0)
                {
                    string sql_webNotice_sel = "select count,AccountMainID from WebNotice where menuID=40 and accountmainID=(SELECT TOP 1 AccountMainID FROM dbo.Account_AccountMain where AccountID=" + mg.ToAccountID.Value + ")";

                    var data = SqlHelper.ExecuteDataset(sql_webNotice_sel, sp);

                    int NoticeCount = 0;
                    if (data.Tables[0] != null && data.Tables[0].Rows.Count > 0)
                    {
                        NoticeCount = Convert.ToInt32(data.Tables[0].Rows[0][0].ToString()) + 1;
                        int AccountMainID = Convert.ToInt32(data.Tables[0].Rows[0][1].ToString());
                        //修改
                        string sql_webNotice_edit = " UPDATE dbo.WebNotice SET Count=" + NoticeCount + " WHERE MenuID=40 AND AccountMainID=" + AccountMainID;
                        SqlHelper.ExecuteNonQuery(sql_webNotice_edit);
                    }
                    else
                    {
                        //新增
                        string sql_webNotice_add = "INSERT INTO dbo.WebNotice ( SystemStatus ,MenuID ,Count ,AccountMainID) VALUES  ( 0 ,40 ,1 ,(SELECT TOP 1 AccountMainID FROM dbo.Account_AccountMain WHERE AccountID=" + mg.ToAccountID.Value + "))";
                        SqlHelper.ExecuteNonQuery(sql_webNotice_add);
                    }
                }
            }
            catch (Exception)
            {
                
            }
            return SqlHelper.ExecuteDataset(sql, sp);
        }

        /// <summary>
        /// 存储群发时 用户不在线处理
        /// </summary>
        /// <param name="mgc"></param>
        /// <returns></returns>
        public static int InsertMessageGroupChat(MessageGroupChat mgc)
        {
            string sql = string.Format("insert into MessageGroupChat(systemStatus,MessageID,UserID,UserType,SID) values(0,{0},{1},{2},{3})", mgc.MessageID, mgc.UserID, mgc.UserType, mgc.SID);

            return SqlHelper.ExecuteNonQuery(sql);

        }

        /// <summary>
        /// 获取一个会话ID中的所有用户
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static DataTable GetSidUser(int sid)
        {
            string sql = string.Format("select case when UserType = 1 then 's'+Convert(varchar(5),UserID) when UserType = 2 then 'u'+Convert(varchar(5),UserID) end as juid ,userID,UserType from dbo.ConversationDetailed a where ConversationID={0}", sid);
            DataTable dt = SqlHelper.ExecuteDataset(sql).Tables[0];

            return dt;
        }

        /// <summary>
        /// 存储离线数据
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public static int InsertOffLineData(PendingMessages pm)
        {
            string sql = @"insert into PendingMessages(SystemStatus,FromAccountID,FromUserID,ToAccountID,ToUserID,SendTime,Content,MessageID,EnumMessageTypeID,MSD,fileUrl,LibraryImageTextsID,ConversationID,voiceMP3Url,FileLength) 
                            values(0,@FromAccountID,@FromUserID,@ToAccountID,@ToUserID,getdate(),@Content,@MessageID,@EnumMessageTypeID,@MSD,@fileUrl,@LibraryImageTextsID,@ConversationID,@voiceMP3Url,@FileLength)";
            SqlParameter[] sp = new SqlParameter[]{
                 new SqlParameter("@FromAccountID",pm.FromAccountID == null ? null: pm.FromAccountID.Value.ToString()),
                 new SqlParameter("@FromUserID", pm.FromUserID == null ? null : pm.FromUserID.Value.ToString()),
                 new SqlParameter("@ToAccountID",pm.ToAccountID == null ? null : pm.ToAccountID.Value.ToString()),
                 new SqlParameter("@ToUserID", pm.ToUserID == null ? null : pm.ToUserID.Value.ToString()),
                 new SqlParameter("@Content",pm.Content==null?"":pm.Content),
                 new SqlParameter("@MessageID",pm.MessageID),
                 new SqlParameter("@EnumMessageTypeID",pm.EnumMessageTypeID),
                 new SqlParameter("@MSD",pm.MSD),
                 new SqlParameter("@fileUrl",pm.FileUrl),
                 new SqlParameter("@LibraryImageTextsID",pm.LibraryImageTextsID == null ? null : pm.LibraryImageTextsID.ToString()),
                 new SqlParameter("@ConversationID",pm.ConversationID),
                 new SqlParameter("@voiceMP3Url",pm.voiceMP3Url),
                 new SqlParameter("@FileLength",pm.FileLength)
            };


            return SqlHelper.ExecuteNonQuery(sql, sp);
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
                    sql = string.Format(@"select ConversationID as [SID],Max(SendTime) as SendTime,
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
        public static DataSet GetUserConversationID(string AoU, string UID)
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
        public static int UpandDelMessType(int SID, string AoU, int ToUID)
        {
            string sql = "";
            if (AoU == "s")
            {
                sql = string.Format("update [Message] set IsReceive = 'true' where ConversationID={0} and IsReceive='false' and ToAccountID= {1}", SID, ToUID);
            }
            else
            {
                sql = string.Format("update [Message] set IsReceive = 'true' where ConversationID={0} and IsReceive='false' and ToUserID= {1} ", SID, ToUID);
            }
            return SqlHelper.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除未读消息( 多人)
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="UserType"></param>
        /// <param name="SID"></param>
        /// <returns></returns>
        public static int DelMessGroupChat(int userID, int UserType, int SID)
        {
            string sql = string.Format("delete MessageGroupChat where SID = {0} and UserID={1} and UserType={2}", SID, userID, UserType);

            return SqlHelper.ExecuteNonQuery(sql);
        }
    }
}
