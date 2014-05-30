using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;

namespace Interface.MerchantInterface
{
    /// <summary>
    /// 教育培训
    /// </summary>
    public interface IM_EducationTrainModel : IBaseModel<M_EducationTrain>
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        IQueryable<M_EducationTrain> GetListByMID(int MID);
    }
}
