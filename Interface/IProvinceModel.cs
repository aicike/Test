using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using System.Web.Mvc;

namespace Interface
{
    public interface IProvinceModel : IBaseModel<Province>
    {
        List<Province> GetProvinceList();
    }
}
