using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IDistrictModel : IBaseModel<District>
    {
        List<District> GetDistrictList(int cityID);
    }
}
