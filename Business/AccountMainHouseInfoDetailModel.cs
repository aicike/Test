using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class AccountMainHouseInfoDetailModel : BaseModel<AccountMainHouseInfoDetail>, IAccountMainHouseInfoDetailModel
    {
        public IQueryable<AccountMainHouseInfoDetail> GetList(int AccountMainHouseInfoID)
        {
            return List().Where(a => a.AccountMainHouseInfoID == AccountMainHouseInfoID);
        }
    }
}
