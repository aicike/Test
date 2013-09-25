using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;

namespace Web.Controllers
{
    public class WebRequest_OtherController : Controller
    {
        public string GetQRCode(int amid)
        {
            string androidUrl = string.Format("{0}/Download/{1}/android.jpg", SystemConst.WebUrl, amid);
            string iosUrl = string.Format("{0}/Download/{1}/ios.jpg", SystemConst.WebUrl, amid);
            return Newtonsoft.Json.JsonConvert.SerializeObject(new { a = androidUrl, i = iosUrl });
        }
    }
}
