using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class AccountMainHousesModel : BaseModel<AccountMainHouses>, IAccountMainHousesModel
    {
        public IQueryable<AccountMainHouses> GetList(int AccountMainID)
        {
            return List().Where(a => a.AccountMainID == AccountMainID);
        }

    }
}
