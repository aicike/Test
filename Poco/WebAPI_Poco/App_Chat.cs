using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_Chat
    {
        public int ID { get; set; }

        public DateTime SendTime { get; set; }

        public string Message { get; set; }

        public string MessageType { get; set; }

        public bool IsRead { get; set; }

        public int SenderID { get; set; }

        public string SenderType { get; set; }
    }
}
