using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_User
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string NameNote { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string HeadImagePath { get; set; }

        public string Pwd { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GN { get; set; }

        public string SEX { get; set; }

        public int? Age { get; set; }

        public string IDCard { get; set; }

        public string Address { get; set; }
    }
}
