using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;

namespace Business
{
    public class AccountMainHousesModel : BaseModel<AccountMainHouses>, IAccountMainHousesModel
    {
        public IQueryable<AccountMainHouses> GetList(int AccountMainID)
        {
            return List().Where(a => a.AccountMainID == AccountMainID);
        }

        //删除所有数据 级联删除
        [Transaction]
        public Result DelteAll(int HousesID)
        {
            Result result = new Result();
            string sql = string.Format(@"delete dbo.AccountMainHouseInfoDetail where AccountMainHouseID={0} 
                           delete dbo.AccountMainHouseType where AccountMainHousesID = {0} 
                           delete dbo.AccountMainHouseInfo where AccountMainHousessID = {0} 
                           delete dbo.AccountMainHouses where ID= {0}",HousesID);
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
