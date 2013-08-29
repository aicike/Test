using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Poco.Enum;
using Injection;
using Poco.WebAPI_Poco;
using Injection.Transaction;

namespace Business
{
    /// <summary>
    /// 推送
    /// </summary>
    public class PushModel
    {
        public PushModel()
        {
        }

        /// <summary>
        /// 普通推送
        /// </summary>
        [Transaction]
        public Result Push(EnumMessageType msgType, int? libraryID, string url, string content, string receiveType, int accountID, string userIds, int accountMainID)
        {
            Result result = new Result();
            var iosModel = Factory.Get("Push_IOS") as IPushModel;
            var androidModel = Factory.Get("Push_Getui") as IPushModel;
            var PushIDInfo = GetClientIDs(receiveType, accountID, userIds);
            //售楼部信息
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var accountMain = accountMainModel.Get(accountMainID);
            string title = string.Format("来自{0}的消息", accountMain.Name);

            //保存在数据库
            var pmModel = Factory.Get<IPushMsgModel>(SystemConst.IOC_Model.PushMsgModel);
            PushMsg pushMsg = new PushMsg();
            switch (msgType)
            {
                case EnumMessageType.Text:
                    pushMsg.EnumMessageType = (int)msgType;
                    pushMsg.Text = content;
                    break;
                case EnumMessageType.Image:
                    pushMsg.EnumMessageType = (int)msgType;
                    pushMsg.LibraryID = libraryID;
                    break;
                case EnumMessageType.Voice:
                    pushMsg.EnumMessageType = (int)msgType;
                    pushMsg.LibraryID = libraryID;
                    break;
                case EnumMessageType.Video:
                    pushMsg.EnumMessageType = (int)msgType;
                    pushMsg.LibraryID = libraryID;
                    break;
                case EnumMessageType.ImageText:
                    pushMsg.EnumMessageType = (int)msgType;
                    pushMsg.LibraryID = libraryID;
                    break;
            }
            pushMsg.PushTime = DateTime.Now;
            pushMsg.PushStatus = (int)EnumPushStatus.Success;
            if (receiveType == "all")
            {
                pushMsg.EnumPushType = (int)EnumPushType.All;
            }
            else
            {
                pushMsg.EnumPushType = (int)EnumPushType.Appoint;
            }
            pushMsg.AccountMainID = accountMainID;
            pushMsg.AccountID = accountID;
            result = pmModel.AddPush(pushMsg, PushIDInfo.userIDs);
            if (result.HasError)
            {
                return result;
            }

            //封装消息
            string hostUrl = SystemConst.WebUrl;
            List<App_AutoMessageReplyContent> pushMessage = new List<App_AutoMessageReplyContent>();
            App_AutoMessageReplyContent rep = new App_AutoMessageReplyContent();
            rep.FileTitle = title;
            rep.MsgID = pushMsg.ID;
            rep.EnumMsgModel = (int)EnumMsgModel.Push;
            rep.Type = (int)msgType;
            rep.SendTime = pushMsg.PushTime.ToString("yyyy-MM-dd hh:mm:ss");
            switch (msgType)
            {
                case EnumMessageType.Text:
                    rep.ID = 0;
                    rep.Content = content;
                    break;
                case EnumMessageType.Image:
                case EnumMessageType.Voice:
                case EnumMessageType.Video:
                    rep.ID = libraryID.Value;
                    rep.FileUrl = hostUrl + Url(url);
                    rep.FileTitle = content;
                    break;
                case EnumMessageType.ImageText:
                    var libraryImageTextModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
                    var itext = libraryImageTextModel.Get(libraryID.Value);
                    rep.ID = libraryID.Value;
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
                            rep_it.FileUrl = hostUrl + Url(it.ImagePath);
                            subImageText.Add(rep_it);
                        }
                        rep.SubContent = Newtonsoft.Json.JsonConvert.SerializeObject(subImageText);
                    }
                    rep.Content = itext.Content;
                    break;
            }
            pushMessage.Add(rep);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(pushMessage);

