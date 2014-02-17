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
using Common;
using System.Text.RegularExpressions;
using System.Data;

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
            Regex regex = new Regex(@"(1[3,5,8][0-9])\d{8}");
            bool isOk = regex.IsMatch(userLoginInfo.Phone);
            if (isOk == false)
            {
                result.Error = "电话号码格式错误，无法发送短信。";
                return result;
            }
            string sms = string.Format("恭喜您成功开通Imtimely App,登录账号为您的手机号码，http://imtimely.com。");
            result = SendSMS(userLoginInfo.Phone, sms);
            return result;
        }

        public Result Send_AccountRegister(int accountID) {
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = accountModel.Get(accountID);
            Result result = new Result();
            if (account == null )
            {
                result.Error = "参数有误，未找到该账号。";
                return result;
            }
            if (string.IsNullOrEmpty(account.Phone)) {
                result.Error = "该账号未配置电话号码。";
                return result;
            }
            Regex regex = new Regex(@"(1[3,5,8][0-9])\d{8}");
            bool isOk = regex.IsMatch(account.Phone);
            if (isOk==false)
            {
                result.Error = "该账号配置的电话号码格式错误，无法发送短信。";
                return result;
            }
            var pwd= DESEncrypt.Decrypt(account.LoginPwd);
            string sms = string.Format("恭喜您成功开通Imtimely App,登录账号为您的手机号码或邮箱，您的密码为{0}，web端登录地址 http://imtimely.com。", pwd);
            result = SendSMS(account.Phone, sms);
            return result;
        }

        /// <summary>
        /// 密码重置
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public Result Send_AccountRegisterPWD(int accountID)
        {
            var accountModel = Factory.Get<IAccountModel>(SystemConst.IOC_Model.AccountModel);
            var account = accountModel.Get(accountID);
            Result result = new Result();
            if (account == null)
            {
                result.Error = "参数有误，未找到该账号。";
                return result;
            }
            if (string.IsNullOrEmpty(account.Phone))
            {
                result.Error = "该账号未配置电话号码。";
                return result;
            }
            Regex regex = new Regex(@"(1[3,5,8][0-9])\d{8}");
            bool isOk = regex.IsMatch(account.Phone);
            if (isOk == false)
            {
                result.Error = "该账号配置的电话号码格式错误，无法发送短信。";
                return result;
            }
            var pwd = DESEncrypt.Decrypt(account.LoginPwd);
            string sms = string.Format("您的Imtimely密码重置成功,登录账号为您的手机号码或邮箱，您的密码为{0}，http://imtimely.com。", pwd);
            result = SendSMS(account.Phone, sms);
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

        //处理活动提醒
        public Result SendSMS_Activity(DataSet ds)
        {
            LoginXMPP();
            Thread.Sleep(1000);
            try
            {
                Result result = new Result();
                if (Connection != null)
                {
                    string content = "";
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        try
                        {
                            string dates = row["ActivityStratDate"].ToString();
                            content = string.Format("您好，{0}先生/女士。您参与的活动\"{1}\" 与于明日{2}开始。地址：http://imtimely.com/default/News?id={3}  【{4}】"
                                                     , row["Phone"], row["Title"], dates.Substring(dates.LastIndexOf(' ')),row["id"] );
                            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
                            msg.Type = MessageType.chat;
                            msg.From = new Jid(webXMPP_json, "localhost", "resource");
                            msg.To = new Jid(appXMPP_json, "localhost", webXMPP_json);
                            XMPP_Body_SMS body = new XMPP_Body_SMS();
                            body.Content = content;
                            body.Phone = row["Phone"].ToString();
                            msg.AddChild(body);
                            Connection.Send(msg);
                        }
                        catch { }
                    }

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
