﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class App_Voice
    {
        public int ID { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string T { get; set; }

        /// <summary>
        /// ImagePath
        /// </summary>
        public string I { get; set; }

        /// <summary>
        /// FileLength
        /// </summary>
        public string L { get; set; }
    }
}
