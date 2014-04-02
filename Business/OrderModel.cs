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
using Common;
using System.Web;

namespace Business
{
    public class OrderModel : BaseModel<Order>, IOrderModel
    {
        private readonly object obj = new object();

        private readonly object Micro_obj = new object();

        public IQueryable<Order> GetByAccountMianID(int accountMainID)
        {
            return List(true).Where(a => a.AccountMainID == accountMainID);
        }

        public IQueryable<Order> GetByAccountID(int accountID)
        {
            return List(true).Where(a => a.OrderUserID == accountID && a.OrderUserType == (int)EnumClientUserType.Account);
        }

        public IQueryable<Order> GetByAccountID(int accountID, bool orderStatusComplete)
        {
            var list = List(true).Where(a => a.OrderUserID == accountID && a.OrderUserType == (int)EnumClientUserType.Account);
            if (orderStatusComplete)
            {
                list = list.Where(a => a.status == (int)EnumOrderStatus.Complete || a.status == (int)EnumOrderStatus.Cancel);
            }
            else
            {
                list = list.Where(a => a.status != (int)EnumOrderStatus.Complete && a.status != (int)EnumOrderStatus.Cancel);
            }
            return list;
        }


        public IQueryable<Order> GetList(int accountMainID, int daybyday, string orderNum, string PhoneNum, string status)
        {
            var list = List(true).Where(a => a.AccountMainID == accountMainID);
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
                string orderNumSql = "SELECT dbo.SetSerialNumber('R',4," + rorder.AccountMainID + ")";
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
                order.EndDate = commonModel.GetEndDate(rorder.BeginDate.Value, orderMtype.DateCnt, rorder.AccountMainID, order.DeliveryType);
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
                //orderDetail.ProductImg = product.imgFilePath;
                orderDetail.ProductType = product.Classify.Name;
                orderDetail.Specification = product.Specification;
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
                    app_order.EndDate = order.EndDate.Value.ToString("yyyy-MM-dd");
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

        /// <summary>
        /// 微商城，获取待支付订单列表
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IQueryable<Order> MicroSite_GetByUserID_WaitPayMent(int amid, int userID)
        {
            return List(true).Where(a => a.AccountMainID == amid && a.OrderUserID == userID && a.OrderUserType == 2 && a.status == (int)EnumOrderStatus.WaitPayMent);
        }


        /// <summary>
        /// 微商城，获取待收货订单列表
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IQueryable<Order> MicroSite_GetByUserID_Proceed(int amid, int userID)
        {
            return List(true).Where(a => a.AccountMainID == amid && a.OrderUserID == userID && a.OrderUserType == 2
                && (a.status == (int)EnumOrderStatus.Proceed ||
                a.status == (int)EnumOrderStatus.WaitDistribution ||
                a.status == (int)EnumOrderStatus.Shipped ||
                a.status == (int)EnumOrderStatus.Payment));
        }

        /// <summary>
        /// 微商城，获取已结束订单列表
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IQueryable<Order> MicroSite_GetByUserID_Complete(int amid, int userID)
        {
            return List(true).Where(a => a.AccountMainID == amid && a.OrderUserID == userID && a.OrderUserType == 2
                && (
                a.status == (int)EnumOrderStatus.Cancel ||
                a.status == (int)EnumOrderStatus.Complete));
        }

        /// <summary>
        /// <summary>
        /// 微商城 提交订单
        /// </summary>
        /// <param name="HPIDS">产品ID（格式 9|10|1）</param>
        /// <param name="HPIDSandCnt">产品ID 与数量（格式 9,2|10,1|1,3）</param>
        /// <param name="HUserID">用户ID</param>
        /// <param name="AID">收货地址ID</param>
        /// <returns></returns>
        [Transaction]
        public Result Micro_AddOrder(string HPIDS, string HPIDSandCnt, int HUserID, int AID, int AMID)
        {
            Result result = new Result();
            try
            {
                lock (Micro_obj)
                {
                    //获取收货地址
                    var AdsModel = Factory.Get<IUserDeliveryAddressModel>(SystemConst.IOC_Model.UserDeliveryAddressModel);
                    var DBUda = AdsModel.Get(AMID, HUserID, AID);
                    OrderUserInfo ouInfo = new OrderUserInfo();
                    ouInfo.AccountMainID = AMID;
                    ouInfo.Address = DBUda.Address;
                    ouInfo.CityID = DBUda.CityID;
                    ouInfo.DistrictID = DBUda.DistrictID;
                    ouInfo.ProvinceID = DBUda.ProvinceID;
                    ouInfo.Receiver = DBUda.Receiver;
                    ouInfo.RPhone = DBUda.RPhone;
                    ouInfo.TelePhone = DBUda.TelePhone;
                    //添加地址到订单收货地址表中
                    var ouInfoModel = Factory.Get<IOrderUserInfoModel>(SystemConst.IOC_Model.OrderUserInfoModel);
                    result = ouInfoModel.Add(ouInfo);
                    if (result.HasError)
                    {
                        result.Error = "参数错误，请稍后重试！";
                        return result;
                    }
                    //获取产品 
                    var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
                    HPIDS = HPIDS.TrimEnd('|');
                    int[] Pstr = HPIDS.ConvertToIntArray('|');
                    var productList = productModel.GetProductListByIDs(Pstr, AMID).ToList();
                    //计算总价钱 与获取产品信息
                    List<OrderDetail> ods = new List<OrderDetail>();
                    double amount = 0;
                    HPIDSandCnt = HPIDSandCnt.TrimEnd('|');
                    string[] HPIDCnt = HPIDSandCnt.Split('|');
                    CommonModel com = new CommonModel();
                    foreach (var item in productList)
                    {
                        if (item.Status != (int)EnumProductType.Normal)
                        {
                            result.Error = "订单中 产品状态已改变！";
                            return result;
                        }
                        foreach (string K in HPIDCnt)
                        {
                            string[] cnts = K.Split(',');
                            if (item.ID == int.Parse(cnts[0]))
                            {
                                //校验库存是否足够
                                if (item.Stock < int.Parse(cnts[1]))
                                {
                                    result.HasError = true;
                                    result.Error = item.ID.ToString();
                                    return result;
                                }
                                //总价
                                amount = amount + (item.Price * int.Parse(cnts[1]));
                                OrderDetail od = new OrderDetail();
                                od.AccountMainID = AMID;
                                od.Count = int.Parse(cnts[1]);
                                od.Price = item.Price;
                                od.ProductID = item.ID;
                                od.ProductName = item.Name;
                                od.ProductType = item.Classify.Name;
                                od.Specification = item.Specification;

                                if (item.ProductImg.FirstOrDefault() != null)
                                {
                                    string Path = Tool.GetAMTemporaryPath(AMID);
                                    var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    var LastName = token + com.CreateRandom("", 5) + item.ProductImg.FirstOrDefault().PImgMini.GetFileSuffix();


                                    var YimgPath = "";
                                    var ImagePath = Path + "/" + LastName;
                                    var mapePath = "";
                                    //集成微网站
                                    if (SystemConst.IsIntegrationWebProject)
                                    {
                                        var accountPath = string.Format(SystemConst.IntegrationPathBase, AMID);
                                        var itemImg = item.ProductImg.FirstOrDefault().PImgMini;
                                        YimgPath = accountPath + itemImg.Substring(itemImg.LastIndexOf('/') + 1);
                                        mapePath = string.Format(SystemConst.IntegrationOrderTemporary, AMID) + Path.Substring(Path.LastIndexOf('/')) + "/" + LastName;

                                    }
                                    //不是集成微网站
                                    else
                                    {
                                        YimgPath = HttpContext.Current.Server.MapPath(item.ProductImg.FirstOrDefault().PImgMini);
                                        mapePath = HttpContext.Current.Server.MapPath(ImagePath);
                                    }



                                    Tool.SuperGetPicThumbnail(YimgPath, mapePath, 70, 100, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                                    od.ProductImg = ImagePath;
                                }
                                else
                                {
                                    od.ProductImg = "/Images/nopicture.png";
                                }

                                ods.Add(od);
                                break;
                            }
                        }

                    }
                    //获取订单编号
                    string orderNumSql = "SELECT dbo.SetSerialNumber('M',5," + AMID + ")";
                    CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                    string orderNum = commonModel.SqlQuery<string>(orderNumSql).FirstOrDefault();
                    if (string.IsNullOrEmpty(orderNum) || orderNum.Length == 0)
                    {
                        result.Error = "参数错误，请稍后重试！";
                        return result;
                    }
                    //添加订单表数据
                    Order order = new Order();
                    order.AccountMainID = AMID;
                    order.OrderNum = orderNum;
                    order.OrderUserID = HUserID;
                    order.OrderUserType = 2;//用户
                    order.OrderDate = DateTime.Now;

                    order.OrderUserInfoID = ouInfo.ID;
                    order.Remark = "";
                    order.Price = amount;
                    order.status = (int)EnumOrderStatus.WaitDistribution;
                    order.DeliveryType = (int)EnumDeliveryType.EveryDay;
                    result = base.Add(order);
                    if (result.HasError)
                    {
                        result.Error = "参数错误，请稍后重试！";
                        return result;
                    }
                    //添加订单产品表数据
                    var orderDetailModel = Factory.Get<IOrderDetailModel>(SystemConst.IOC_Model.OrderDetailModel);
                    result = orderDetailModel.AddOrderDetail(ods, order.ID);
                    if (result.HasError)
                    {
                        result.Error = "参数错误，请稍后重试！";
                        return result;
                    }

                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 微商城 web端 获取全部订单列表
        /// </summary>
        /// <param name="accountMainID">AMID</param>
        /// <param name="daybyday">时间段</param>
        /// <param name="orderNum">订单号</param>
        /// <param name="PhoneNum">电话号</param>
        /// <param name="status">状态</param>
        /// <param name="UserName">客户姓名</param>
        /// <param name="Pname">产品名称</param>
        /// <returns></returns>
        public IQueryable<Order> Micro_GetList(int accountMainID, int daybyday, string orderNum, string PhoneNum, string status, string UserName, string Pname)
        {
            var list = List(true).Where(a => a.AccountMainID == accountMainID);
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
            if (!string.IsNullOrEmpty(UserName))
            {
                list = list.Where(a => a.OrderUserInfo.Receiver.Contains(UserName.Trim()));
            }
            if (!string.IsNullOrEmpty(Pname))
            {
                list = list.Where(a => a.OrderDetail.Any(b => b.ProductName.Contains(Pname.Trim())));
            }
            return list;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [Transaction]
        public Result CancelOrder(int amid, int orderID)
        {
            Result result = new Result();
            Order order = List().Where(a => a.AccountMainID == amid && a.ID == orderID).FirstOrDefault();
            if (order == null) {
                result.Error = "无效的订单参数，无法操作。";
                return result;
            }
            var status = (EnumOrderStatus)order.status;
            if (status != EnumOrderStatus.WaitDistribution)
            {
                result.Error = "该订单已在配送过程中，无法取消。";
                return result;
            }
            //恢复产品库存
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            foreach (var item in order.OrderDetail)
            {
                item.Product.Stock = item.Product.Stock + item.Count;
            }
            //更改订单状态为取消
            order.status = (int)EnumOrderStatus.Revoke;
            result = Edit(order);
            //if (result.HasError)
            //{
            //    return result;
            //}
            return result;
        }
    }
}
