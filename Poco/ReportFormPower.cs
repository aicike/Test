using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class ReportFormPower : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// EnumReportForm  ID
        /// </summary>
        public int EnumReportID { get; set; }

    }
}
