using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;
using Injection.Transaction;

namespace Business
{
    public class MessageModel : BaseModel<Message>
    {
        [Transaction]
        public void Delete()
        {
            Delete(1);
            IAccountMainModel amm = Factory.Get<IAccountMainModel>(SystemConst.IOC_Model.AccountMainModel);
            amm.Delete(1);
        }
    }
}
