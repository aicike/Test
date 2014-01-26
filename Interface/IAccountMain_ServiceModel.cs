using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAccountMain_ServiceModel : IBaseModel<AccountMain_Service>
    {
        Result Add(int accountMainID, int[] serviceID);
        List<AccountMain_Service> GetListByAccountMainID(int accountMainID);
    }
}
