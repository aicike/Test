using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

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
    }
}