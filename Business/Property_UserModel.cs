using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;

namespace Business
{
    public class Property_UserModel : BaseModel<Property_User>, IProperty_UserModel
    {
        public IQueryable<Property_User> GetListByAccountMainID(int accountMainID)
        {
            return base.List(true).Where(a => a.AccountMainID == accountMainID);
        }
    }
}
