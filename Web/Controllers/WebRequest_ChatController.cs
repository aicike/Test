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
using System.Configuration;

namespace Web.Controllers
{
    public class WebRequest_ChatController : Controller
    {
        /// <summary>
        /// 获取用户的聊天记录
        /// </summary>
        public string GetMessageList(int currentUserType,int sessionID, int pageSize, int lastMessageID)
        {
            var messageModel = Factory.Get<IMessageModel>(SystemConst.IOC_Model.MessageModel);
            List<Message> list = null;
            if (lastMessageID <= 0)
            {
                list = messageModel.GetList(sessionID).Take(pageSize).ToList();
            }
            else
            {
                list = messageModel.GetList(sessionID).Where(a => a.ID < lastMessageID).Take(pageSize).ToList();
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

        /// <summary>
        /// 查找或创建会话ID
        /// </summary>
        /// <param name="AccountMainID">售楼部ID</param>
        /// <param name="AID">售楼用户ID 当Ctype为2时 此字段存用户ID</param>
        /// <param name="UID">用户ID 当Ctype为1时 此字段存售楼用户ID</param>
        /// <param name="Ctype">会话类型 0 ：售楼部与用户间对话，1：售楼部与售楼部间对话 ，2：用户与用户间对话</param>
        /// <returns></returns>
        public string GetSetConversationID(string AccountMainID, string AID, string UID, string Ctype)
        {
            var ConversationModel = Factory.Get<IConversationModel>(SystemConst.IOC_Model.ConversationModel);
            var Conver = ConversationModel.GetCID(AccountMainID, AID, UID, Ctype);
            return Conver.ToString();
            
        }

        /// <summary>
        /// 移动端上传文件
        /// </summary>
        /// <param name="FileType">文件类型 1 图片，2语音，3视频,4 头像</param>
        /// <param name="UserID">发送人ID</param>
        /// <param name="UserType">发送人类型 1，售楼人员，2用户</param>
        /// <param name="UserAccountMainID">发送人所属售楼部ID</param>
        /// <param name="FileBuffer">二进制文件</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public string UpLodeFiles(int FileType, int UserType, int UserID, int UserAccountMainID)//byte[] FileBuffer
        {

            Result result = new Result();
            //路径
            string UpFile = ConfigurationManager.AppSettings["UpLodeFile"].ToString();
            //人员类型
            string UpType = string.Empty;
            //文件类型
            string UpFileType = string.Empty;
            //文件类型 参数
            switch (FileType)
            { 
                case 1:
                    UpFileType = "Image";
                    break;
                case 2:
                    UpFileType = "Voice";
                    break;
                case 3:
                    UpFileType = "Video";
                    break;
                case 4:
                    UpFileType = "HeadImage";
                    break;
            }
            //发送人类型
            if (UserType == 1)
            {
                UpType = "Account";
            }
            else if (UserType == 2)
            {
                UpType = "User";
            }
            //拼接路径
            string UPFileHname = DateTime.Now.ToString("yyMMddhhmmss");
            string Path = string.Format("{0}{1}.Message/{2}/{3}/{4}", UpFile, UserAccountMainID, UpType, UserID, UpFileType);
            try
            {
                if (!System.IO.File.Exists(Server.MapPath(Path)))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(Path));
                }
                Path = Path + "/" + UPFileHname + "_" + Request.Files[0].FileName;
                //全路径
                string FilePath = Server.MapPath(Path);

                //保存文件
                Request.Files[0].SaveAs(FilePath);
                
                //System.IO.File.WriteAllBytes(FilePath, FileBuffers);
                //返回路径
                result.Entity = SystemConst.WebUrl + Url.Content(Path);
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }

        }

    }
}
