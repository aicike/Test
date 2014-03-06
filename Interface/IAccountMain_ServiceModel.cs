using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Poco.Enum;

namespace Interface
{
    public interface IAccountMain_ServiceModel : IBaseModel<AccountMain_Service>
    {
        Result Add(int accountMainID, int[] serviceID);
        List<AccountMain_Service> GetListByAccountMainID(int accountMainID);

        /// <summary>
        /// 检查账号是否有某种服务
        /// </summary>
        bool CheckService(EnumService enumService, int accountMainID);
    }
}
