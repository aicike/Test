using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    public class App_VIPInfoExpenseDetail
    {
        public int ID { get; set; }

        /// <summary>
        /// 消费时间
        /// </summary>
        public string ExpenseDate { get; set; }

        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal ExpensePrice { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string ExpenseType { get; set; }
    }
}
