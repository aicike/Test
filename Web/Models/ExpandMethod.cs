using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web;
using Poco.Enum;

namespace System
{
    public static partial class ExpandMethod
    {
        /// <summary>
        /// 加密id为token值(同时把URL进行编码) true 为不编码
        /// </summary>
        /// <param name="id_token"></param>
        /// <returns></returns>
        public static string EnumDataStatusGetValue(this int EnumDataStatus)
        {
            string value = null;
            switch ((EnumDataStatus)EnumDataStatus) { 
                case Poco.Enum.EnumDataStatus.Disabled:
                    value = "审核未通过";
                    break;
                case Poco.Enum.EnumDataStatus.Enabled:
                    value = "审核通过";
                    break;
                case Poco.Enum.EnumDataStatus.None:
                    value = "未提交审核";
                    break;
                case Poco.Enum.EnumDataStatus.WaitPayMent:
                    value = "等待付款";
                    break;
            }
            return value;
        }


    }
}