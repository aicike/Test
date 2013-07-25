using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Poco.Enum;

namespace Interface
{
    public interface IClientInfoModel : IBaseModel<ClientInfo>
    {
        void PostClientID(string clientID);
    }
}