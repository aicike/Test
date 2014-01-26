using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;

namespace Business
{
    public class ServiceModel : BaseModel<Service>, IServiceModel
    {
        public List<Service> GetList()
        {
            return List().OrderBy(a=>a.ID).ToList();
        }
    }
}
