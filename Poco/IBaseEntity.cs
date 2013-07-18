using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public interface IBaseEntity
    {
         int ID { get; set; }

         int SystemStatus { get; set; }
    }
}
