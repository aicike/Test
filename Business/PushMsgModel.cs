using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Injection;

namespace Business
{
    public class PushMsgModel : BaseModel<PushMsg>, IPushMsgModel
    {
        [Transaction]
        public Result AddPush(PushMsg entity, List<int> userID)
        {
            Result result = base.Add(entity);
            if (result.HasError)
            {
                return result;
            }
            IPushMsgDetailModel pmdModel = Factory.Get<IPushMsgDetailModel>(SystemConst.IOC_Model.PushMsgDetailModel);
            return pmdModel.AddPushMsgDetail(entity.ID, Poco.Enum.EnumClientUserType.User, userID);
        }
    }
}
