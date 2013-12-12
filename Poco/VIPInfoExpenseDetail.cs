using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class VIPInfoExpenseDetail : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 消费时间
        /// </summary>
        public DateTime ExpenseDate { get; set; }

        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal ExpensePrice { get; set; }

        /// <summary>
        /// 本次消费后余额
        /// </summary>
        public decimal Balance { get; set; }

        public int VIPInfoID { get; set; }

        public virtual VIPInfo VIPInfo { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }
    }
}
