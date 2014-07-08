using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    /// <summary>
    /// 家教
    /// </summary>
    public class App_Tutor
    {
        public int ID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 原图
        /// </summary>
        public string MainImage { get; set; }

        /// <summary>
        /// 列表展示缩略图
        /// </summary>
        public string ShowImage { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 价格/
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 发布时间（审核通过时间）
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

        /// <summary>
        /// 详细地址
        /// </summary>
        public string URL { get; set; }
    }
}
