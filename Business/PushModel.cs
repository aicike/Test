using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Poco.Enum;
using Injection;
using Poco.WebAPI_Poco;

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

        public Result Push_Text(string content, string receiveType, int accountID, string userIds, int accountMainID)
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
            //未实现
            List<App_AutoMessageReplyContent> pushMessage = new List<App_AutoMessageReplyContent>();
            App_AutoMessageReplyContent rep = new App_AutoMessageReplyContent();
            rep.ID = 0;
            rep.Type = (int)EnumMessageType.Text;
            rep.FileTitle = title;
            rep.Content = content;
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
            if (result.HasError == false)
            {
                //保存在数据库中
            }
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
            if (receiveType == "all")
            {
                clientIds_android = clientInfoList_android.Where(a => userIDs.Contains(a.EntityID.Value)).Select(a => a.ClientID).ToList();
                clientIds_ios = clientInfoList_ios.Where(a => userIDs.Contains(a.EntityID.Value)).Select(a => a.ClientID).ToList();
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
            }
            PushIDInfo pi = new PushIDInfo();
            pi.Android = clientIds_android.ToArray();
            pi.IOS = clientIds_ios.ToArray();
            return pi;
        }

        public class PushIDInfo
        {
            public string[] Android { get; set; }
            public string[] IOS { get; set; }
        }
    }
}
