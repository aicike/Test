using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IMerchantModel : IBaseModel<Merchant>
    {
         /// <summary>
        /// 商户登陆
        /// </summary>
        /// <param name="Phone"></param>
        /// <param name="PWD"></param>
        /// <returns></returns>
        Result Login(string Phone, string PWD);
    }
}
