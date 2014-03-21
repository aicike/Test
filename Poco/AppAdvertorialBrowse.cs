using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class AppAdvertorialBrowse : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 浏览日期
        /// </summary>
        [Display(Name = "标题")]
        public DateTime BrowseDate { get; set; }

        /// <summary>
        /// 资讯
        /// </summary>
        [Display(Name = "资讯")]
        public int? AppAdvertorialID { get; set; }
        public virtual AppAdvertorial AppAdvertorial { get; set; }

        /// <summary>
        /// 活动
        /// </summary>
        [Display(Name = "活动")]
        public int? ActivityInfoID { get; set; }
        public virtual ActivityInfo ActivityInfo { get; set; }

        /// <summary>
        /// 调查
        /// </summary>
        [Display(Name = "调查")]
        public int? SurveyMainID { get; set; }
        public virtual SurveyMain SurveyMain { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        [Display(Name = "浏览次数")]
        public int BrowseCnt{get;set;}

        /// <summary>
        /// 浏览资讯类型 枚举
        /// </summary>
        [Display(Name = "浏览次数")]
        public int EnumBrowseType { get; set; }


    }
}
