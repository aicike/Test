using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IOrderMTypeModel : IBaseModel<OrderMType>
    {
        IQueryable<OrderMType> GetList(int AccountMainID);
    }
}
