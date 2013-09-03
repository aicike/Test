using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class Conversation : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "用户ID1")]
        public string User1ID { get; set; }

        [Display(Name = "用户ID2")]
        public string User2ID { get; set; }

        /// <summary>
        /// 消息分类 0 ：AccountUser，1： ，2：
        /// </summary>
        [Display(Name="消息分类")]
        public string Ctype { get; set; }
    }
}
