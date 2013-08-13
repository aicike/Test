using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interface;
using Poco;
using Injection;
using Poco.WebAPI_Poco;
using System.Text;
using Poco.Enum;

namespace Web.Controllers
{
    public class WebRequest_AutoMessageController : Controller
    {
        const string Text = "Text";
        const string Image = "Image";
        const string Video = "Video";
        const string Voice = "Voice";
        const string ImageText = "ImageText";
        private string hostUrl = null;

        /// <summary>
        /// 获取首次引导信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetFirstAutoMessageList(int accountMainID)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            var list = autoMessage_KeywordModel.GetFirstAutoMessage(accountMainID);
            Result result = new Result();
            List<App_AutoMessage> appList = new List<App_AutoMessage>();
            foreach (var item in list)
            {
                appList.Add(new App_AutoMessage
                {
                    ID = item.ID,
                    RuleName = item.RuleName
                });
            }
            result.Entity = appList;
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 根据编号返回答案
        /// </summary>
        [HttpPost]
        public string GetAutoMessageByID(int autoMessageID)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            Result result = new Result();
            try
            {
                var obj = autoMessage_KeywordModel.Get(autoMessageID);

                var otherReply = obj.KeywordAutoMessages.OrderBy(a => a.Order);
                const string Text = "Text";
                const string Image = "Image";
                const string Video = "Video";
                const string Voice = "Voice";
                const string ImageText = "ImageText";
                hostUrl = string.Format("http://{0}:{1}", Request.Url.Host, Request.Url.Port);
                result.Entity = GetReplayList(otherReply.ToList());
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                throw;
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }


        /// <summary>
        /// 根据关键字返回答案
        /// </summary>
        [HttpPost]
        public string GetAutoMessageByKey(int accountMainID, string key)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            Result result = new Result();
            try
            {
                var list = autoMessage_KeywordModel.GetAutoMessageByKey(accountMainID, key);
                List<App_AutoMessage> messageList = new List<App_AutoMessage>();
                if (list.Count == 0)
                {
                    result.EntityType = "App_AutoMessage";
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result);
                }

                if (list.Count > 1)
                {
                    //多条，推送问题
                    foreach (var messageKey in list)
                    {
                        messageList.Add(new App_AutoMessage
                        {
                            ID = messageKey.ID,
                            RuleName = messageKey.RuleName
                        });
                    }
                    result.EntityType = "App_AutoMessage";
                    result.Entity = messageList;
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result);
                }
                else
                {
                    //单条，推送答案
                    var replay = list.FirstOrDefault();
                    var otherReply = replay.KeywordAutoMessages.OrderBy(a => a.Order);

                    string hostUrl = string.Format("http://{0}:{1}", Request.Url.Host, Request.Url.Port);

                    result.EntityType = "App_AutoMessageReplyContent";
                    result.Entity = GetReplayList(otherReply.ToList());
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result);
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        private List<App_AutoMessageReplyContent> GetReplayList(List<KeywordAutoMessage> list)
        {
            var libraryTextModel = Factory.Get<ILibraryTextModel>(SystemConst.IOC_Model.LibraryTextModel);
            var libraryImageModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
            var libraryImageTextModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var libraryVideoModel = Factory.Get<ILibraryVideoModel>(SystemConst.IOC_Model.LibraryVideoModel);
            var libraryVoiceModel = Factory.Get<ILibraryVoiceModel>(SystemConst.IOC_Model.LibraryVoiceModel);

            List<App_AutoMessageReplyContent> replayList = new List<App_AutoMessageReplyContent>();
            foreach (var item in list)
            {
                App_AutoMessageReplyContent rep = new App_AutoMessageReplyContent();
                switch (item.EnumMessageType.Token)
                {
                    case Text:
                        rep.ID = item.ID;
                        rep.Type = (int)EnumMessageType.Text;
                        rep.Content = item.TextReply;
                        break;
                    case Image:
                        var img = libraryImageModel.Get(item.MessageID);
                        if (img != null)
                        {
                            rep.Type = (int)EnumMessageType.Image;
                            rep.FileUrl = hostUrl + Url.Content(img.FilePath);
                            rep.FileTitle = img.FileName;
                        }
                        break;
                    case Video:
                        var video = libraryVideoModel.Get(item.MessageID);
                        if (video != null)
                        {
                            rep.Type = (int)EnumMessageType.Video;
                            rep.FileUrl = hostUrl + Url.Content(video.FilePath);
                            rep.FileTitle = video.FileName;
                        }
                        break;
                    case Voice:
                        var voice = libraryVoiceModel.Get(item.MessageID);
                        if (voice != null)
                        {
                            rep.Type = (int)EnumMessageType.Voice;
                            rep.FileUrl = hostUrl + Url.Content(voice.FilePath);
                            rep.FileTitle = voice.FileName;
                        }
                        break;
                    case ImageText:
                        var itext = libraryImageTextModel.Get(item.MessageID);
                        if (itext != null)
                        {
                            rep.Type = (int)EnumMessageType.ImageText;
                            var headImage = hostUrl + Url.Content(itext.ImagePath);
                            rep.Content = string.Format("|{0}|/n{1}", headImage, itext.Summary);
                        }
                        break;
                }
                replayList.Add(rep);
            }
            return replayList;
        }
    }
}
