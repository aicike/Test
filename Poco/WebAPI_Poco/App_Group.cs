using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_Group
    {
        public int ID { get; set; }

        public string GN { get; set; }

        public List<App_User> UL { get; set; }
    }
}
