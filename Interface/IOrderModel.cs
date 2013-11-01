using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IOrderModel : IBaseModel<Order>
    {
        IQueryable<Order> GetByAccountMianID(int accountMainID);

        IQueryable<Order> GetByAccountID(int accountID);

        IQueryable<Order> GetByAccountID(int accountID, bool orderStatusComplete);
    }
}
