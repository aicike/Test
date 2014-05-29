using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;

namespace Interface.MerchantInterface
{
    /// <summary>
    /// 干洗服务
    /// </summary>
    public interface IM_DryCleaningModel : IBaseModel<M_DryCleaning>
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        IQueryable<M_DryCleaning> GetListByMID(int MID);
    }
}
