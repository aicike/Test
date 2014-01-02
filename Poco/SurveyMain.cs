using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 调查主表
    /// </summary>
    public class SurveyMain : IBaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 售楼部ID
        /// </summary>
        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 调查标题
        /// </summary>
        [Display(Name = "调查标题")]
        [Required(ErrorMessage = "请输入调查标题")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string SurveyTitle { get; set; }

        /// <summary>
        /// 调查备注或描述
        /// </summary>
        [Display(Name = "调查备注或描述")]
        [Required(ErrorMessage = "请输入调查备注或描述")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        public string SurveyRemarks { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public int AccountID { get; set; }
        public virtual Account Account { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 状态 0：开启 1：停用
        /// </summary>
        [Display(Name = "创建日期")]
        public int Status { get; set; }

        /// <summary>
        /// 调查类型
        /// </summary>
        [Display(Name = "调查类型")]
        public int EnumSurveyMainType { get; set; }

        public virtual ICollection<SurveyTrouble> SurveyTrouble { get; set; }


    }
}
