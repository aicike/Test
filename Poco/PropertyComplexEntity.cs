using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    /// <summary>
    /// 业主信息（视图）
    /// 业务表，不会在数据库中生成
    /// </summary>
    public class PropertyComplexEntity
    {
        /// <summary>
        /// User表ID信息
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 楼号
        /// </summary>
        public string BuildingNum { get; set; }

        /// <summary>
        /// 单元
        /// </summary>
        public string CellNum { get; set; }

        /// <summary>
        /// 房号
        /// </summary>
        public string HouseNum { get; set; }

        /// <summary>
        /// 业主姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 业主电话
        /// </summary>
        public string UserPhone{ get; set; }
    }
}
