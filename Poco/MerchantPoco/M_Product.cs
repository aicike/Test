using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco.MerchantPoco
{
    /// <summary>
    /// 商家促销
    /// </summary>
    public class M_Product : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        [Display(Name = "图片")]
        public string ImagePath { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 是否发布商品
        /// </summary>
        public bool IsPublish { get; set; }

        /// <summary>
        /// 审核状态 
        /// </summary>
        public int EnumDataStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatDate { get; set; }

        /// <summary>
        /// 发布时间（审核通过时间）
        /// </summary>
        public DateTime? PublishDate { get; set; }

        public int MerchantID { get; set; }

        public virtual Merchant Merchant { get; set; }
    }
}
