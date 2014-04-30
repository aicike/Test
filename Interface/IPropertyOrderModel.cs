using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IPropertyOrderModel : IBaseModel<PropertyOrder>
    {
         /// <summary>
        /// 提交物业订单
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="AMID"></param>
        /// <param name="UserID"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        Result UpPropertyOrder(int[] IDS, int AMID, int UserID);
    }
}