            //ios推送
            //未实现
            //android推送
            PushMessage message = new PushMessage();
            message.Title = title;
            message.Text = content;
            message.Logo = "logo.png";
            message.EnumEvent = EnumEvent.Wait;// EnumEvent.Immediately;
            message.MessageJson = json;
            result = Push_Getui.SendMessage(message, PushIDInfo.Android);
            return result;
        }

        /// <summary>
        /// 当没有在线，并且接收到消息时，调用推送方法
        /// </summary>
        public Result PushFromChat(EnumMessageType msgType, string content, EnumClientUserType toUserType, int toUserID, EnumClientUserType fromUserType, int fromUserID)
        {
            //string name = null;
            ////获取发信人名称
            //if (fromUserType == EnumClientUserType.Account)
            //{
            //    var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            //    var account = accountModel.Get(fromUserID);
            //    name = account.Name;
            //}
            //else
            //{
            //    var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            //    var user = userModel.Get(fromUserID);
            //    name = user.Name;
            //}
            string title = "你有一条消息。";
            Result result = new Result();
            var iosModel = Factory.Get("Push_IOS") as IPushModel;
            var androidModel = Factory.Get("Push_Getui") as IPushModel;
            var PushIDInfo = GetClientIDs(toUserType, toUserID);
            //封装消息
            List<App_AutoMessageReplyContent> pushMessage = new List<App_AutoMessageReplyContent>();
            App_AutoMessageReplyContent rep = new App_AutoMessageReplyContent();
            rep.FileTitle = title;
            rep.MsgID = fromUserID;
            rep.UserType = (int)fromUserType;
            rep.EnumMsgModel = (int)EnumMsgModel.Call;
            rep.Type = (int)msgType;            
            pushMessage.Add(rep);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(pushMessage);

            //ios推送
            //未实现
            //android推送
            PushMessage message = new PushMessage();
            message.Title = title;
            message.Text = content;
            message.Logo = "logo.png";
            message.EnumEvent = EnumEvent.Wait;// EnumEvent.Immediately;
            message.MessageJson = json;
            result = Push_Getui.SendMessage(message, PushIDInfo.Android);
            return result;
        }


        /// <summary>
        /// 获取clientids
        /// </summary>
        private PushIDInfo GetClientIDs(string receiveType, int accountID, string userIds)
        {
            var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
            var account_UserModel = Factory.Get<IAccount_UserModel>(SystemConst.IOC_Model.Account_UserModel);
            string clientUserType = EnumClientUserType.User.ToString();
            string clientSystemType_android = EnumClientSystemType.Android.ToString();
            string clientSystemType_ios = EnumClientSystemType.IOS.ToString();
            //安卓用户
            var clientInfoList_android = clientInfoModel.List().Where(a => a.EnumClientUserType.Token == clientUserType && a.EnumClientSystemType.Token == clientSystemType_android);
            //IOS用户
            var clientInfoList_ios = clientInfoModel.List().Where(a => a.EnumClientUserType.Token == clientUserType && a.EnumClientSystemType.Token == clientSystemType_ios);

            var userIDs = account_UserModel.List().Where(a => a.AccountID == accountID).Select(a => a.UserID).ToList();
            List<string> clientIds_android = new List<string>();
            List<string> clientIds_ios = new List<string>();
            List<int> pushUserID = null;
            if (receiveType == "all")
            {
                clientIds_android = clientInfoList_android.Where(a => userIDs.Contains(a.EntityID.Value)).Select(a => a.ClientID).ToList();
                clientIds_ios = clientInfoList_ios.Where(a => userIDs.Contains(a.EntityID.Value)).Select(a => a.ClientID).ToList();
                pushUserID = userIDs;
            }
            else if (receiveType == "user")
            {
                int[] temp_uids = userIds.ConvertToIntArray(',');
                int[] uids = temp_uids.Where(a => userIDs.Contains(a)).Select(a => a).ToArray();
                if (uids != null)
                {
                    clientIds_android = clientInfoList_android.Where(a => uids.Contains(a.EntityID.Value)).Select(a => a.ClientID).ToList();
                    clientIds_ios = clientInfoList_ios.Where(a => uids.Contains(a.EntityID.Value)).Select(a => a.ClientID).ToList();
                }
                pushUserID = uids.ToList();
            }
            PushIDInfo pi = new PushIDInfo();
            pi.Android = clientIds_android.ToArray();
            pi.IOS = clientIds_ios.ToArray();
            pi.userIDs = pushUserID;
            return pi;
        }

        private PushIDInfo GetClientIDs(EnumClientUserType userType, int userID)
        {
            string clientSystemType_android = EnumClientSystemType.Android.ToString();
            string clientSystemType_ios = EnumClientSystemType.IOS.ToString();
            var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
            string ut= userType.ToString();
            var android = clientInfoModel.List().Where(a => a.EnumClientUserType.Token == ut && a.EntityID == userID && a.EnumClientSystemType.Token == clientSystemType_android).Select(a=>a.ClientID);
            var ios = clientInfoModel.List().Where(a => a.EnumClientUserType.Token == ut && a.EntityID == userID && a.EnumClientSystemType.Token == clientSystemType_ios).Select(a => a.ClientID);
            PushIDInfo pi = new PushIDInfo();
            pi.Android = android.ToArray();
            pi.IOS = ios.ToArray();
            return pi;
        }

        public class PushIDInfo
        {
            public string[] Android { get; set; }
            public string[] IOS { get; set; }
            public List<int> userIDs { get; set; }
        }

        private static string Url(string url)
        {
            return url.Replace("~", "");
        }
    }
}
