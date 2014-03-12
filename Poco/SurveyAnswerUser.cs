using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class SurveyAnswerUser : IBaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        public string UserName { get; set;}

        /// <summary>
        /// 电话
        /// </summary>
        [Display(Name = "电话")]
        public string UserPhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        public string UserEmail { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        [Display(Name = "公司")]
        public string UserComPany { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [Display(Name = "职位")]
        public string UserPosition { get; set; }


        public virtual ICollection<SurveyAnswer> SurveyAnswer { get; set; }
    }
}
