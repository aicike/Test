﻿using System;
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

        [HttpPost]
        public string GetAutoMessageByID(int autoMessageID)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            Result result = new Result();
            try
            {

                var obj = autoMessage_KeywordModel.Get(autoMessageID);
                List<App_AutoMessageReplyContent> replayList = new List<App_AutoMessageReplyContent>();

                //普通文本
                foreach (var item in obj.TextReplys)
                {
                    App_AutoMessageReplyContent rep = new App_AutoMessageReplyContent();
                    rep.ID = item.ID;
                    rep.Type = (int)EnumMessageType.Text;
                    rep.Content = item.Content;
                    replayList.Add(rep);
                }
                //其他类型回复
                var otherReply = obj.KeywordAutoMessages.OrderBy(a => a.Order);
                const string Image = "Image";
                const string Video = "Video";
                const string Voice = "Voice";
                const string ImageText = "ImageText";

                var libraryImageModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
                var libraryImageTextModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
                var libraryVideoModel = Factory.Get<ILibraryVideoModel>(SystemConst.IOC_Model.LibraryVideoModel);
                var libraryVoiceModel = Factory.Get<ILibraryVoiceModel>(SystemConst.IOC_Model.LibraryVoiceModel);
                string hostUrl = string.Format("http://{0}:{1}", Request.Url.Host, Request.Url.Port);

                foreach (var item in otherReply)
                {
                    App_AutoMessageReplyContent rep = new App_AutoMessageReplyContent();
                    switch (item.EnumMessageType.Token)
                    {
                        case Image:
                            var img = libraryImageModel.Get(item.MessageID);
                            if (img != null)
                            {
                                rep.Type = (int)EnumMessageType.Image;
                                rep.Content = hostUrl + Url.Content(img.FilePath);
                            }
                            break;
                        case Video:
                            var video = libraryVideoModel.Get(item.MessageID);
                            if (video != null)
                            {
                                rep.Type = (int)EnumMessageType.Video;
                                rep.Content = hostUrl + Url.Content(video.FilePath);
                            }
                            break;
                        case Voice:
                            var voice = libraryVoiceModel.Get(item.MessageID);
                            if (voice != null)
                            {
                                rep.Type = (int)EnumMessageType.Voice;
                                rep.Content = hostUrl + Url.Content(voice.FilePath);
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
                result.Entity = replayList;
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                throw;
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public string GetAutoMessageByKey(int accountMainID, string key)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            Result result = new Result();
            try
            {
                var list = autoMessage_KeywordModel.GetAutoMessageByKey(accountMainID, key);
                List<App_AutoMessageReplyContent> replayList = new List<App_AutoMessageReplyContent>();
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
                    //普通文本
                    foreach (var reply in replay.TextReplys)
                    {
                        App_AutoMessageReplyContent rep = new App_AutoMessageReplyContent();
                        rep.ID = reply.ID;
                        rep.Type = (int)EnumMessageType.Text;
                        rep.Content = reply.Content;
                        replayList.Add(rep);
                    }
                    //其他类型回复
                    var otherReply = replay.KeywordAutoMessages.OrderBy(a => a.Order);
                    const string Image = "Image";
                    const string Video = "Video";
                    const string Voice = "Voice";
                    const string ImageText = "ImageText";

                    var libraryImageModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
                    var libraryImageTextModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
                    var libraryVideoModel = Factory.Get<ILibraryVideoModel>(SystemConst.IOC_Model.LibraryVideoModel);
                    var libraryVoiceModel = Factory.Get<ILibraryVoiceModel>(SystemConst.IOC_Model.LibraryVoiceModel);
                    string hostUrl = string.Format("http://{0}:{1}", Request.Url.Host, Request.Url.Port);

                    foreach (var item in otherReply)
                    {
                        App_AutoMessageReplyContent rep = new App_AutoMessageReplyContent();
                        switch (item.EnumMessageType.Token)
                        {
                            case Image:
                                var img = libraryImageModel.Get(item.MessageID);
                                if (img != null)
                                {
                                    rep.Type = (int)EnumMessageType.Image;
                                    rep.Content = hostUrl + Url.Content(img.FilePath);
                                }
                                break;
                            case Video:
                                var video = libraryVideoModel.Get(item.MessageID);
                                if (video != null)
                                {
                                    rep.Type = (int)EnumMessageType.Video;
                                    rep.Content = hostUrl + Url.Content(video.FilePath);
                                }
                                break;
                            case Voice:
                                var voice = libraryVoiceModel.Get(item.MessageID);
                                if (voice != null)
                                {
                                    rep.Type = (int)EnumMessageType.Voice;
                                    rep.Content = hostUrl + Url.Content(voice.FilePath);
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
                    result.EntityType = "App_AutoMessageReplyContent";
                    result.Entity = replayList;
                    return Newtonsoft.Json.JsonConvert.SerializeObject(result);
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

    }
}
