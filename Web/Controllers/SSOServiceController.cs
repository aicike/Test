using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroSite_Web.Controllers
{
    public class SSOServiceController : Controller
    {
        public ActionResult Index()
        {
            string backURL = Server.UrlDecode(Request.QueryString["BackURL"]);

            //获取Cookie
            HttpCookie tokenCookie = Request.Cookies["Token"];
            if (tokenCookie != null)
            {
                backURL = backURL.Replace("$Token$", tokenCookie.Values["Value"].ToString());
            }
            return Redirect(backURL);
        }
    }
}
