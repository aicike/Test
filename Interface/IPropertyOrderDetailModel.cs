using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IPropertyOrderDetailModel : IBaseModel<PropertyOrderDetail>
    {
        /// <summary>
        /// 添加明细
        /// </summary>
        /// <param name="IDS"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        Result AddOrderDatail(int[] IDS, int AMID, int PID);
    }
}
