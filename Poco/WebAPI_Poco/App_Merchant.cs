using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_Merchant
    {
        public int ID { get; set; }

        //商户名称
        public string Name { get; set; }
        //商户地址
        public string Address { get; set; }

        //电话
        public string Phone { get; set; }

        //商家LOGO
        public string Logo{ get; set; }

        //简介
        public string Introduction { get; set; }
    }
}