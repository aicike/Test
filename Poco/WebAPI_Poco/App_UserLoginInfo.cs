﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_UserLoginInfo
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Pwd { get; set; }

        public int AccountMainID { get; set; }

        public string IMEI { get; set; }

        public int? AccountID { get; set; }

        public string Phone { get; set; }

        public int? UserID { get; set; }

        /// <summary>
        /// 客户端系统类型
        /// </summary>
        public int EnumClientSystemType { get; set; }

        /// <summary>
        /// 客户端账号类型
        /// </summary>
        public int EnumClientUserType { get; set; }

        /// <summary>
        /// 客户端ID
        /// </summary>
        public string ClientID { get; set; }
    }
}
