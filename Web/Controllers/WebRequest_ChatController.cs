using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.WebAPI_Poco;
using Poco.Enum;

namespace Web.Controllers
{
    public class WebRequest_ChatController : Controller
    {
        /// <summary>
        /// 获取用户的聊天记录
        /// </summary>
        public string GetMessageList(int currentUserType, int accountID, int userID, int pageSize, int lastMessageID)
        {
            var messageModel = Factory.Get<IMessageModel>(SystemConst.IOC_Model.MessageModel);
            List<Message> list = null;
            if (lastMessageID <= 0)
            {
                list = messageModel.GetList(accountID, userID).Take(pageSize).ToList();
            }
            else
            {
                list = messageModel.GetList(accountID, userID).Where(a => a.ID < lastMessageID).Take(pageSize).ToList();
            }
            List<App_Chat> chats = new List<App_Chat>();
            var userType = (EnumClientUserType)currentUserType;//当前用户身份的类型
            foreach (var item in list)
            {
                App_Chat chat = new App_Chat();
                chat.ID = item.ID;
                chat.SendTime = item.SendTime.ToString(SystemConst.Business.TimeFomatFull);
                chat.Content = item.TextContent;//todo 
                chat.Type = item.EnumMessageTypeID;
                //发送方向：接收 或 发送
                switch (userType)
                {
                    case EnumClientUserType.Account:
                        if (item.FromAccountID.HasValue && item.FromAccountID.Value > 0)
                        {
                            chat.SendDirection = (int)EnumMessageSend.To;
                        }
                        else
                        {
                            chat.SendDirection = (int)EnumMessageSend.From;
                        }
                        break;
                    case EnumClientUserType.User:
                        if (item.FromUserID.HasValue && item.FromUserID.Value > 0)
                        {
                            chat.SendDirection = (int)EnumMessageSend.To;
                        }
                        else
                        {
                            chat.SendDirection = (int)EnumMessageSend.From;
                        }
                        break;
                }
                chat.IsRead = item.IsReceive;
                chats.Add(chat);
            }
            Result result = new Result();
            result.Entity = chats;
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        public string UpLodeFiles(int Type, string FileName, string FileBuffer)
        {
            return "";
        }

    }
}
