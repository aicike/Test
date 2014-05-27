using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_Move
    {  /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }


        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string PublishDate { get; set; }

        /// <summary>
        /// 商家ID
        /// </summary>
        public int MID { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string MName { get; set; }
    }
}
