﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco.MerchantPoco;
using Interface.MerchantInterface;

namespace Business.MerchantBusiness
{
    public class M_TakeOutModel : BaseModel<M_TakeOut>, IM_TakeOutModel
    {
        public IQueryable<M_TakeOut> ListByMerchantID(int merchantID)
        {
            return List().Where(a => a.MerchantID == merchantID).OrderByDescending(a => a.CreatDate);
        }
    }
}
