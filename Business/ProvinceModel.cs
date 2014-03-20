using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using System.Web.Mvc;

namespace Business
{
    public class ProvinceModel : BaseModel<Province>, IProvinceModel
    {
        public List<Province> GetProvinceList()
        {
            return List().ToList();
        }
    }
}
