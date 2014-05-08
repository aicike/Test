using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    /// <summary>
    /// 家教
    /// </summary>
    public class Tutor : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }
    }
}
