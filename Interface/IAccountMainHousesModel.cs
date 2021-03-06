﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAccountMainHousesModel : IBaseModel<AccountMainHouses>
    {
        IQueryable<AccountMainHouses> GetList(int AccountMainID);

        Result DelteAll(int HousesID);
    }
}
