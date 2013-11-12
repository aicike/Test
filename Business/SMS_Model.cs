using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using agsXMPP.protocol.client;
using System.Threading;
using Poco;
using Injection;
using Interface;

namespace Business
{
    public class SMS_Model
    {
        private static XmppClientConnection Connection;
        private static string webXMPP_json = "imtimely_sms_web";
        private static string appXMPP_json = "imtimely_sms_app";

        public Result Send_UserRegister(int userLoginID) {
            var userLoginModel= Factory.Get<IUserLoginInfoModel>(SystemConst.IOC_Model.UserLoginInfoModel);
            var userLoginInfo = userLoginModel.Get(userLoginID);
            Result result = new Result();
            if (userLoginInfo == null) {
                result.Error = "参数有误，未找到该账号。";
                return result;
            }
            string sms = string.Format("恭喜您成功开通Imtimely App,登录账号为您的手机号码。");
            result = SendSMS(userLoginInfo.Phone, sms);
            return result;
        }

        public Result SendSMS(string phone, string content)
        {
            LoginXMPP();
            Thread.Sleep(1000);
            try
            {
                Result result = new Result();
                if (Connection != null)
                {
                    agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
                    msg.Type = MessageType.chat;
                    msg.From = new Jid(webXMPP_json, "localhost", "resource");
                    msg.To = new Jid(appXMPP_json, "localhost", webXMPP_json);

                    XMPP_Body_SMS body = new XMPP_Body_SMS();
                    body.Content = content;
                    body.Phone = phone;
                    msg.AddChild(body);
                    Connection.Send(msg);
                    Presence p = new Presence();
                    p.Type = PresenceType.unavailable;
                    Connection.Send(p);
                }
                else
                {
                    result.Error = "SMS发送失败。";
                }
                return result;
            }
            catch (Exception ex)
            {
                //发送失败
                return new Result() { Error = "SMS发送失败" };
            }
        }


        //登陆XMPP
        private void LoginXMPP()
        {
            //连接xmpp
            if (Connection == null)
            {
                System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
                {
                    Connection = new XmppClientConnection();
                    //登陆方法
                    Connection.OnLogin += new ObjectHandler((object sender) =>
                    {

                        Connection.Show = ShowType.chat;
                        Connection.SendMyPresence();
                    }
                    );
                    //Connection.OnMessage += new XmppClientConnection.MessageHandler(con_OnMessage);
                    //出息消息
                    Connection.Username = webXMPP_json;
                    Connection.Server = "127.0.0.1";
                    Connection.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["XMPP_SMS"]);
                    Connection.Resource = "web";
                    Thread mythread = new Thread(() => { Connection.Open(); });
                    mythread.Start();
                    mythread.IsBackground = true;
                });
                t.Start();
            }
            else {
                if (Connection.Status=="") {
                    Thread mythread = new Thread(() => { Connection.Open(); });
                    mythread.Start();
                    mythread.IsBackground = true;
                }
            }
        }
    }
}
