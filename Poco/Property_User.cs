using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    /// <summary>
    /// 业主信息
    /// </summary>
    public class Property_User :IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 房屋信息
        /// </summary>
        public int Property_HouseID { get; set; }

        public virtual Property_House Property_House { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public int UserLoginInfoID { get; set; }

        public virtual UserLoginInfo UserLoginInfo { get; set; }
    }
}
