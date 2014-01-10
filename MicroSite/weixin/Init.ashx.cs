using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Poco;
using System.Text;
using System.Web.Security;

namespace MicroSite.weixin
{
    /// <summary>
    /// Init 的摘要说明
    /// </summary>
    public class Init : IHttpHandler
    {
        //自定义的TOken 
        public string Token = "imtimely";

        public bool isReturnNum = true;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";


            bool is0k = false;
            EmailInfo emailInfo = new EmailInfo();
            emailInfo.To = "176534021@qq.com;";
            emailInfo.Subject = "微信开发者信息";
            try
            {
                var signature = context.Request["signature"];//微信加密签名，signature结合了开发者填写的token参数和请求中的timestamp参数、nonce参数。
                var timestamp = context.Request["timestamp"];//时间戳
                var nonce = context.Request["nonce"];//随机数
                var echostr = context.Request["echostr"];//随机字符串

                //signature = "72a0424232ad07bd368c433d14636dff26dadb39";
                //timestamp = "1389176526";
                //nonce = "1388323802";
                //echostr = "5962805335827490510";


                is0k = checkAuthentication(Token, timestamp, nonce, signature);
                string value = "方法返回结果：" + is0k + " [signature=" + signature + "] " + " [timestamp=" + timestamp + "] " + " [nonce=" + nonce + "] " + " [echostr=" + echostr + "] ";


                //emailInfo = new EmailInfo();
                //emailInfo.To = "176534021@qq.com;";
                //emailInfo.Subject = "微信开发者信息";
                //emailInfo.IsHtml = false;
                //emailInfo.Body = value;
                //SendEmail.SendMailAsync(emailInfo);

                if (is0k && !string.IsNullOrEmpty(echostr))
                {
                    context.Response.Write(echostr);
                }
            }
            catch (Exception ex)
            {
                //emailInfo = new EmailInfo();
                //emailInfo.To = "176534021@qq.com;";
                //emailInfo.Subject = "微信开发者信息";
                //emailInfo.IsHtml = false;
                //emailInfo.Body = ex.Message;
                //SendEmail.SendMailAsync(emailInfo);
            }
        }


        /**
    *  Function:微信验证方法
    *  @author JLC
    *  @param signature 微信加密签名
    *  @param timestamp 时间戳
    *  @param nonce     随机数
    *  @param echostr   随机字符串
    *  @return
    */
        private bool checkAuthentication(string token, string timestamp, string nonce, string signature)
        {
            // 将获取到的参数放入数组
            string[] ArrTmp = { Token, timestamp, nonce };
            // 按微信提供的方法，对数据内容进行排序
            var array = ArrTmp.OrderBy(a => a).ToArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Count(); i++)
            {
                sb.Append(array[i]);
            }
            // 对排序后的字符串进行SHA-1加密
            // string pwd = SHA1Encrypt(sb.ToString());
            string pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(sb.ToString(), "SHA1");
            if (pwd.Equals(signature, StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}