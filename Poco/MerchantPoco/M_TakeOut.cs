using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.MerchantPoco
{
    /// <summary>
    /// 周边外卖主表
    /// </summary>
    public class M_TakeOut:IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int MerchantID { get; set; }

        public virtual Merchant Merchant { get; set; }

        public int SystemUserID { get; set; }

        public virtual SystemUser SystemUser { get; set; }

        public virtual M_TakeOutDetail M_TakeOutDetails { get; set; }

    }
}
