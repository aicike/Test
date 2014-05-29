using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco.MerchantPoco
{
    /// <summary>
    /// 家教
    /// </summary>
    public class M_Tutor : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int MerchantID { get; set; }
        public virtual Merchant Merchant { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "请输入标题")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Title { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required(ErrorMessage = "请输入备注")]
        [StringLength(50, ErrorMessage = "长度小于200")]
        public string Remark { get; set; }

        /// <summary>
        /// 原图
        /// </summary>
        [Required(ErrorMessage = "请上传一张图片")]
        [StringLength(50, ErrorMessage = "长度小于200")]
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
        /// 价格
        /// </summary>
        [Required(ErrorMessage = "请输入价格")]
        public decimal Price { get; set; }

        /// <summary>
        /// 价格类型 月、次、个
        /// </summary>
        [Required(ErrorMessage = "请输入价格类型 例：月、次..")]
        public string PriceRemark { get; set; }


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
