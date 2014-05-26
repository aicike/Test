using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Poco.Enum;

namespace Poco.MerchantPoco
{
    /// <summary>
    /// 周边外卖主表
    /// </summary>
    public class M_TakeOut:IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int MerchantID { get; set; }

        public virtual Merchant Merchant { get; set; }

        public int? SystemUserID { get; set; }

        public virtual SystemUser SystemUser { get; set; }

        public virtual ICollection<M_TakeOutDetail> M_TakeOutDetails { get; set; }

        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入标题")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Title { get; set; }

        [Display(Name = "图片")]
        public string ImagePath { get; set; }


        [Display(Name = "送餐费")]
        [Required(ErrorMessage = "请输入送餐费")]
        [RegularExpression(@"^(\d+(.\d{1,2})|\d+)$", ErrorMessage = "请输入有效的金额")]
        public decimal TakeOutPrice { get; set; }

        [Display(Name = "订餐电话")]
        [Required(ErrorMessage = "请输入订餐电话")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Phone { get; set; }

        /// <summary>
        /// 详细内容
        /// </summary>
        [Display(Name = "内容")]
        public string Content { get; set; }

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

        public virtual ICollection<M_CommunityMapping> M_CommunityMappings { get; set; }
    }
}
