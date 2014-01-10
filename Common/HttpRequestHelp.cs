using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Common
{
    public class HttpRequestHelp
    {
        public string QueryStringForPost(string url, params HttpRequestParam[] param)
        {
            HttpWebResponse res = null;
            string strresult = "";
            try
            {

                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "post";
                req.KeepAlive = true;
                req.ContentType = "application/x-www-form-urlencoded";
                //CookieContainer cookiecon = new CookieContainer();
                //req.CookieContainer = cookiecon;
                //req.CookieContainer.SetCookies(new Uri(url),cookieheader);


                if (param != null)
                {

                    byte[] somebytes = null;

                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < param.Length; i++)
                    {
                        if (i == 0)
                        {
                            sb.AppendFormat("{0}={1}", param[i].Key, param[i].Value.ToString());
                        }
                        else
                        {
                            sb.AppendFormat("&{0}={1}", param[i].Key, param[i].Value.ToString());
                        }
                    }
                    somebytes = Encoding.UTF8.GetBytes(sb.ToString());
                    req.ContentLength = somebytes.Length;
                    Stream newstream = req.GetRequestStream();
                    newstream.Write(somebytes, 0, somebytes.Length);
                    newstream.Close();
                }
                else
                {
                    req.ContentLength = 0;
                }
                res = (HttpWebResponse)req.GetResponse();
                Stream receivestream = res.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader sr = new StreamReader(receivestream, encode);
                char[] read = new char[256];
                int count = sr.Read(read, 0, 256);
                while (count > 0)
                {
                    string str = new string(read, 0, count);
                    strresult += str;
                    count = sr.Read(read, 0, 256);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (res != null)
                {
                    res.Close();
                }
            }
            return strresult;
        }
        public T QueryStringForPost<T>(string url, params HttpRequestParam[] param) where T : class
        {
            try
            {
                string value = QueryStringForPost(url, param);
                if (!string.IsNullOrEmpty(value))
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }


    public class HttpRequestParam
    {
        public HttpRequestParam(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        public string Key { get; set; }
        public object Value { get; set; }
    }
}
