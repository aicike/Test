using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;

namespace Interface.MerchantInterface
{
    /// <summary>
    /// 宠物医院
    /// </summary>
    public interface IM_PetHospitalModel : IBaseModel<M_PetHospital>
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        IQueryable<M_PetHospital> GetListByMID(int MID);
    }
}
