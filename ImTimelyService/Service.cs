using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using AcceptanceServer.DataOperate;
using agsXMPP;
using System.Threading;
using agsXMPP.protocol.client;
using Poco;
using System.Configuration;

namespace ImTimelyService
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }
        private static XmppClientConnection Connection;
        private static string webXMPP_json = "imtimely_sms_web";
        private static string appXMPP_json = "imtimely_sms_app";

        protected override void OnStart(string[] args)
        {
            Timer1Min.Start();
            Timer5Min.Start();
        }

        protected override void OnStop()
        {
        }


        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        public void SetLog(string Str)
        {
            try
            {


                string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string filePath = "D:\\log";
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                FileStream aFile = new FileStream(filePath + "\\" + fileName, FileMode.Append);
                StreamWriter sw = new StreamWriter(aFile);
                sw.WriteLine("*************************文本****************************");
                sw.WriteLine("【出现时间】：" + DateTime.Now.ToString());

                sw.WriteLine("【内容】：" + Str);

                sw.WriteLine("************************************************************");
                sw.Close();
                aFile.Close();
            }
            catch { }
        }



        private void Timer5Min_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

        }

        static string ActivityDate = "";
        /// <summary>
        ///  1分钟执行一次
        /// </summary>
        private void Timer1Min_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //活动提前一天发送短信提醒 12点
            if (DateTime.Now.ToString("HH") == "12")
            {
                if (ActivityDate != DateTime.Now.ToString("dd"))
                {
                    ActivityDate = DateTime.Now.ToString("dd");

                }
            }


        }

        //活动提前一天发送短信提醒 
        public void SendSMS()
        {
            System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
            {
                string sql = @"select a.id,a.Title, a.ActivityStratDate,b.Phone,b.name,c.Name as AName from ActivityInfo a, dbo.ActivityInfoParticipator b,accountMain c where a.id =b.ActivityInfoID and a.accountMainid = c.id
                                and CONVERT(varchar(100), a.ActivityStratDate, 23)= CONVERT(varchar(100), (GetDate()+1), 23)";
                DataSet ds = SqlHelper.ExecuteDataset(sql);

                if (ds != null)
                {
                    SendSMS_Activity(ds);
                }
               
            });
            t.Start();

        }


         //处理活动提醒
        public void SendSMS_Activity(DataSet ds)
        {
            LoginXMPP();
            Thread.Sleep(1000);
            string webUrl = ConfigurationManager.AppSettings["WebUrl"].ToString();
            try
            {
                if (Connection != null)
                {
                    string content = "";
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        try
                        {
                            string dates = row["ActivityStratDate"].ToString();
                            content = string.Format("您好，{0}先生/女士。您参与的活动\"{1}\" 与于明日{2}开始。地址：{3}/default/News?id={4}  【{5}】"
                                                     , row["Phone"], row["Title"], dates.Substring(dates.LastIndexOf(' ')),webUrl,row["id"],row["AName"] );
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
                   //"SMS发送失败。";
                }
            }
            catch (Exception ex)
            {
                //发送失败
               
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
}
