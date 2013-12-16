using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IVIPInfoExpenseDetailModel : IBaseModel<VIPInfoExpenseDetail>
    {
        IQueryable<VIPInfoExpenseDetail> GetByUserID(int userID);

    }
}
