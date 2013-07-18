using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class CityModel : BaseModel<City>, ICityModel
    {
        public List<City> GetCityList(int provinceID)
        {
            return List().Where(a => a.ProvinceID == provinceID).OrderBy(a => a.ID).ToList();
        }
    }
}
