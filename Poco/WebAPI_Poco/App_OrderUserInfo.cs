using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_OrderUserInfo
    {
        public int ID { get; set; }

        public int ProvinceID { get; set; }

        public string Province { get; set; }

        public int CityID { get; set; }

        public string City { get; set; }

        public int DistrictID { get; set; }

        public string District { get; set; }

        public string Address { get; set; }

        public string UserName { get; set; }

        public string UserPhone { get; set; }

        public string UserTelePhone { get; set; }
    }
}
