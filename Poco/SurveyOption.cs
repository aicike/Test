using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 调查选项表
    /// </summary>
    public class SurveyOption : IBaseEntity
    {
        
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 问题ID
        /// </summary>
        [Display(Name = "问题ID")]
        public int SurveyTroubleID { get; set; }
        public virtual SurveyTrouble SurveyTrouble { get; set; }

        /// <summary>
        /// 选项
        /// </summary>
        [Display(Name = "选项")]
        [Required(ErrorMessage = "请输入选项")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Option { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        [Display(Name = "分数")]
        public int Fraction { get; set; }


        public virtual ICollection<SurveyAnswer> SurveyAnswer { get; set; }
    }
}
