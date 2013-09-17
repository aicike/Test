using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;

namespace Business
{
    public class AccountMainHouseInfoModel : BaseModel<AccountMainHouseInfo>, IAccountMainHouseInfo
    {

        public IQueryable<AccountMainHouseInfo> GetList(int AccountMainHouseID)
        {
            return List().Where(a => a.AccountMainHousessID == AccountMainHouseID);
        }


        //删除信息 级联删除
        [Transaction]
        public Result DelteAll(int HousesInfoID)
        {
            Result result = new Result();
            string sql = string.Format(@"delete dbo.AccountMainHouseInfoDetail where AccountMainHouseInfoID={0} 
                           delete dbo.AccountMainHouseInfo where ID = {0}", HousesInfoID);
            try
            {
                result.Entity = base.SqlExecute(sql);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = ex.Message;
            }
            return result;
        }
    }
}
