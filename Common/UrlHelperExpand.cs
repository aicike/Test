using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using System.Web.Routing;
using System.IO;

namespace System.Web.Mvc
{
    public static class UrlHelperExpand
    {
        public static string RouteUrl(this UrlHelper Url, string routeName, bool useDomain, RouteValueDictionary routeValues)
        {
            string url = Url.RouteUrl(routeName, routeValues);// new { action = "Index", controller = "Home", HostName = account.HostName });
            if (useDomain)
            {
                try
                {
                    string hostName = routeValues["HostName"] as string;
                    url = "http://" + hostName + "." + SystemConst.WebUrl + url;
                }
                catch (Exception ex)
                {
                    throw new Exception("HostName没有配置，需要修改。", ex);
                }
            }
            return url;
        }

        /// <summary>
        /// 多项目之间切换时，会遇到二级域名影响，造成无法加载图片，js，css等文件，需要使用此方法进行修复文件路径
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="contentPath"></param>
        /// <param name="isFixPath">是否修复</param>
        /// <returns></returns>
        public static string Content(this UrlHelper Url, string contentPath, bool isFixPath)
        {
            string url = null;
            if (isFixPath)
            {
                url = string.Format("http://{0}{1}", SystemConst.IntegrationWebUrl, Url.Content(contentPath));
            }
            else
            {
                url = Url.Content(contentPath);
            }
            return url;
        }

        /// <summary>
        /// 检查物理文件是否存在，不存在则返回提供的文件
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="contentPath">需要检查的文件</param>
        /// <param name="newFilePath">替换的文件</param>
        /// <returns></returns>
        public static string Content(this UrlHelper Url, string contentPath, string newFilePath)
        {
            string filePath = HttpContext.Current.Server.MapPath(contentPath);
            if (File.Exists(filePath) == false)
            {
                return newFilePath;
            }
            return contentPath;
        }
    }
}