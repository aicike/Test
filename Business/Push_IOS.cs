using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using JdSoft.Apple.Apns.Notifications;
using System.Web;
using Injection;

namespace Business
{
    public class Push_IOS : IPushModel
    {

        /// <summary>
        /// IOS 信息推送
        /// </summary>
        /// <param name="strDeviceToken">用户 Token</param>
        /// <param name="strContent">推送内容</param>
        /// <param name="p12File">证书路径（相对）</param>
        /// <returns></returns>
        public static Result SendMessage(string strDeviceToken, string strContent, string p12File)
        {
            Result result = new Result();
            try
            {
                bool sandbox = true;
                string testDeviceToken = strDeviceToken;

                //证书密码
                string p12FilePassword = "password";
                string p12Filename =p12File;

                NotificationService service = new NotificationService(sandbox, p12Filename, p12FilePassword, 1);

                service.SendRetries = 5; //5 retries before generating notificationfailed event
                service.ReconnectDelay = 5000; //5 seconds

                //service.Error += new NotificationService.OnError(service_Error);
                //service.NotificationTooLong += new NotificationService.OnNotificationTooLong(service_NotificationTooLong);
                //service.BadDeviceToken += new NotificationService.OnBadDeviceToken(service_BadDeviceToken);
                //service.NotificationFailed += new NotificationService.OnNotificationFailed(service_NotificationFailed);
                //service.NotificationSuccess += new NotificationService.OnNotificationSuccess(service_NotificationSuccess);
                //service.Connecting += new NotificationService.OnConnecting(service_Connecting);
                //service.Connected += new NotificationService.OnConnected(service_Connected);
                //service.Disconnected += new NotificationService.OnDisconnected(service_Disconnected);
                Notification alertNotification = new Notification(testDeviceToken);

                //通知内容
                alertNotification.Payload.Alert.Body = strContent;

                alertNotification.Payload.Sound = "default";
                alertNotification.Payload.Badge = 1;

                //队列的通知是否送达
                if (service.QueueNotification(alertNotification))
                {

                }

                service.Close();
                service.Dispose();
            }catch
            {
                result.HasError = true;
            }
            return result;
        }
    }
}
