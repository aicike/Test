using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;

namespace Business
{
    public class OrderMTypeModel : BaseModel<OrderMType>, IOrderMTypeModel
    {
        public IQueryable<OrderMType> GetList(int AccountMainID)
        {
            var Orderlist = List().Where(a => a.AccountMainID == AccountMainID);
            return Orderlist;
        }
    }
}
