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

        //商家ID
        public int MerchantID { get; set; }

        //商家名称 
        public string MerchantName { get; set; }

        //商品明细，富文本
        public string Content { get; set; }

        public List<App_TakeOutItem> ItemList { get; set; }

        public string ImagePath { get; set; }
    }

    public class App_TakeOutItem
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }
    }
}
