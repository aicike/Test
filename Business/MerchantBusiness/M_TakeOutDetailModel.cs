using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface.MerchantInterface;
using Poco.MerchantPoco;

namespace Business.MerchantBusiness
{
    public class M_TakeOutDetailModel : BaseModel<M_TakeOutDetail>, IM_TakeOutDetailModel
    {
        public List<M_TakeOutDetail> List(int takeOutID, int merchantID)
        {
            return List().Where(a => a.M_TakeOutID == takeOutID && a.M_TakeOut.MerchantID == merchantID).ToList();
        }
    }
}
