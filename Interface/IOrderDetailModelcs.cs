using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IOrderDetailModel : IBaseModel<OrderDetail>
    {
        IQueryable<OrderDetail> GetOrderDetailByOrderID(int OrderID);
        
    }
}
