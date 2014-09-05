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
using JdSoft.Apple.Apns.Notifications;
using System.Web;

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

        public class PushJson
        {
            public int type { get; set; }
            public object obj { get; set; }
        }


        private enum PushType
        {
            user_automsg = 1,   //用户_自助问答
            user_news = 2,      //用户_通知，新闻
            account_bx = 3,        //物业_保修
            account_ts = 4,        //物业_投诉
            user_kd = 5       //用户_快递
        }

        #region 自助问答推送

        /// <summary>
        /// 普通推送
        /// </summary>
        [Transaction]
        public Result Push(EnumMessageType msgType, int? libraryID, string url, string content, string receiveType, int accountID, string houseIds, int accountMainID)
        {
            Result result = new Result();
            var iosModel = Factory.Get("Push_IOS") as IPushModel;
            var androidModel = Factory.Get("Push_Getui") as IPushModel;

            //根据HouseID获取userID
            string userIds = "";
            if (receiveType == "user")
            {
                var property_UserModel = Factory.Get<IProperty_UserModel>(SystemConst.IOC_Model.Property_UserModel);
                var houseList = houseIds.ConvertToIntArray(',');
                var UserLoginInfoList = property_UserModel.List().Where(a => houseList.Contains(a.Property_HouseID)).Select(a => a.UserLoginInfo).ToList();
                if (UserLoginInfoList != null)
                {
                    foreach (var item in UserLoginInfoList)
                    {
                        if (item != null)
                        {
                            userIds += item.Users.Select(a => a.ID).ToList().ConvertToString(",");
                        }
                    }
                }
            }
            var PushIDInfo = GetClientIDs_user(receiveType, accountMainID, userIds);
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
            string hostUrl = SystemConst.WebUrlIP;
            List<App_AutoMessageReplyContent> pushMessage = new List<App_AutoMessageReplyContent>();
            App_AutoMessageReplyContent rep = new App_AutoMessageReplyContent();
            rep.FileTitle = title;
            rep.MsgID = pushMsg.ID;
            rep.EnumMsgMode = (int)EnumMsgMode.Push;
            rep.Type = (int)msgType;
            rep.SendTime = pushMsg.PushTime.ToString("yyyy-MM-dd HH:mm:ss");
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
            var obj = new PushJson() { type = (int)PushType.user_automsg, obj = pushMessage };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            //ios推送 用户端

            pushNotifications_User("您有一条新的消息。", accountMainID, PushIDInfo.IOS);

            //android推送
            PushMessage message = new PushMessage();
            message.Title = title;
            message.Text = content;
            message.Logo = "ic_launcher.png";
            message.EnumEvent = EnumEvent.Wait;// EnumEvent.Immediately;
            message.MessageJson = json;
            result = Push_Getui.SendMessage(message, PushIDInfo.Android);
            return result;
        }
        #endregion

        #region 用户端，新闻、通知类推送

        /// <summary>
        /// 新闻，通知推送
        /// </summary>
        /// <param name="type">类型：小区通知 news</param>
        /// <param name="accountMainID"></param>
        /// <returns></returns>
        public Result Push(string type, int objID, string objTitle, string objImage, string objContent, int accountMainID)
        {
            var PushIDInfo = GetClientIDs_user("all", accountMainID, null);
            string title = "";
            var obj = new PushJson() { type = (int)PushType.user_news, obj = new { pushType = "2", id = objID, title = objTitle, F = SystemConst.WebUrlIP + objImage ?? "", P = objContent, url = SystemConst.WebUrlIP + "/Default/News?id_token=" + objID.TokenEncrypt() } };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            switch (type)
            {
                case "news":
                    title = "新通知";
                    break;
            }
            //android推送
            PushMessage message = new PushMessage();
            message.Title = title;
            message.Text = objTitle;
            message.Logo = "ic_launcher.png";
            message.EnumEvent = EnumEvent.Wait;// EnumEvent.Immediately;
            message.MessageJson = json;
            var result = Push_Getui.SendMessage(message, PushIDInfo.Android);
            return result;
        }
        #endregion

        #region 物业端，物业接收到保修，投诉推送

        /// <summary>
        /// 物业端，物业接收到保修，投诉推送
        /// </summary>
        /// <param name="accountMainID"></param>
        /// <returns></returns>
        public Result Push(string type, int objID, string objTitle, int accountMainID)
        {
            Result result = null;
            try
            {
                var PushIDInfo = GetClientIDs_account("all", accountMainID, null);
                string title = "";
                var obj = new PushJson() { type = (int)PushType.account_bx, obj = new { id = objID, title = objTitle } };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                switch (type)
                {
                    case "bx":
                        title = "新通知：投诉信息";
                        break;
                    case "ts":
                        title = "新通知：投诉信息";
                        break;
                }
                //android推送
                PushMessage message = new PushMessage();
                message.Title = title;
                message.Text = objTitle;
                message.Logo = "ic_launcher.png";
                message.EnumEvent = EnumEvent.Wait;// EnumEvent.Immediately;
                message.MessageJson = json;
                result = Push_Getui.SendMessage(message, PushIDInfo.Android);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        #endregion

        #region 用户端，快递代收推送

        public Result Push(string type, int accountMainID, string phone,string msg)
        {
            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            var userIDs = userModel.List().Where(a => a.AccountMainID == accountMainID && a.Phone == phone).Select(a => a.ID).ToList().ConvertToString(",");
            if (userIDs == null || userIDs.Length == 0)
            {
                return new Result();
            }
            var PushIDInfo = GetClientIDs_user("user", accountMainID, userIDs);
            var obj = new PushJson() { type = (int)PushType.user_kd };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            //android推送
            PushMessage message = new PushMessage();
            message.Title = "新通知-快递代收";
            message.Text = msg;
            message.Logo = "ic_launcher.png";
            message.EnumEvent = EnumEvent.Wait;// EnumEvent.Immediately;
            message.MessageJson = json;
            var result = Push_Getui.SendMessage(message, PushIDInfo.Android);
            return result;
        }
        #endregion

        /// <summary>
        /// 当没有在线，并且接收到消息时，调用推送方法 备注 目前只能发送用户端
        /// </summary>
        public Result PushFromChat(EnumMessageType msgType, string content, EnumClientUserType toUserType, int toUserID, EnumClientUserType fromUserType, int fromUserID)
        {
            var accountmodel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = accountmodel.Get(fromUserID);
            var account_accounmain = account.Account_AccountMains.FirstOrDefault();
            string title = account_accounmain.AccountMain.Name + "的" + account.Name + " 给您发来一条消息。";
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
            rep.EnumMsgMode = (int)EnumMsgMode.Call;
            rep.Type = (int)msgType;
            pushMessage.Add(rep);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(pushMessage);

            //ios推送 用户端
            if (toUserType == EnumClientUserType.User)
            {
                var usermodel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
                var user = usermodel.Get(toUserID);
                pushNotifications_User(title, user.AccountMainID, PushIDInfo.IOS);
            }
            //android推送
            PushMessage message = new PushMessage();
            message.Title = title;
            message.Text = "";//content;
            message.Logo = "ic_launcher.png";
            message.EnumEvent = EnumEvent.Wait;// EnumEvent.Immediately;
            message.MessageJson = json;
            result = Push_Getui.SendMessage(message, PushIDInfo.Android);
            return result;
        }


        /// <summary>
        /// 获取clientids
        /// </summary>
        private PushIDInfo GetClientIDs_user(string receiveType, int accountMainID, string userIds)
        {
            var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
            var userModel = Factory.Get<IUserModel>(SystemConst.IOC_Model.UserModel);
            string clientUserType = EnumClientUserType.User.ToString();
            string clientSystemType_android = EnumClientSystemType.Android.ToString();
            string clientSystemType_ios = EnumClientSystemType.IOS.ToString();
            //安卓用户
            var clientInfoList_android = clientInfoModel.List().Where(a => a.EnumClientUserType.Token == clientUserType && a.EnumClientSystemType.Token == clientSystemType_android);
            //IOS用户
            var clientInfoList_ios = clientInfoModel.List().Where(a => a.EnumClientUserType.Token == clientUserType && a.EnumClientSystemType.Token == clientSystemType_ios);

            var userIDs = userModel.List().Where(a => a.AccountMainID == accountMainID).Select(a => a.ID).ToList();
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


        /// <summary>
        /// 获取clientids
        /// </summary>
        private PushIDInfo GetClientIDs_account(string receiveType, int accountMainID, string accountIds)
        {
            var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
            var account_AccountMainModel = Factory.Get<IAccount_AccountMainModel>(SystemConst.IOC_Model.Account_AccountMainModel);
            string clientUserType = EnumClientUserType.Account.ToString();
            string clientSystemType_android = EnumClientSystemType.Android.ToString();
            string clientSystemType_ios = EnumClientSystemType.IOS.ToString();
            //安卓用户
            var clientInfoList_android = clientInfoModel.List().Where(a => a.EnumClientUserType.Token == clientUserType && a.EnumClientSystemType.Token == clientSystemType_android);
            //IOS用户
            var clientInfoList_ios = clientInfoModel.List().Where(a => a.EnumClientUserType.Token == clientUserType && a.EnumClientSystemType.Token == clientSystemType_ios);

            var accountIDs = account_AccountMainModel.List().Where(a => a.AccountMainID == accountMainID).Select(a => a.AccountID).ToList();
            List<string> clientIds_android = new List<string>();
            List<string> clientIds_ios = new List<string>();
            if (receiveType == "all")
            {
                clientIds_android = clientInfoList_android.Where(a => accountIDs.Contains(a.EntityID.Value)).Select(a => a.ClientID).ToList();
                clientIds_ios = clientInfoList_ios.Where(a => accountIDs.Contains(a.EntityID.Value)).Select(a => a.ClientID).ToList();
            }
            else if (receiveType == "user")
            {
                int[] temp_uids = accountIds.ConvertToIntArray(',');
                int[] uids = temp_uids.Where(a => accountIDs.Contains(a)).Select(a => a).ToArray();
                if (uids != null)
                {
                    clientIds_android = clientInfoList_android.Where(a => uids.Contains(a.EntityID.Value)).Select(a => a.ClientID).ToList();
                    clientIds_ios = clientInfoList_ios.Where(a => uids.Contains(a.EntityID.Value)).Select(a => a.ClientID).ToList();
                }
            }
            PushIDInfo pi = new PushIDInfo();
            pi.Android = clientIds_android.ToArray();
            pi.IOS = clientIds_ios.ToArray();
            return pi;
        }

        private PushIDInfo GetClientIDs(EnumClientUserType userType, int userID)
        {
            string clientSystemType_android = EnumClientSystemType.Android.ToString();
            string clientSystemType_ios = EnumClientSystemType.IOS.ToString();
            var clientInfoModel = Factory.Get<IClientInfoModel>(SystemConst.IOC_Model.ClientInfoModel);
            string ut = userType.ToString();
            var android = clientInfoModel.List().Where(a => a.EnumClientUserType.Token == ut && a.EntityID == userID && a.EnumClientSystemType.Token == clientSystemType_android).Select(a => a.ClientID);
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



        /// <summary>
        /// （用户端）IOS推送服务
        /// </summary>
        /// <param name="strContent"></param>
        /// <param name="accountMainID"></param>
        /// <param name="clientIDs">用户token</param>
        public static void pushNotifications_User(string strContent, int accountMainID, string[] ClientIDs)
        {
            var accountMainModel = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            var accountMain = accountMainModel.Get(accountMainID);
            //证书路径 用户端证书
            string p12File = accountMain.IOSClientCertificate;
            foreach (var item in ClientIDs)
            {
                //推送
                Push_IOS.SendMessage(item, strContent, p12File);
            }
        }
    }


}
