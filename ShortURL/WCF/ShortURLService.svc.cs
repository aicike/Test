using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ShortURL.Business;

namespace ShortURL.WCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ShortURLService”。
    public class ShortURLService : IShortURLService
    {
        public string[] GetURL(string fullUrl)
        {
            URLMapModel model = new URLMapModel();
            string shrotURL = null;
            string error = model.SaveShorUrl(fullUrl, out shrotURL);
            string url = System.Configuration.ConfigurationManager.AppSettings["WebUrl"];
            shrotURL = string.Format("http://url.{0}/{1}", url, shrotURL);

            return new string[] { error, shrotURL };
        }

        public void DeleteShortURL(string shortURL)
        {
            URLMapModel model = new URLMapModel();
            model.DeleteShortURL(shortURL);
        }
    }
}
