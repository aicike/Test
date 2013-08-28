﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Poco;
using AcceptanceServer.DataOperate;
using Poco.Enum;

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
            Pmg.LibraryImageTextsID = null;
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
            pm.EnumMessageTypeID = int.Parse(Np.EID);
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
        /// 获取未读消息
        /// </summary>
        /// <param name="AoU"></param>
        /// <param name="AoUID"></param>
        /// <returns></returns>
        public static List<UnreadMessage> GetUnreadMessage(string AoU, string AoUID)
        {
            List<UnreadMessage> UMlist = new List<UnreadMessage>();
            DataTable dt = DataHandle.GetUnreadMessage(AoU,AoUID).Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                UnreadMessage um = new UnreadMessage();
                if (AoU == "u")
                {
                    um.FromAccountID = row["FromAccountID"].ToString();
                    um.FromUserID = "";
                }
                else
                {
                    um.FromAccountID = "";
                    um.FromUserID = row["fromUserID"].ToString();
                }
                um.Content = row["Content"].ToString();
                um.EID = row["EID"].ToString();
                um.SendTime = row["SendTime"].ToString();
                um.SendCnt = row["cnt"].ToString();

                UMlist.Add(um);
            }

            return UMlist;
        }

    }
}
