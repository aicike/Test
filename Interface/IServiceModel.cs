using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IServiceModel:IBaseModel<Service>
    {
        List<Service> GetList();
    }
}
