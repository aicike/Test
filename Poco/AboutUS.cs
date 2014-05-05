using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class AboutUS : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 售楼部ID
        /// </summary>
        [Display(Name = "售楼部")]
        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }



    }
}
