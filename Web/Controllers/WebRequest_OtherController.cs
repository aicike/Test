using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poco;
using Injection;
using Interface;
using Poco.WebAPI_Poco;

namespace Web.Controllers
{
    public class WebRequest_OtherController : Controller
    {
        public string GetQRCode(int amid)
        {
            string androidUrl = string.Format("{0}/Download/{1}/android.jpg", SystemConst.WebUrlIP, amid);
            string iosUrl = string.Format("{0}/Download/{1}/ios.jpg", SystemConst.WebUrlIP, amid);
            return Newtonsoft.Json.JsonConvert.SerializeObject(new { a = androidUrl, i = iosUrl });
        }

        /// <summary>
        /// 获取图文素材
        /// </summary>
        /// <param name="amid">AccountMainID</param>
        public string GetLibraryImageText(int amid)
        {
            var libraryImageTextModel = Factory.Get<ILibraryImageTextModel>(SystemConst.IOC_Model.LibraryImageTextModel);
            var list = libraryImageTextModel.GetLibraryList(amid).ToList();
            List<App_ImageText> appList = new List<App_ImageText>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    App_ImageText app = new App_ImageText();
                    app.ID = item.ID;
                    app.I = item.ImagePath;
                    app.T = item.Title;
                    app.S = item.Summary;
                    app.C = item.Content;
                    if (item.LibraryImageTexts != null && item.LibraryImageTexts.Count > 0)
                    {
                        List<App_ImageText> appSubList = new List<App_ImageText>();
                        foreach (var sub in item.LibraryImageTexts)
                        {
                            App_ImageText appSub = new App_ImageText();
                            appSub.ID = sub.ID;
                            appSub.I = sub.ImagePath;
                            appSub.T = sub.Title;
                            appSub.S = sub.Summary;
                            appSub.C = sub.Content;
                            appSubList.Add(appSub);
                        }
                        app.Sub = appSubList;
                    }
                    appList.Add(app);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(appList);
        }
    }
}
