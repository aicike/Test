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


        public int VIPInfoID { get; set; }

        public virtual VIPInfo VIPInfo { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }

        public int AccountID { get; set; }
        public virtual Account Account { get; set; }


        public int EnumVIPOperate { get; set; }

    }
}
