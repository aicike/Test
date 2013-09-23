using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Poco;
using System.Web.SessionState;
using System.Reflection;

namespace Web.Models
{
    public class SessionSharedHttpModule : IHttpModule
    {
        string _rootDomain = null; //一级域名


        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            _rootDomain = SystemConst.WebUrl; //一级域名赋值

            //去除一级域名以外信息（将www.dhlx.cn改为dhlx.cn，如果一级域名不是常量赋值的话）
            //_rootDomain = _rootDomain.Substring(_rootDomain.LastIndexOf('.', _rootDomain.LastIndexOf('.') - 1) + 1);

            //要实现会话共享还得修改OutOfProcSessionStateStore类下的一个私有的静态字段s_uribase
            //OutOfProcSessionStateStore的声明为：
            //internal sealed class OutOfProcSessionStateStore : SessionStateStoreProviderBase
            //s_uribase的声明为：
            //static string       s_uribase;
            //关于OutOfProcSessionStateStore类以及s_uribase字段的内容请查阅OutOfProcStateClientManager.cs文件
            //文件路径：Framework源代码\V2.0.5727\dd\ndp\fx\src\xsp\System\Web\State\OutOfProcStateClientManager.cs
            Type stateServerSessionProvider = typeof(HttpSessionState).Assembly.GetType("System.Web.SessionState.OutOfProcSessionStateStore");
            FieldInfo uriField = stateServerSessionProvider.GetField("s_uribase", BindingFlags.Static | BindingFlags.NonPublic);
            if (uriField == null)
                throw new ArgumentException("UriField was not found");

            uriField.SetValue(null, _rootDomain);
            context.EndRequest += new EventHandler(context_EndRequest);

        }

        /// <summary>
        /// 从发送给客户端的Cookie集合中找出记录会话ID的Cookie
        /// 并修改它的Domain属性值为要共享的一级域名
        /// </summary>

        void context_EndRequest(object sender, System.EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            for (int i = app.Context.Response.Cookies.Count - 1; i >= 0; i--)
            {

                //ASP.NET_SessionId是默认的存储会话ID的key，如果修改了默认值这里要修改成一致的
                if (app.Context.Response.Cookies[i].Name.Equals("ASP.NET_SessionId"))
                {
                    app.Context.Response.Cookies[i].Domain = _rootDomain;
                    return;
                }
            }
        }
    }
}