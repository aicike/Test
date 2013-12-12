using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Injection;

namespace Business
{
    public class VIPInfoExpenseDetailModel : BaseModel<VIPInfoExpenseDetail>, IVIPInfoExpenseDetailModel
    {
        public IQueryable<VIPInfoExpenseDetail> GetByUserID(int userID)
        {
            return List().Where(a => a.UserID == userID);
        }
    }
}
