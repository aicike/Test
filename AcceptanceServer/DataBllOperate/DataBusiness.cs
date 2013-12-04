using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Poco;
using AcceptanceServer.DataOperate;
using Poco.Enum;
using System.IO;

namespace AcceptanceServer.DataBllOperate
{
    public static class DataBusiness
    {
        /// <summary>
        /// 存储聊天记录
        /// </summary>
        /// <returns></returns>
        public static DataSet InsertChatRecord(agsXMPP.protocol.client.Message msg, NewsProtocol Np)
        {
            //存储记录
            Poco.Message Pmg = new Poco.Message();
            Pmg.TextContent = msg.Body;
            Pmg.EnumMessageTypeID = int.Parse(Np.EID);
            Pmg.FileUrl = Np.FielUrl;
            Pmg.FileLength = Np.FL;
            Pmg.voiceMP3Url = "";
            if (Np.EID == "3")//音频
            {
                Pmg.voiceMP3Url = Np.FielUrl.Substring(0, Np.FielUrl.LastIndexOf('.')) + ".mp3";
            }
            Pmg.LibraryImageTextsID = null;
            Pmg.ConversationID = int.Parse(Np.SID);
            Pmg.CType = 0;
            if(Np.ImgTextID != null)
            {
                Pmg.LibraryImageTextsID = int.Parse(Np.ImgTextID);
            }
            //售楼人员对购房者发送消息
            if (Np.MSD == ((int)EnumMessageSendDirection.Account_User).ToString())
            {
                Pmg.EnumMessageSendDirectionID = (int)EnumMessageSendDirection.Account_User;
                Pmg.FromAccountID = int.Parse(msg.From.User.Substring(1));
                Pmg.ToUserID = int.Parse(msg.To.User.Substring(1));
                Pmg.FromUserID = null;
                Pmg.ToAccountID = null;
            }
            //购房者对售楼人员发送消息
            else if (Np.MSD == ((int)EnumMessageSendDirection.User_Account).ToString())
            {
                Pmg.EnumMessageSendDirectionID = (int)EnumMessageSendDirection.User_Account;
                Pmg.FromUserID = int.Parse(msg.From.User.Substring(1));
                Pmg.ToAccountID = int.Parse(msg.To.User.Substring(1));
                Pmg.FromAccountID = null;
                Pmg.ToUserID = null;
            }
            //业务人员对业务人员发消息
            else if (Np.MSD == ((int)EnumMessageSendDirection.Account_Account).ToString())
            {
                Pmg.EnumMessageSendDirectionID = (int)EnumMessageSendDirection.Account_Account;
                Pmg.FromAccountID = int.Parse(msg.From.User.Substring(1));
                Pmg.ToAccountID = int.Parse(msg.To.User.Substring(1));
                Pmg.FromUserID = null;
                Pmg.ToUserID = null;
            }
            //用户对用户发消息
            else if (Np.MSD == ((int)EnumMessageSendDirection.User_User).ToString())
            {
                Pmg.EnumMessageSendDirectionID = (int)EnumMessageSendDirection.User_User;
                Pmg.FromUserID = int.Parse(msg.From.User.Substring(1));
                Pmg.ToUserID = int.Parse(msg.To.User.Substring(1));
                Pmg.FromAccountID = null;
                Pmg.ToAccountID = null;
            
            }
            //多人聊天
            else if (Np.MSD == ((int)EnumMessageSendDirection.GroupChat).ToString())
            {
                Pmg.CType = 1;
                Pmg.EnumMessageSendDirectionID = (int)EnumMessageSendDirection.GroupChat;
                string AOU = msg.From.User.Substring(0,1);
                if (AOU == "s")
                {
                    Pmg.FromAccountID = int.Parse(msg.From.User.Substring(1));
                    Pmg.ToUserID = null;
                    Pmg.FromUserID = null;
                    Pmg.ToAccountID = null;
                }
                else if (AOU == "u")
                {
                    Pmg.FromUserID = int.Parse(msg.From.User.Substring(1));
                    Pmg.ToUserID = null;
                    Pmg.FromAccountID = null;
                    Pmg.ToAccountID = null;
                }
            }
            return  DataHandle.InsertChatRecord(Pmg); //存储
        }


