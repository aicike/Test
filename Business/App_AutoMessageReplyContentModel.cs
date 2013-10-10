using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.WebAPI_Poco;
using Poco;
using Injection;
using Interface;
using Poco.Enum;

namespace Business
{
    public class App_AutoMessageReplyContentModel
    {
        private App_AutoMessageReplyContentModel() { }

        public static List<App_AutoMessageReplyContent> GetReplayList(List<KeywordAutoMessage> list)
        {
            var libraryTextModel = Factory.Get<ILibraryTextModel>(SystemConst.IOC_Model.LibraryTextModel);
            var libraryImageModel = Factory.Get<ILibraryImageModel>(SystemConst.IOC_Model.LibraryImageModel);
            var libraryImageTextModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var libraryVideoModel = Factory.Get<ILibraryVideoModel>(SystemConst.IOC_Model.LibraryVideoModel);
            var libraryVoiceModel = Factory.Get<ILibraryVoiceModel>(SystemConst.IOC_Model.LibraryVoiceModel);

            string hostUrl = SystemConst.WebUrlIP;
            List<App_AutoMessageReplyContent> replayList = new List<App_AutoMessageReplyContent>();
            foreach (var item in list)
            {
                App_AutoMessageReplyContent rep = new App_AutoMessageReplyContent();
                switch (item.EnumMessageType.Token)
                {
                    case "Text":
                        rep.ID = item.ID;
                        rep.Type = (int)EnumMessageType.Text;
                        rep.Content = item.TextReply;
                        break;
                    case "Image":
                        var img = libraryImageModel.Get(item.MessageID);
                        if (img != null)
                        {
                            rep.ID = img.ID;
                            rep.Type = (int)EnumMessageType.Image;
                            rep.FileUrl = hostUrl + Url(img.FilePath);
                            rep.FileTitle = img.FileName;
                        }
                        break;
                    case "Video":
                        var video = libraryVideoModel.Get(item.MessageID);
                        if (video != null)
                        {
                            rep.ID = video.ID;
                            rep.Type = (int)EnumMessageType.Video;
                            rep.FileUrl = hostUrl + Url(video.FilePath);
                            rep.FileTitle = video.FileName;
                        }
                        break;
                    case "Voice":
                        var voice = libraryVoiceModel.Get(item.MessageID);
                        if (voice != null)
                        {
                            rep.ID = voice.ID;
                            rep.Type = (int)EnumMessageType.Voice;
                            rep.FileUrl = hostUrl + Url(voice.FilePath);
                            rep.FileTitle = voice.FileName;
                        }
                        break;
                    case "ImageText":
                        var itext = libraryImageTextModel.Get(item.MessageID);
                        if (itext != null)
                        {
                            rep.ID = itext.ID;
                            rep.Type = (int)EnumMessageType.ImageText;
                            rep.FileTitle = itext.Title;
                            rep.Summary = itext.Summary;
                            rep.FileUrl = hostUrl + Url(itext.ImagePath);
                            if (itext.LibraryImageTexts.Count > 0)
                            {
                                List<App_AutoMessageReplyContent> subImageText = new List<App_AutoMessageReplyContent>();
                                foreach (var it in itext.LibraryImageTexts)
                                {
                                    App_AutoMessageReplyContent rep_it = new App_AutoMessageReplyContent();
                                    rep_it.ID = it.ID;
                                    rep_it.Type = (int)EnumMessageType.ImageText;
                                    rep_it.FileTitle = it.Title;
                                    rep_it.FileUrl = it.ImagePath;
                                    subImageText.Add(rep_it);
                                }
                                rep.SubContent = Newtonsoft.Json.JsonConvert.SerializeObject(subImageText);
                            }
                            rep.Content = itext.Content;
                        }
                        break;
                }
                rep.SendTime = item.SendTime;
                replayList.Add(rep);
            }
            return replayList;
        }

        private static string Url(string url)
        {
            return url.Replace("~", "");
        }
    }
}
