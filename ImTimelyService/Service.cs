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
using Business;

namespace ImTimelyService
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

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
            Task t = new Task(() =>
            {
                string sql = @"select a.id,a.Title, a.ActivityStratDate,b.Phone,b.name,c.Name as AName from ActivityInfo a, dbo.ActivityInfoParticipator b,accountMain c where a.id =b.ActivityInfoID and a.accountMainid = c.id
                                and CONVERT(varchar(100), a.ActivityStratDate, 23)= CONVERT(varchar(100), (GetDate()+1), 23)";
                DataSet ds = SqlHelper.ExecuteDataset(sql);

                string content = "";
                if (ds != null)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    { 
                           
                    }
                }
                SMS_Model sms = new SMS_Model();
                sms.SendSMS_Activity(ds);
            });
            t.Start();

        }


    }
}
