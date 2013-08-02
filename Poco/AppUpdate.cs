using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    /// <summary>
    /// 规定哪些数据需要更新(DateTime.Now.Ticks.ToString())
    /// </summary>
    public class AppUpdate : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccounMain { get; set; }
    }
}
