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
        public string GetMessageList(int currentUserType, int sessionID, int pageSize, int lastMessageID)
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
            var libraryImageTextModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            foreach (var item in list)
            {
                App_Chat chat = new App_Chat();
                chat.ID = item.ID;
                var sendTime = item.SendTime.ToString(SystemConst.Business.TimeFomatFull);
                chat.SendTime = sendTime;
                chat.Type = item.EnumMessageTypeID;
                switch (item.EnumMessageTypeID)
                {
                    case (int)EnumMessageType.Text:
                        chat.Content = item.TextContent;
                        break;
                    case (int)EnumMessageType.Image:
                    case (int)EnumMessageType.Video:
                    case (int)EnumMessageType.Voice:
                        chat.FileUrl = item.FileUrl;
                        break;
                    case (int)EnumMessageType.ImageText:
                        var itext = libraryImageTextModel.Get(item.LibraryImageTextsID.Value);
                        if (itext != null)
                        {
                            chat.ID = itext.ID;
                            chat.FileTitle = itext.Title;
                            chat.Summary = itext.Summary;
                            chat.FileUrl =SystemConst.WebUrl+ itext.ImagePath.Replace("~","");
                            if (itext.LibraryImageTexts.Count > 0)
                            {
                                List<App_AutoMessageReplyContent> subImageText = new List<App_AutoMessageReplyContent>();
                                foreach (var it in itext.LibraryImageTexts)
                                {
                                    App_AutoMessageReplyContent rep_it = new App_AutoMessageReplyContent();
                                    rep_it.ID = it.ID;
                                    rep_it.Type = (int)EnumMessageType.ImageText;
                                    rep_it.FileTitle = it.Title;
                                    rep_it.FileUrl = SystemConst.WebUrl + it.ImagePath.Replace("~", "");
                                    //rep_it.SendTime = sendTime;
                                    subImageText.Add(rep_it);
                                }
                                chat.SubContent = Newtonsoft.Json.JsonConvert.SerializeObject(subImageText);
                            }
                            chat.Content = itext.Content;
                        }
                        break;
                }
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
            try
            {
                var Conver = ConversationModel.GetCID(AccountMainID, AID, UID, Ctype);
                return Conver.ToString();
            }
            catch
            {
                return "0";
            }
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
        public string UpLodeFiles(int FileType, int UserType, int UserID, int UserAccountMainID,string Token)//byte[] FileBuffer
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
            //头像
            if (FileType == 4)
            {
                //售楼部
                if (UserType == 1)
                {
                    Path = string.Format("{0}{1}/Account", UpFile, UserAccountMainID);
                }
            }
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
                result.Entity = new { URL = SystemConst.WebUrl + Url.Content(Path), Token = Token };
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
