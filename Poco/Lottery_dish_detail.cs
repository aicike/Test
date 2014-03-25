using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 大转盘详细
    /// </summary>
    public class Lottery_dish_detail : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int Lottery_dishID { get; set; }

        public virtual Lottery_dish Lottery_dish { get; set; }

        /// <summary>
        /// 奖品名称
        /// </summary>
        [Display(Name = "奖品名称")]
        [Required(ErrorMessage = "请输入标题")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Name { get; set; }

        /// <summary>
        /// 中奖比例
        /// </summary>
        [Display(Name = "中奖比例")]
        public double Ratio { get; set; }

        /// <summary>
        /// 奖品图片地址
        /// </summary>
        [Display(Name = "奖品图片")]
        public string Image { get; set; }

        /// <summary>
        /// 奖品描述
        /// </summary>
        [Display(Name = "奖品描述")]
        [Required(ErrorMessage = "请输入奖品描述")]
        public string Description { get; set; }

        /// <summary>
        /// 业务字段，不会在数据库中生成
        /// </summary>
        [Display(Name = "是否新上传图片")]
        public bool IsNewImg { get; set; }
    }
}
