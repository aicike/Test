using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 抽奖活动-大转盘
    /// </summary>
    public class Lottery_dish:IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 抽奖标题
        /// </summary>
        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入标题")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Name { get; set; }

        /// <summary>
        /// 抽奖状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 抽奖描述
        /// </summary>
        [Display(Name = "描述")]
        [Required(ErrorMessage = "请输入描述")]
        public string Description { get; set; }

        public int AccountMainID { get; set; }

        public virtual AccountMain AccountMain { get; set; }

        public virtual ICollection<Lottery_dish_detail> Lottery_dish_details { get; set; }

        public virtual ICollection<Lottery_User> Lottery_Users { get; set; }
    }
}
