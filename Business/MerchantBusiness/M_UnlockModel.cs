using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;
using Interface.MerchantInterface;

namespace Business.MerchantBusiness
{
    public class M_UnlockModel : BaseModel<M_Unlock>, IM_UnlockModel
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="MID"></param>
        /// <returns></returns>
        public IQueryable<M_Unlock> GetListByMID(int MID)
        {
            var list = List().Where(a=>a.MerchantID == MID).OrderByDescending(a=>a.CreatDate);
            return list;
        }
    }
}
