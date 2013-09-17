using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAccountMainHouseInfo : IBaseModel<AccountMainHouseInfo>
    {
        IQueryable<AccountMainHouseInfo> GetList(int AccountMainHouseID);

        Result DelteAll(int HousesInfoID);
    }
}
