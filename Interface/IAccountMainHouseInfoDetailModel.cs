using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAccountMainHouseInfoDetailModel : IBaseModel<AccountMainHouseInfoDetail>
    {
        IQueryable<AccountMainHouseInfoDetail> GetList(int AccountMainHouseInfoID);
    }
}
