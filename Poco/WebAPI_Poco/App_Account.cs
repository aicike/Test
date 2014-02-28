using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_Account
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string HeadImagePath { get; set; }

        public string Email { get; set; }

        public string Pwd { get; set; }

        public string Phone { get; set; }

        public string Role { get; set; }

        public int AccountMainID { get; set; }
    }
}
