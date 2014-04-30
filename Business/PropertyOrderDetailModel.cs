using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Injection;
using Injection.Transaction;

namespace Business
{
    public class PropertyOrderDetailModel : BaseModel<PropertyOrderDetail>, IPropertyOrderDetailModel
    {

        /// <summary>
        /// 添加明细
        /// </summary>
        /// <param name="IDS"></param>
        /// <param name="AMID"></param>
        /// <returns></returns>
        [Transaction]
        public Result AddOrderDatail(int[] IDS, int AMID, int PID)
        {
            Result result = new Result();
            //获取物业费详细
            var propertyfeemodel = Factory.Get<IPropertyFeeInfoModel>(SystemConst.IOC_Model.PropertyFeeInfoModel);
            var ProPertyList = propertyfeemodel.GetPropertyFeeByIDS(IDS, AMID);
            StringBuilder stringBuilderSql = new StringBuilder("INSERT INTO dbo.PropertyOrderDetail( SystemStatus ,AccountMainID ,PropertyFeeInfoID ,Title,Price,Count) ");
            foreach (var item in ProPertyList)
            {
                stringBuilderSql.AppendFormat(" SELECT 0,{0},{1},'{2}',{3},1 UNION ALL", AMID, PID, item.PayDate + "物业费", item.Total);
            }
            var OptionSql = stringBuilderSql.ToString();
            OptionSql = OptionSql.Remove(OptionSql.Length - " UNION ALL".Length);
            CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            var cnt = commonModel.SqlExecute(OptionSql);
            if (cnt <= 0)
            {
                result.HasError = true;
            }
            return result;
        }

    }
}
