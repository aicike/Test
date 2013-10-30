using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_Class
    {
        public int ID { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 上级ID 0为一级分类
        /// </summary>
        public int ParentID { get; set; }

        public List<App_Product> Product { get; set; }
    }
}
