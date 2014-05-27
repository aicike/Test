using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business.SellBusiness
{
    public class PaymentModel : BaseModel<PayInfo>, IPaymentModel
    {
        /// <summary>
        /// 查找支付宝信息
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        public PayInfo GetInfoByAMID(int AMID)
        {
            var item = List().Where(a=>a.AccountMainID ==AMID).FirstOrDefault();
            return item;
        }
    }
}
