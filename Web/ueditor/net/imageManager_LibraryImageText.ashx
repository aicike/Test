<%@ WebHandler Language="C#" Class="imageManager_LibraryImageText" %>
/**
 * Created by visual studio2010
 * User: xuheng
 * Date: 12-3-7
 * Time: 下午16:29
 * To change this template use File | Settings | File Templates.
 */
using System;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

public class imageManager_LibraryImageText : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };                //文件允许格式

        string action = context.Server.HtmlEncode(context.Request["action"]);

        if (action == "get")
        {
            String str = String.Empty;


            var account = context.Session[Poco.SystemConst.Session.LoginAccount] as Poco.Account;
            if (account == null)
            {
                false.NotAuthorizedPage();
            }

            var tempPath = Poco.SystemConst.Business.PathFileLibrary.Replace("~","");
            var path = string.Format(tempPath, account.CurrentAccountMainID);
            DirectoryInfo info = new DirectoryInfo(context.Server.MapPath(path));

            //目录验证
            if (info.Exists)
            {
                foreach (FileInfo fi in info.GetFiles())
                {
                    if (Array.IndexOf(filetype, fi.Extension.ToLower()) != -1)
                    {
                        str += path + "/"  + fi.Name + "ue_separate_ue";
                    }
                }
                
                
                DirectoryInfo[] infoArr = info.GetDirectories();
                foreach (DirectoryInfo tmpInfo in infoArr)
                {
                    foreach (FileInfo fi in tmpInfo.GetFiles())
                    {
                        if (Array.IndexOf(filetype, fi.Extension.ToLower()) != -1)
                        {
                            str += path + "/" + tmpInfo.Name + "/" + fi.Name + "ue_separate_ue";
                        }
                    }
                }
            }

            context.Response.Write(str);
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