using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_VIPInfo
    {
        public int ID { get; set; }

        ///卡号
        public string CardNumber { get; set; }

        //余额
        public decimal Balance { get; set; }

        //开卡日期
        public string CreateDate { get; set; }

        //vip类型
        public string VIPType { get; set; }

        //积分
        public int score { get; set; }

        //状态
        public string Status { get; set; }
    }
}
