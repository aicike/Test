using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IAccountMainOrganizationModel : IBaseModel<AccountMainOrganization>
    {
        Result Login(string loginName, string pwd);
    }
}
