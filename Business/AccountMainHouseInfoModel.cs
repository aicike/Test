using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class AccountMainHouseInfoModel : BaseModel<AccountMainHouseInfo>, IAccountMainHouseInfo
    {

        public IQueryable<AccountMainHouseInfo> GetList(int AccountMainHouseID)
        {
            return List().Where(a => a.AccountMainHousessID == AccountMainHouseID);
        }

    }
}
