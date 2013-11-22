using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class Holiday : IBaseEntity
    {
         
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }



        public DateTime HoliDayValue { get; set; }


        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Remark { get; set; }

    }
}
