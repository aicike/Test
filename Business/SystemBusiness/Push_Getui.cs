using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.igetui.api.openservice;
using com.igetui.api.openservice.igetui.template;
using com.igetui.api.openservice.igetui;
using Poco;
using Interface;

namespace Business
{
    public class Push_Getui : IPushModel
    {
        private Push_Getui() { }

        //参数设置 <-----参数需要重新设置----->
        private static string APPID = "b9Y6AwqYpI6BXa7WXdNWY1";                     //您应用的AppId
        private static string APPKEY = "OJFrUP0Ozi6xrnY1uDxXb";                     //您应用的AppKey
        private static string MASTERSECRET = "opMTIjMpFQ76iq5kzDAvC5";              //您应用的MasterSecret 
        private static string CLIENTID = "03b2ac5b2c55619f7c29f87eabff771f";        //您获取的clientID
        private static String HOST = "http://sdk.open.api.igexin.com/apiex.htm";    //HOST：OpenService接口地址
        //private static String HOST = "http://192.168.10.61:8006/apiex.htm";

        public static Result SendMessage(PushMessage pushMessage, params string[] clientIDs)
        {
            Result result = new Result();
            try
            {
                IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);
                ListMessage message = new ListMessage();
                NotificationTemplate template = new NotificationTemplate();
                template.AppId = APPID;                             // 应用APPID
                template.AppKey = APPKEY;                           // 应用APPKEY
                template.Title = pushMessage.Title;                // 通知标题
                template.Text = pushMessage.Text;                 // 通知内容
                template.Logo = pushMessage.Logo;                         // 应用Logo
                template.TransmissionType = ((int)pushMessage.EnumEvent).ToString();                    // 收到消息是否立即启动应用，1为立即启动，2则广播等待客户端自启动
                template.TransmissionContent = pushMessage.MessageJson;  // 封装的消息json                
                template.IsRing = pushMessage.IsRing;					// 收到通知是否响铃，可选，默认响铃
                template.IsVibrate = pushMessage.IsVibrate;					// 收到通知是否震动，可选，默认振动
                template.IsClearable = pushMessage.IsClearable;				// 通知是否可清除，可选，默认可清除
                message.Data = template;
                //设置接收者
                List<com.igetui.api.openservice.igetui.Target> targetList = new List<com.igetui.api.openservice.igetui.Target>();
                foreach (var item in clientIDs)
                {
                    com.igetui.api.openservice.igetui.Target target = new com.igetui.api.openservice.igetui.Target();
                    target.appId = APPID;
                    target.clientId = item;
                    targetList.Add(target);
                }
                string contentId = push.getContentId(message);
                string pushResult = push.pushMessageToList(contentId, targetList);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }
        
        /// <summary>
        /// 透传
        /// </summary>
        private static void PushMessageToSingle()
        {
            // 推送主类
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);

            //通知模版：支持TransmissionTemplate、LinkTemplate、NotificationTemplate，此处以TransmissionTemplate为例
            TransmissionTemplate template = new TransmissionTemplate();
            template.AppId = APPID;                             // 应用APPID
            template.AppKey = APPKEY;                           // 应用APPKEY
            template.TransmissionType = "1";                    // 收到消息是否立即启动应用，1为立即启动，2则广播等待客户端自启动
            template.TransmissionContent = "您需要透传的内容";  // 需要透传的内容

            // 单推消息模型
            SingleMessage message = new SingleMessage();
            //message.IsOffline = true;                         // 用户当前不在线时，是否离线存储,可选
            //message.OfflineExpireTime = 3600 * 12;            // 离线有效时间，单位为毫秒，可选
            message.Data = template;

            com.igetui.api.openservice.igetui.Target target = new com.igetui.api.openservice.igetui.Target();
            target.appId = APPID;
            target.clientId = CLIENTID;

            String pushResult = push.pushMessageToSingle(message, target);
            System.Console.WriteLine("-----------PushMessageToSingle--------------" + pushResult);
        }

        //PushMessageToList接口测试代码
        private static void PushMessageToList()
        {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);

            ListMessage message = new ListMessage();

            NotificationTemplate template = new NotificationTemplate();
            template.AppId = APPID;                             // 应用APPID
            template.AppKey = APPKEY;                           // 应用APPKEY
            template.Title = "此处填写通知标题";                // 通知标题
            template.Text = "此处填写通知内容";                 // 通知内容
            template.Logo = "ic_launcher.png";                         // 应用Logo
            template.TransmissionType = "2";                    // 收到消息是否立即启动应用，1为立即启动，2则广播等待客户端自启动
            template.TransmissionContent = "你需要透传的内容";  // 需要透传的内容
            //template.IsRing = true;					// 收到通知是否响铃，可选，默认响铃
            //template.IsVibrate = true;					// 收到通知是否震动，可选，默认振动
            //template.IsClearable = true;				// 通知是否可清除，可选，默认可清除

            message.Data = template;

            //设置接收者
            List<com.igetui.api.openservice.igetui.Target> targetList = new List<com.igetui.api.openservice.igetui.Target>();
            com.igetui.api.openservice.igetui.Target target1 = new com.igetui.api.openservice.igetui.Target();
            target1.appId = APPID;
            target1.clientId = CLIENTID;

            // 如需要，可以设置多个接收者
            //com.igetui.api.openservice.igetui.Target target2 = new com.igetui.api.openservice.igetui.Target();
            //target2.AppId = APPID;
            //target2.ClientId = "ddf730f6cabfa02ebabf06e0c7fc8da0";

            targetList.Add(target1);
            //targetList.Add(target2);

            String contentId = push.getContentId(message);
            String pushResult = push.pushMessageToList(contentId, targetList);
            System.Console.WriteLine("-----------PushMessageToList--------------" + pushResult);
        }

        //pushMessageToApp接口测试代码
        private static void pushMessageToApp()
        {
            IGtPush push = new IGtPush(HOST, APPKEY, MASTERSECRET);

            AppMessage message = new AppMessage();

            //通知模版：支持TransmissionTemplate、LinkTemplate、NotificationTemplate，此处以LinkTemplate为例
            LinkTemplate template = new LinkTemplate();
            template.AppId = APPID;                     //应用APPID
            template.AppKey = APPKEY;                   // 应用APPKEY
            template.Title = "此处填写通知标题";        // 通知标题
            template.Text = "此处填写通知内容";         // 通知内容
            template.Logo = "ic_launcher.png";                 // 通知Logo  
            template.Url = "跳转的网页地址";            // 跳转地址
            //template.IsRing = true;					// 收到通知是否响铃，可选，默认响铃
            //template.IsVibrate = true;					// 收到通知是否震动，可选，默认振动
            //template.IsClearable = true;				// 通知是否可清除，可选，默认可清除


            message.Data = template;

            List<String> appIdList = new List<string>();
            appIdList.Add(APPID);

            List<String> phoneTypeList = new List<string>();    //通知接收者的手机操作系统类型
            phoneTypeList.Add("ANDROID");
            //phoneTypeList.Add("IPHONE");

            List<String> provinceList = new List<string>();     //通知接收者所在省份
            provinceList.Add("浙江");
            provinceList.Add("上海");
            provinceList.Add("北京");

            message.AppIdList = appIdList;
            message.PhoneTypeList = phoneTypeList;
            message.ProvinceList = provinceList;

            String pushResult = push.pushMessageToApp(message);
            System.Console.WriteLine("-----------pushMessageToApp--------------" + pushResult);
        }
    }
}
