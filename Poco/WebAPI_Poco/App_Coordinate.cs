using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_Coordinate
    {
        string lng;
        /// <summary>
        /// 纬度
        /// </summary>
        public string Lng
        {
            get { return lng; }
            set { lng = value; }
        }

        string lat;
        /// <summary>
        /// 经度
        /// </summary>
        public string Lat
        {
            get { return lat; }
            set { lat = value; }
        }
    }
}
