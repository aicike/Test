using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using System.Web;
using System.IO;
using Injection;
using Poco.Enum;
using Spring.Transaction.Interceptor;

namespace Business
{
    public class AccountMainExpandInfoModel : BaseModel<AccountMainExpandInfo>, IAccountMainExpandInfoModel
    {
    }
}
