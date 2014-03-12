using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;

namespace System
{
    public static partial class ExpandMethod
    {
        public static string ObjectToJson<T>(this IList<T> list, string jsonName = null)
        {
            if (list == null)
            {
                throw new NullReferenceException();
            }
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            if (!string.IsNullOrEmpty(jsonName))
            {
                json = string.Format("{{\"{0}\":{1}}}", jsonName, json);
            }
            return json;
        }

        public static T JsonToObject<T>(this string json)
        {
            if (json == null)
            {
                throw new NullReferenceException();
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 字符串转成整型数组
        /// </summary>
        public static int[] ConvertToIntArray(this string[] value)
        {
            if (value == null)
            {
                return null;
            }
            int[] array = new int[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] = Convert.ToInt32(value[i]);
            }
            return array;
        }

        /// <summary>
        /// 集合拼接成字符串
        /// </summary>
        public static string ConvertToString<T>(this IList<T> list, string split)
        {
            StringBuilder str = new StringBuilder();
            foreach (var item in list)
            {
                str.AppendFormat("{0}{1}", item, split);
            }
            string value = str.ToString();
            value = value.Remove(value.Length - split.Length);
            return value;
        }

        public static int[] ConvertToIntArray(this string value, char split)
        {
            if (string.IsNullOrEmpty(value) == false)
            {
                string[] str_array = value.Split(new char[] { split }, StringSplitOptions.RemoveEmptyEntries);
                int[] array = new int[str_array.Length];
                for (int i = 0; i < str_array.Length; i++)
                {
                    array[i] = int.Parse(str_array[i]);
                }
                return array;
            }
            return null;
        }

        /// <summary>
        /// 截取字符串,只显示设置长度的文字，超出用token字符串显示
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string Show(this string value, int length, string token = "")
        {
            if (value.Length <= length)
            {
                return value;
            }
            value = value.Substring(0, length) + token;
            return value;
        }

        /// <summary>
        /// false 发生没有权限异常
        /// </summary>
        /// <param name="value"></param>
        public static void NotAuthorizedPage(this bool value, string message = null)
        {
            if (!value)
            {
                if (message != null)
                {
                    throw new ApplicationException(message);
                }
                else
                {
                    throw new ApplicationException(SystemConst.Notice.NotAuthorized);
                }
            }
        }

        /// <summary>
        /// 根据指定日期格式显示日期
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string DateFormat(this DateTime datetime)
        {
            return datetime.ToString("MM-dd hh:mm");
        }

        public static string DefaultHeadImage(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return SystemConst.Business.DefaultHeadImage;
            }
            return path;
        }

        /// <summary>
        /// 获取字符窜后缀名
        /// </summary>
        /// <param name="path"></param>
        /// <returns>.xxx</returns>
        public static string GetFileSuffix(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }
            else
            {
                return path.Substring(path.LastIndexOf('.'));
            }
        }

        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns>.xxx</returns>
        public static string GetFileName(this string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "";
            }
            else
            {
                return path.Substring(path.LastIndexOf("\\")+1);
            }
        }

        /// <summary>
        /// 解密token为id值
        /// </summary>
        /// <param name="id_token"></param>
        /// <returns></returns>
        public static int TokenDecrypt(this string id_token)
        {
            var value = Common.DESEncrypt.Decrypt(id_token);
            int id = 0;
            bool isOk = int.TryParse(value, out id);
            return id;
        }

        /// <summary>
        /// 加密id为token值(同时把URL进行编码)
        /// </summary>
        /// <param name="id_token"></param>
        /// <returns></returns>
        public static string TokenEncrypt(this int id_token)
        {
            var value = Common.DESEncrypt.Encrypt(id_token+"");
            value = HttpUtility.UrlEncodeUnicode(value);
            return value;
        }

        /// <summary>
        /// 加密id为token值(不把URL进行编码)
        /// </summary>
        /// <param name="id_token"></param>
        /// <param name="isNoUrlEncode"></param>
        /// <returns></returns>
        public static string TokenEncrypt(this int id_token,bool isNoUrlEncode=false)
        {
            var value = Common.DESEncrypt.Encrypt(id_token + "");
            return value;
        }

    }
}