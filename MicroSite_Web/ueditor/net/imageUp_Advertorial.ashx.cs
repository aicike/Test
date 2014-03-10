using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Poco;

namespace Web.ueditor.net
{
    /// <summary>
    /// imageUp_Advertorial 的摘要说明
    /// </summary>
    public class imageUp_Advertorial : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";

            //上传配置
            int size = 2;           //文件大小限制,单位MB                             //文件大小限制，单位MB
            string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };         //文件允许格式


            //上传图片
            Hashtable info = new Hashtable();
            Uploader_Advertorial up = new Uploader_Advertorial();

            var account = context.Session[Poco.SystemConst.Session.LoginAccount] as Poco.Account;
            if (account == null)
            {
                false.NotAuthorizedPage();
            }


            var tempPath = Poco.SystemConst.Business.PathBase.Replace("~", "");
            var VirtualPath = string.Format(tempPath, account.CurrentAccountMainID);
            var path = "";
            //集成微网站
            if (SystemConst.IsIntegrationWebProject)
            {
                path = string.Format(SystemConst.IntegrationPathBase, account.CurrentAccountMainID);
            }
            //不是集成微网站
            else
            {
                path = string.Format(tempPath, account.CurrentAccountMainID);
            }


            info = up.upFile(context, path, filetype, size, VirtualPath);                   //获取上传状态


            string title = up.getOtherInfo(context, "pictitle");                   //获取图片描述
            string oriName = up.getOtherInfo(context, "fileName");                //获取原始文件名


            string Url =  Poco.SystemConst.WebUrlIP;

            //集成微网站
            if (SystemConst.IsIntegrationWebProject)
            {

            }
            //不是集成微网站
            else {
                HttpContext.Current.Response.Write("{'url':'" + Url + info["url"] + "','title':'" + title + "','original':'" + oriName + "','state':'" + info["state"] + "'}");  //向浏览器返回数据json数据
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