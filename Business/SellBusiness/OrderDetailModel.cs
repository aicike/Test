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
    public class OrderDetailModel : BaseModel<OrderDetail>, IOrderDetailModel
    {
        public IQueryable<OrderDetail> GetOrderDetailByOrderID(int OrderID)
        {
            var list = List(true).Where(a=>a.OrderID==OrderID);
            return list;
        }

        /// <summary>
        /// 批量添加产品信息到中间表中
        /// </summary>
        /// <param name="ods"></param>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [Transaction]
        public Result AddOrderDetail(List<OrderDetail> ods, int orderID)
        {
            Result result = new Result();
            try
            {
                StringBuilder stringBuilderSql = new StringBuilder("INSERT INTO dbo.OrderDetail( SystemStatus ,AccountMainID ,OrderID ,ProductID,ProductName,ProductType,[Price],[Count],Specification,[ProductImg]) ");
                foreach (var item in ods)
                {
                    stringBuilderSql.AppendFormat(" SELECT 0,{0},{1},{2},'{3}','{4}',{5},{6},'{7}','{8}' UNION ALL", item.AccountMainID, orderID, item.ProductID, item.ProductName, item.ProductType, item.Price, item.Count, item.Specification,item.ProductImg);
                }

                CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;

                var AnswerSql = stringBuilderSql.ToString();
                AnswerSql = AnswerSql.Remove(AnswerSql.Length - " UNION ALL".Length);
                commonModel.SqlExecute(AnswerSql);
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
