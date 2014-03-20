using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;

namespace Business
{
    public class OrderUserInfoModel : BaseModel<OrderUserInfo>, IOrderUserInfoModel
    {
        public IQueryable<OrderUserInfo> GetByAccountMainID(int accountMainID)
        {
            return List(true).Where(a => a.AccountMainID == accountMainID);
        }
    }
}
