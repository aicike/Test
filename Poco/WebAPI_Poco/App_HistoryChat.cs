using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    /// <summary>
    /// 主历史会话
    /// </summary>
    public class App_HistoryChat
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public String UserName { get; set; }

        public int UserID { get; set; }
    }
}
