using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class OrderMIntermediateModel : BaseModel<OrderMIntermediate>, IOrderMIntermediateModel
    {
        public OrderMIntermediate GetMintByOrderID(int OrderID)
        {
            var list = List().Where(a => a.OrderID == OrderID).FirstOrDefault();
            return list;
        }
    }
}
