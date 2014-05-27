using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IPaymentModel : IBaseModel<PayInfo>
    {
        /// <summary>
        /// 查找支付宝信息
        /// </summary>
        /// <param name="AMID"></param>
        /// <returns></returns>
        PayInfo GetInfoByAMID(int AMID);
    }
}
