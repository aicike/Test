using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;

namespace Interface.MerchantInterface
{
    public interface IM_TakeOutDetailModel : IBaseModel<M_TakeOutDetail>
    {
        List<M_TakeOutDetail> List(int takeOutID, int merchantID);
    }
}
