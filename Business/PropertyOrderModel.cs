using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using Poco;
using Injection;
using Injection.Transaction;
using Poco.Enum;

namespace Business
{
    public class PropertyOrderModel : BaseModel<PropertyOrder>, IPropertyOrderModel
    {
        private readonly object Property_obj = new object();

        /// <summary>
        /// 提交物业订单
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="AMID"></param>
        /// <param name="UserID"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        [Transaction]
        public Result UpPropertyOrder(int[] IDS, int AMID, int UserID)
        {

            Result result = new Result();
            try
            {
                lock (Property_obj)
                {
                    //获取订单号
                    string orderNumSql = "SELECT dbo.SetSerialNumber_Property('P',4," + AMID + ")";
                    CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                    string orderNum = commonModel.SqlQuery<string>(orderNumSql).FirstOrDefault();

                    //添加数据到订单表
                    PropertyOrder po = new PropertyOrder();
                    po.AccountMainID = AMID;
                    po.EnumPropertyOrderType = (int)EnumPropertyOrderType.PropertyFee;
                    po.OrderDate = DateTime.Now;
                    po.OrderName = "物业费";
                    po.OrderNum = orderNum;
                    po.OrderUserID = UserID;
                    var propertyfeemodel = Factory.Get<IPropertyFeeInfoModel>(SystemConst.IOC_Model.PropertyFeeInfoModel);
                    var price = propertyfeemodel.GetPriceByIDS(IDS, AMID);
                    po.Price = price;
                    po.status = (int)EnumOrderStatus.WaitPayMent;
                    result = base.Add(po);
                    if (result.HasError)
                    {
                        return result;
                    }
                    //添加明细
                    var propertyorderdetailmodel = Factory.Get<IPropertyOrderDetailModel>(SystemConst.IOC_Model.PropertyOrderDetailModel);
                    result = propertyorderdetailmodel.AddOrderDatail(IDS, AMID, po.ID);
                    if (!result.HasError)
                    {
                        var entity = new { OrderNum = orderNum,OrderName = "物业费", Price = price };
                        result.Entity = entity;
                    }
                }
            }
            catch (Exception ex){
                result.HasError = true;
                result.Error = ex.Message;
            }

            return result;
        }
    }
}
