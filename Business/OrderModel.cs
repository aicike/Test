using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Poco.Enum;
using Injection.Transaction;
using Injection;
using Poco.WebAPI_Poco;

namespace Business
{
    public class OrderModel : BaseModel<Order>, IOrderModel
    {
        private readonly object obj = new object();

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
                list=list.Where(a => a.status == (int)EnumOrderStatus.Complete || a.status == (int)EnumOrderStatus.Cancel);
            }
            else
            {
                list=list.Where(a => a.status != (int)EnumOrderStatus.Complete && a.status != (int)EnumOrderStatus.Cancel);
            }
            return list;
        }


        public IQueryable<Order> GetList(int accountMainID, int daybyday, string orderNum, string PhoneNum, string status)
        {
            var list = List().Where(a => a.AccountMainID == accountMainID);
            DateTime day = DateTime.Now;
            if (status != "all")
            {
                int sta = int.Parse(status);
                list = list.Where(a => a.status == sta);
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
                list = list.Where(a => a.OrderNum.Contains(orderNum.Trim()));
            }
            if (!string.IsNullOrEmpty(PhoneNum))
            {
                list = list.Where(a => a.OrderUserInfo.RPhone.Contains(PhoneNum.Trim()));
            }
            return list;
        }

        [Transaction]
        public Result AddOrder(Order rorder, OrderUserInfo orderUserInfo, int productID, int count, int OrderMTypeID)
        {
            lock (obj)
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
                string orderNumSql = "SELECT dbo.SetSerialNumber('R',4,"+rorder.AccountMainID+")";
                CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                string orderNum = commonModel.SqlQuery<string>(orderNumSql).FirstOrDefault();
                if (string.IsNullOrEmpty(orderNum) || orderNum.Length == 0)
                {
                    result.Error = "参数错误，请稍后重试！";
                    return result;
                }
                //获取卖奶订单类型
                var orderMtypeModel = Factory.Get<IOrderMTypeModel>(SystemConst.IOC_Model.OrderMTypeModel);
                var orderMtype = orderMtypeModel.Get(OrderMTypeID);
                if (orderMtype == null)
                {
                    result.Error = "参数错误，请稍后重试！";
                    return result;
                }

                //收货信息
                orderUserInfo.AccountMainID = rorder.AccountMainID;
                if (orderUserInfo.ID == 0)
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
                var totalPrice = product.Price * count * orderMtype.DateCnt * orderMtype.Count;

                //订单信息
                Order order = new Order();
                order.AccountMainID = rorder.AccountMainID;
                order.OrderNum = orderNum;
                order.OrderUserID = rorder.OrderUserID;
                order.OrderUserType = rorder.OrderUserType;
                order.OrderDate = DateTime.Now;
                order.BeginDate = rorder.BeginDate;
                order.EndDate = commonModel.GetEndDate(rorder.BeginDate, orderMtype.DateCnt,rorder.AccountMainID,order.DeliveryType);
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
                //订单类型中间表
                OrderMIntermediate orderMIntermediate = new OrderMIntermediate();
                orderMIntermediate.OrderID = order.ID;
                orderMIntermediate.MTypeName = orderMtype.Name;
                orderMIntermediate.MTypeDateCnt = orderMtype.DateCnt;
                orderMIntermediate.MTypeCount = orderMtype.Count;
                orderMIntermediate.surplusDay = orderMtype.Count;
                var orderMIntermediateModel = Factory.Get<IOrderMIntermediateModel>(SystemConst.IOC_Model.OrderMIntermediateModel);
                result = orderMIntermediateModel.Add(orderMIntermediate);
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
                    App_Order app_order = new App_Order();
                    app_order.OrderID = order.ID;
                    app_order.OrderNum = order.OrderNum;
                    app_order.OrderStatus = GetOrderStatusName((EnumOrderStatus)order.status);
                    app_order.EndDate = order.EndDate.ToString("yyyy-MM-dd");
                    app_order.TotalPrice = order.Price;
                    result.Entity = order;
                }

                return result;
            }
        }

        public string GetOrderStatusName(EnumOrderStatus orderStatus)
        {
            string value = null;
            switch (orderStatus)
            {
                case EnumOrderStatus.Cancel:
                    value = "取消";
                    break;
                case EnumOrderStatus.Complete:
                    value = "已完成";
                    break;
                case EnumOrderStatus.Proceed:
                    value = "进行中";
                    break;
                case EnumOrderStatus.Revoke:
                    value = "撤销";
                    break;
                case EnumOrderStatus.WaitPayMent:
                    value = "等待付款";
                    break;
                case EnumOrderStatus.Payment:
                    value = "已付款";
                    break;
                case EnumOrderStatus.Shipped:
                    value = "已发货";
                    break;
            }
            return value;
        }

        public string GeDeliveryTypeName(EnumDeliveryType deliveryType)
        {
            string value = null;
            switch (deliveryType)
            {
                case EnumDeliveryType.EveryDay:
                    value = "每日送";
                    break;
                case EnumDeliveryType.OffDay:
                    value = "仅休息日(周六-周日)送";
                    break;
                case EnumDeliveryType.WorkingDay:
                    value = "仅工作日送";
                    break;
            }
            return value;
        }


        public Result SetOrderStatus(int id, int status) 
        {
            Result result = new Result();

            string sql = string.Format("update [Order] set status={1} where id ={0}", id, status);
            if (base.SqlExecute(sql) <= 0)
            {
                result.HasError = true;
                result.Error = "操作失败 请稍后再试！";
            }

            return result;
        }




    }
}
