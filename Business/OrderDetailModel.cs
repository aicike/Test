using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;

namespace Business
{
    public class OrderDetailModel : BaseModel<OrderDetail>, IOrderDetailModel
    {
        public IQueryable<OrderDetail> GetOrderDetailByOrderID(int OrderID)
        {
            var list = List().Where(a=>a.OrderID==OrderID);
            return list;
        }
    }
}
