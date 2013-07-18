﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;

namespace Business
{
    public class DistrictModel : BaseModel<District>, IDistrictModel
    {
        public List<District> GetDistrictList(int cityID)
        {
            return List().Where(a => a.CityID == cityID).OrderBy(a => a.ID).ToList();
        }
    }
}
