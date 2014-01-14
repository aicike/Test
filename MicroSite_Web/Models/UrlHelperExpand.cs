using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;

namespace System.Web.Mvc
{
    public static class UrlHelperExpand
    {
        public static string RouteUrl(this UrlHelper Url, string routeName, dynamic routeValues, bool useDomain)
        {
            string url = Url.RouteUrl(routeName, routeValues);// new { action = "Index", controller = "Home", HostName = account.HostName });
            if (useDomain)
            {
                try
                {
                    string hostName = routeValues.HostName;
                    url = "http://" +hostName + "." + SystemConst.WebUrl + url;
                }
                catch (Exception ex)
                {
                    throw new Exception("HostName没有配置，需要修改。", ex);
                }
            }
            return url;
        }
    }
}