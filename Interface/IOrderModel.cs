using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IOrderModel : IBaseModel<Order>
    {
        public IQueryable<Order> GetByAccountMianID(int accountMainID);

        public IQueryable<Order> GetByAccountID(int accountID);

        public IQueryable<Order> GetByAccountID(int accountID, bool orderStatusComplete);
    }
}