        /// <summary>
        /// 存储离线数据
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public static int InsertOffLineData(agsXMPP.protocol.client.Message msg, NewsProtocol Np,int MessageID)
        {
            PendingMessages pm = new PendingMessages();
            pm.MessageID = MessageID;
            pm.Content = msg.Body;
            pm.FileUrl = Np.FielUrl;
            pm.FileLength = Np.FL;
            pm.voiceMP3Url = "";
            if (Np.EID == "3")//音频
            {
                pm.voiceMP3Url = Np.FielUrl.Substring(0, Np.FielUrl.LastIndexOf('.')) + ".mp3";
            }
            pm.EnumMessageTypeID = int.Parse(Np.EID);
            pm.ConversationID = int.Parse(Np.SID);
            pm.LibraryImageTextsID = null;
            if (Np.ImgTextID != null)
            {
                pm.LibraryImageTextsID = int.Parse(Np.ImgTextID);
            }
            //售楼人员对购房者发送消息
            if (Np.MSD == ((int)EnumMessageSendDirection.Account_User).ToString())
            {
                pm.MSD = ((int)EnumMessageSendDirection.Account_User).ToString();
                pm.FromAccountID = int.Parse(msg.From.User.Substring(1));
                pm.ToUserID = int.Parse(msg.To.User.Substring(1));
                pm.FromUserID = null;
                pm.ToAccountID = null;
            }
            //购房者对售楼人员发送消息
            else if (Np.MSD == ((int)EnumMessageSendDirection.User_Account).ToString())
            {
                pm.MSD = ((int)EnumMessageSendDirection.User_Account).ToString();
                pm.FromUserID = int.Parse(msg.From.User.Substring(1));
                pm.ToAccountID = int.Parse(msg.To.User.Substring(1));
                pm.FromAccountID = null;
                pm.ToUserID = null;
            }

            return DataHandle.InsertOffLineData(pm); //存储
        }

        /// <summary>
        /// 获取所有消息与未读消息数量
        /// </summary>
        /// <param name="AoU"></param>
        /// <param name="AoUID"></param>
        /// <returns></returns>
        public static List<UnreadMessage> GetUnreadMessage(string AoU, string AoUID)
        {
            List<UnreadMessage> UMlist = new List<UnreadMessage>();
            DataSet ds = DataHandle.GetUnreadMessage(AoU, AoUID);
            if (ds != null)
            {
                DataTable dt = DataHandle.GetUnreadMessage(AoU, AoUID).Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    UnreadMessage um = new UnreadMessage();
                    //um.FromID = row["FromID"].ToString();
                    //um.Content = row["Content"].ToString();
                    //um.EID = row["EID"].ToString();
                    //um.SendTime = row["SendTime"].ToString();
                    //um.MessageCnt = row["Messagecnt"].ToString();
                    //um.MSD = row["MSD"].ToString();
                    //um.SID = row["SID"].ToString();
                    UMlist.Add(um);
                }
            }

            return UMlist;
        }

        /// <summary>
        /// 删除未读消息并修改消息状态
        /// </summary>
        /// <param name="SID">会话ID</param>
        /// <param name="AoU">s == u</param>
        /// <param name="ToUID">用户ID</param>
        /// <returns></returns>
        public static int UpandDelMessType(int SID, string AoU, int ToUID)
        {
            return DataHandle.UpandDelMessType(SID,AoU,ToUID);
        }
        /// <summary>
        /// 获取一个会话ID中的所有用户
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static DataTable GetSidUser(int SID)
        {
            return DataHandle.GetSidUser(SID);
        }



        /// <summary>
        /// 存储群发时 用户不在线处理
        /// </summary>
        /// <param name="mgc"></param>
        /// <returns></returns>
        public static int InsertMessageGroupChat(int MessageID,int UserID,int UserType,int SID)
        {
            MessageGroupChat mgc = new MessageGroupChat();
            mgc.MessageID = MessageID;
            mgc.UserID = UserID;
            mgc.UserType = UserType;
            mgc.SID = SID;

            return DataHandle.InsertMessageGroupChat(mgc);

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


            return DataHandle.DelMessGroupChat(userID, UserType, SID);
        }


        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        public static void SetLog(Exception ex)
        {
            try
            {


                string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string filePath = Environment.CurrentDirectory + "\\log";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                FileStream aFile = new FileStream(filePath + "\\" + fileName, FileMode.Append);
                StreamWriter sw = new StreamWriter(aFile);
                sw.WriteLine("*************************异常文本****************************");
                sw.WriteLine("【出现时间】：" + DateTime.Now.ToString());
                if (ex != null)
                {
                    sw.WriteLine("【异常类型】：" + ex.GetType().Name);
                    sw.WriteLine("【异常信息】：" + ex.Message);
                    sw.WriteLine("【堆栈调用】：" + ex.StackTrace);
                }
                else
                {
                    sw.WriteLine("【未处理异常】：" + ex.StackTrace);
                }
                sw.WriteLine("************************************************************");
                sw.Close();
                aFile.Close();
            }
            catch { }
        }

    }
}
