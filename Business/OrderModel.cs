using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Poco.Enum;

namespace Business
{
    public class OrderModel : BaseModel<Order>, IOrderModel
    {
        public IQueryable<Order> GetByAccountMianID(int accountMainID)
        {
            return List().Where(a => a.AccountMainID == accountMainID);
        }

        public IQueryable<Order> GetByAccountID(int accountID)
        {
            return List().Where(a => a.OrderUserID == accountID && a.OrderUserType == (int)EnumClientUserType.Account);
        }

        public IQueryable<Order> GetByAccountID(int accountID, bool orderStatusComplete)
        {
            var list = List().Where(a => a.OrderUserID == accountID && a.OrderUserType == (int)EnumClientUserType.Account);
            if (orderStatusComplete)
            {
                list.Where(a => a.status == (int)EnumOrderStatus.Complete || a.status == (int)EnumOrderStatus.Cancel);
            }
            else
            {
                list.Where(a => a.status != (int)EnumOrderStatus.Complete && a.status != (int)EnumOrderStatus.Cancel);
            }
            return list;
        }



    }
}
