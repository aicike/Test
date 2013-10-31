using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IOrderUserInfoModel : IBaseModel<OrderUserInfo>
    {
        IQueryable<OrderUserInfo> GetByAccountMainID(int accountMainID);
    }
}