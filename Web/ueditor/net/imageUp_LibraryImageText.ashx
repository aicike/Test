<%@ WebHandler Language="C#" Class="imageUp_LibraryImageText" %>
<%@ Assembly Src="Uploader.cs" %>

using System;
using System.Web;
using System.IO;
using System.Collections;

public class imageUp_LibraryImageText : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        context.Response.ContentType = "text/plain";

        //上传配置
        int size = 2;           //文件大小限制,单位MB                             //文件大小限制，单位MB
        string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };         //文件允许格式


        //上传图片
        Hashtable info = new Hashtable();
        Uploader up = new Uploader();

        var account = context.Session[Poco.SystemConst.Session.LoginAccount] as Poco.Account;
        if (account == null)
        {
            false.NotAuthorizedPage();
        }


        var tempPath = Poco.SystemConst.Business.PathFileLibrary.Replace("~", "");
        var path = string.Format(tempPath, account.CurrentAccountMainID);

        info = up.upFile(context, path, filetype, size);                   //获取上传状态

        string title = up.getOtherInfo(context, "pictitle");                   //获取图片描述
        string oriName = up.getOtherInfo(context, "fileName");                //获取原始文件名

        try
        {
            //上传到图片库
            var libraryImageModel = Injection.Factory.Get<Interface.ILibraryImageModel>(Poco.SystemConst.IOC_Model.LibraryImageModel);
            Poco.LibraryImage entity = new Poco.LibraryImage();
            entity.FileName = title;
            entity.FilePath = info["url"].ToString();
            entity.AccountMainID = account.CurrentAccountMainID;
            libraryImageModel.Add(entity);
        }
        catch (Exception ex)
        {
        }

        string Url = "http://" + Poco.SystemConst.WebUrlIP;

        HttpContext.Current.Response.Write("{'url':'" +Url+ info["url"] + "','title':'" + title + "','original':'" + oriName + "','state':'" + info["state"] + "'}");  //向浏览器返回数据json数据
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}