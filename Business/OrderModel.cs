using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Poco.Enum;
using Injection.Transaction;
using Injection;

namespace Business
{
    public class OrderModel : BaseModel<Order>, IOrderModel
    {
        public IQueryable<Order> GetByAccountMianID(int accountMainID)
        {
            return List().Where(a => a.AccountMainID == accountMainID);
        }

        public IQueryable<Order> GetByAccountID(int accountID)
        {
            return List().Where(a => a.OrderUserID == accountID && a.OrderUserType == (int)EnumClientUserType.Account);
        }

        public IQueryable<Order> GetByAccountID(int accountID, bool orderStatusComplete)
        {
            var list = List().Where(a => a.OrderUserID == accountID && a.OrderUserType == (int)EnumClientUserType.Account);
            if (orderStatusComplete)
            {
                list.Where(a => a.status == (int)EnumOrderStatus.Complete || a.status == (int)EnumOrderStatus.Cancel);
            }
            else
            {
                list.Where(a => a.status != (int)EnumOrderStatus.Complete && a.status != (int)EnumOrderStatus.Cancel);
            }
            return list;
        }


        public IQueryable<Order> GetList(int accountMainID, int daybyday, string orderNum, string PhoneNum, string status)
        {
            var list = List().Where(a => a.AccountMainID == accountMainID);
            DateTime day = DateTime.Now;
            if (status != "all")
            {
                list = list.Where(a => a.status == int.Parse(status));
            }
            switch (daybyday)
            {
                case 0:

                    break;
                case 7:
                    day = DateTime.Now.AddDays(-8);
                    list = list.Where(a => a.OrderDate > day);
                    break;
                case 30:
                    day = DateTime.Now.AddDays(-31);
                    list = list.Where(a => a.OrderDate > day);
                    break;
                case 365:
                    day = DateTime.Now.AddDays(-366);
                    list = list.Where(a => a.OrderDate > day);
                    break;
            }
            if (!string.IsNullOrEmpty(orderNum))
            {
                list.Where(a => a.OrderNum.Contains(orderNum.Trim()));
            }
            if (!string.IsNullOrEmpty(PhoneNum))
            {
                list.Where(a => a.OrderUserInfo.RPhone.Contains(PhoneNum.Trim()));
            }
            return list;
        }

        [Transaction]
        public Result AddOrder(Order rorder, OrderUserInfo orderUserInfo, int productID, int count)
        {
            Result result = new Result();
            //获取产品信息
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var product = productModel.Get(productID);
            if (product == null)
            {
                result.Error = "参数错误，请稍后重试！";
                return result;
            }
            //计算订单编号
            string orderNumSql = "SELECT dbo.SetSerialNumber('R',4,1)";
            CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
            string orderNum = commonModel.SqlQuery<string>(orderNumSql).FirstOrDefault();
            if (string.IsNullOrEmpty(orderNum) || orderNum.Length == 0)
            {
                result.Error = "参数错误，请稍后重试！";
                return result;
            }
            //收货信息
            orderUserInfo.AccountMainID = rorder.AccountMainID;
            if (orderUserInfo.ID != null)
            {
                if (orderUserInfo.IsUpdate == false)
                {
                    //修改
                    var orderUserInfoModel = Factory.Get<IOrderUserInfoModel>(SystemConst.IOC_Model.OrderUserInfoModel);
                    result = orderUserInfoModel.Edit(orderUserInfo);
                }
            }
            else
            {
                //新增
                var orderUserInfoModel = Factory.Get<IOrderUserInfoModel>(SystemConst.IOC_Model.OrderUserInfoModel);
                result = orderUserInfoModel.Add(orderUserInfo);
            }
            if (result.HasError)
            {
                result.Error = "参数错误，请稍后重试！";
                return result;
            }
            //计算价格
            var totalPrice = product.Price * count;

            //订单信息
            Order order = new Order();
            order.AccountMainID = rorder.AccountMainID;
            order.OrderNum = orderNum;
            order.OrderUserID = rorder.OrderUserID;
            order.OrderUserType = rorder.OrderUserType;
            order.OrderDate = DateTime.Now;
            order.BeginDate = rorder.BeginDate;
            order.EndDate = DateTime.Now;//todo 需要计算
            order.OrderUserInfoID = orderUserInfo.ID;
            order.Remark = rorder.Remark;
            order.Price = totalPrice;
            order.status = (int)EnumOrderStatus.Proceed;
            order.DeliveryType = rorder.DeliveryType;
            result = base.Add(order);
            if (result.HasError)
            {
                result.Error = "参数错误，请稍后重试！";
                return result;
            }
            //订单详细
            var orderDetailModel = Factory.Get<IOrderDetailModel>(SystemConst.IOC_Model.OrderDetailModel);
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.AccountMainID = order.AccountMainID;
            orderDetail.OrderID = order.ID;
            orderDetail.ProductID = productID;
            orderDetail.ProductName = product.Name;
            orderDetail.ProductImg = product.imgFilePath;
            orderDetail.ProductType = product.Classify.Name;
            orderDetail.Price = product.Price;
            orderDetail.Count = count;
            result = orderDetailModel.Add(orderDetail);
            if (result.HasError)
            {
                result.Error = "参数错误，请稍后重试！";
                return result;
            }
            else
            {
                result.Entity = order;
            }
            return result;
        }
    }
}
