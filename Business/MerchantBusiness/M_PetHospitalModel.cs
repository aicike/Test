using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface.MerchantInterface;
using Poco.MerchantPoco;

namespace Business.MerchantBusiness
{
    /// <summary>
    /// 宠物医院
    /// </summary>
    public class M_PetHospitalModel : BaseModel<M_PetHospital>, IM_PetHospitalModel
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        public IQueryable<M_PetHospital> GetListByMID(int MID)
        {
            var list = List().Where(a => a.MerchantID == MID).OrderByDescending(a => a.CreatDate);
            return list;
        }
    }
}
