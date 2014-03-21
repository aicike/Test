using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;
using Business.Imtimely_ShortURL;
using System.Threading;

namespace System
{
    public static partial class ExpandMethod
    {
        /// <summary>
        /// 获取短URL
        /// </summary>
        /// <param name="id_token"></param>
        /// <returns></returns>
        public static string ConvertToShortURL(this string fullURL)
        {
            string shortURL = null;
            try
            {
                ShortURLServiceClient susc = new ShortURLServiceClient();
                string[] value = susc.GetURL(fullURL);
                if (value == null || value[0] != null)
                {
                    shortURL = fullURL;
                }
                else
                {
                    shortURL = value[1];
                }
                susc.Close();
            }
            catch (Exception ex)
            {

            }
            return shortURL;
        }


        public delegate void AsyncEventHandler();

        /// <summary>
        /// 删除短URL
        /// </summary>
        /// <param name="shortURL"></param>
        /// <returns></returns>
        public static void DeleteShortURL(this string shortURL)
        {
            try
            {
                shortURL = shortURL.Substring(shortURL.LastIndexOf('/') + 1);
                ShortURLServiceClient susc = new ShortURLServiceClient();
                susc.BeginDeleteShortURL(shortURL, (IAsyncResult) =>
                {
                    susc.Close();
                }, null);
                susc.DeleteShortURL(shortURL);
            }
            catch (Exception ex)
            {

            }
        }
    }
}