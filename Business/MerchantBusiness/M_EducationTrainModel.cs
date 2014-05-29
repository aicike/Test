using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;
using Interface.MerchantInterface;

namespace Business.MerchantBusiness
{
    /// <summary>
    /// 教育培训
    /// </summary>
    public class M_EducationTrainModel : BaseModel<M_EducationTrain>, IM_EducationTrainModel
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        public IQueryable<M_EducationTrain> GetListByMID(int MID)
        {
            var list = List().Where(a => a.MerchantID == MID).OrderByDescending(a => a.CreatDate);
            return list;
        }
    }
}
