using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 调查问题表
    /// </summary>
    public class SurveyTrouble : IBaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 调查主表ID
        /// </summary>
        public int SurveyMainID { get; set; }
        public virtual SurveyMain SurveyMain { get; set; }

        /// <summary>
        /// 题目编号
        /// </summary>
        [Display(Name = "题目编号")]
        [Required(ErrorMessage = "请输入题目编号")]
        [RegularExpression(@"\d+", ErrorMessage = "请输入正确的数字")]
        public int TroubleNumber { get; set; }

        /// <summary>
        /// 题目名称
        /// </summary>
        [Display(Name = "题目名称")]
        [Required(ErrorMessage = "请输入题目名称")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string TroubleName { get; set; }

        /// <summary>
        /// 题目类型
        /// </summary>
        [Display(Name = "题目类型")]
        public int EnumTroubleType { get; set; }

        public virtual ICollection<SurveyOption> SurveyOption { get; set; }

        public virtual ICollection<SurveyAnswer> SurveyAnswer { get; set; }

    }
}
