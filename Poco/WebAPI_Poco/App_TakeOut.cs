using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_TakeOut
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string ImgPath { get; set; }

        public string Summary { get; set; }

        //总价
        public decimal Price { get; set; }

        //送餐费
        public decimal TakeOutPrice { get; set; }
    }
}
