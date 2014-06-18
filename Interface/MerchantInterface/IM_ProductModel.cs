using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;
using Poco;

namespace Interface.MerchantInterface
{
    public interface IM_ProductModel : IBaseModel<M_Product>
    {
        IQueryable<M_Product> ListByMerchantID(int merchantID);

        Result Add(M_Product entity, int[] communityIDs, int w, int h, int x1, int y1, int tw, int th);

        Result Edit(M_Product entity, int[] communityIDs, int w, int h, int x1, int y1, int tw, int th);
    }
}
