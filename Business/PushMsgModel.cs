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

        public IQueryable<PushMsg> GetList(int accountMainID, int accountID)
        {
            return List().Where(a => a.AccountMainID == accountMainID && a.AccountID == accountID);
        }

        [Transaction]
        public new Result Delete(int id)
        {
            Result result = new Result();
            try
            {
                string delDetailSql = "DELETE dbo.PushMsgDetail WHERE PushMsgID=" + id;
                string delSql = "DELETE dbo.PushMsg WHERE ID=" + id;
                var commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                commonModel.SqlExecute(delDetailSql);
                commonModel.SqlExecute(delSql);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }
    }
}
