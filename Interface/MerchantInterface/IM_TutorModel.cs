using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;

namespace Interface.MerchantInterface
{
    /// <summary>
    /// 家教
    /// </summary>
    public interface IM_TutorModel : IBaseModel<M_Tutor>
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        IQueryable<M_Tutor> GetListByMID(int MID);
    }
}
