using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;
using Poco;

namespace Interface.MerchantInterface
{
    public interface IM_TakeOutModel : IBaseModel<M_TakeOut>
    {
        IQueryable<M_TakeOut> ListByMerchantID(int merchantID);

        Result Add(M_TakeOut entity, int[] communityIDs);
    }
}
