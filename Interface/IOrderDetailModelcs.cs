using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IOrderDetailModelcs : IBaseModel<OrderDetail>
    {
        IQueryable<OrderDetail> GetOrderDetailByOrderID(int OrderID);
        
    }
}
