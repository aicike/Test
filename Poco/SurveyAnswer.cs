using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 调查回答表
    /// </summary>
    public class SurveyAnswer : IBaseEntity
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
        /// 选项ID
        /// </summary>
        [Display(Name = "选项ID")]
        public int? SurveyOptionID { get; set; }
        public virtual SurveyOption SurveyOption { get; set; }

        /// <summary>
        /// 回答内容
        /// </summary>
        [Display(Name = "回答内容")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        public string Content { get; set; }

        /// <summary>
        /// 回答日期
        /// </summary>
        [Display(Name = "回答日期")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 回答人名称
        /// </summary>
        [Display(Name = "回答人名称")]
        public string UserName { get; set; }

        /// <summary>
        /// 回答人ID
        /// </summary>
        [Display(Name = "回答人ID")]
        public int? UserID { get; set; }

        /// <summary>
        /// 回答人类型 0用户端，1销售端
        /// </summary>
        [Display(Name = "回答人类型 0用户端，1销售端")]
        public int? UserType { get; set; }

        /// <summary>
        /// 回答用户编码
        /// </summary>
        [Display(Name = "回答用户编码")]
        public string UserCode { get; set; }


        /// <summary>
        /// 回答用户信息表ID
        /// </summary>
        [Display(Name = "用户信息表ID")]
        public int? SurveyAnswerUserID { get; set; }
        public virtual SurveyTrouble SurveyAnswerUser { get; set; }
    }
}
