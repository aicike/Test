using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class App_ImageText
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
        /// Summary
        /// </summary>
        public string S { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public string C { get; set; }

        public List<App_ImageText> Sub { get; set; }
    }
}
