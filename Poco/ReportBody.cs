using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class ReportBody
    {
        /// <summary>
        /// y坐标数据 例“[10,200,3]”
        /// </summary>
        public string categories { get; set; }

        /// <summary>
        /// 每个区域间隔大小 例 50
        /// </summary>
        public string tickInterval { get; set; }

        /// <summary>
        /// x 坐标数据 例“[2013-11-01,2013-11-02,2013-11-03]”
        /// </summary>
        public string data { get; set; }

    }
}
