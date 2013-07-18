using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface ICityModel : IBaseModel<City>
    {
        List<City> GetCityList(int provinceID);
    }
}
