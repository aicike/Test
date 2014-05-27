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

        Result Add(M_TakeOut entity, int[] communityIDs, int w, int h, int x1, int y1, int tw, int th);

        Result Edit(M_TakeOut entity, int[] communityIDs, int w, int h, int x1, int y1, int tw, int th);

        Result AddDetail(int id, int LoginMerchantID, List<M_TakeOutDetail> list, string content);
    }
}
