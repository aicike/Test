using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;

namespace Business
{
    public class AutoMessage_AddModel : BaseModel<AutoMessage_Add>, IAutoMessage_AddModel
    {

        public AutoMessage_Add GitInfo(int AccountMainID)
        {
            return List().Where(a => a.AccountMainID == AccountMainID).FirstOrDefault();
        }
    }
}
