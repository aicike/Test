using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Injection;
using Interface;
using Poco;
using Poco.WebAPI_Poco;
using Poco.Enum;

namespace Web.Controllers
{
    public class WebRequest_ProductController : Controller
    {
        /// <summary>
        /// 获取一级分类，及分类下产品
        /// </summary>
        /// <param name="amid"></param>
        /// <returns></returns>
        public string Get1levelClass(int amid)
        {
            var classModel = Factory.Get<IClassifyModel>(SystemConst.IOC_Model.ClassifyModle);
            var classList = classModel.Get1levelClass(amid);
            List<App_Class> list = new List<App_Class>();
            foreach (var item in classList)
            {
                App_Class appClass = new App_Class();
                appClass.ID = item.ID;
                appClass.Name = item.Name;
                appClass.ParentID = item.ParentID;
                List<App_Product> productList = new List<App_Product>();
                if (item.Product != null)
                {
                    foreach (var p in item.Product)
                    {
                        App_Product product = new App_Product();
                        product.ID = p.ID;
                        product.imgFilePath = SystemConst.WebUrlIP + p.imgFilePath.Replace("~", "");
                        product.Introduction = p.Introduction;
                        product.Specification = p.Specification;
                        product.Price = p.Price;
                        product.Name = p.Name;
                        productList.Add(product);
                    }
                }
                appClass.Product = productList;
                list.Add(appClass);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取产品详细
        /// </summary>
        /// <param name="pid"></param>
        public string GetProduct(int pid)
        {
            var productModel = Factory.Get<IProductModel>(SystemConst.IOC_Model.ProductModel);
            var p = productModel.Get(pid);
            App_Product product = new App_Product();
            product.ID = p.ID;
            product.imgFilePath = SystemConst.WebUrlIP + p.imgFilePath.Replace("~", "");
            product.Introduction = p.Introduction;
            product.Specification = p.Specification;
            product.Price = p.Price;
            product.Name = p.Name;
            return Newtonsoft.Json.JsonConvert.SerializeObject(product);
        }

        /// <summary>
        /// 配送时间类型
        /// </summary>
        /// <returns></returns>
        public string GetDeliveryType()
        {
            List<object> list = new List<object>();
            list.Add(new { id = (int)EnumDeliveryType.WorkingDay, name = "仅工作日送" });
            list.Add(new { id = (int)EnumDeliveryType.OffDay, name = "仅休息日(周六-周日)送" });
            list.Add(new { id = (int)EnumDeliveryType.EveryDay, name = "每日送" });
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="orderUserID">最后一个orderUserInfo的id</param>
        /// <returns></returns>
        public string GetOrderUserInfo(int amid)
        {
            var model = Factory.Get<IOrderUserInfoModel>(SystemConst.IOC_Model.OrderUserInfoModel);
            var list = model.GetByAccountMainID(amid).OrderBy(a => a.ID).Select(a => new App_OrderUserInfo()
             {
                 ID = a.ID,
                 ProvinceID = a.ProvinceID,
                 Province = a.Province.Name,
                 CityID = a.CityID,
                 City = a.City.Name,
                 DistrictID = a.DistrictID,
                 District = a.District.Name,
                 Address = a.Address,
                 UserName = a.Receiver,
                 UserPhone = a.RPhone,
                 UserTelePhone = a.TelePhone
             }).ToList();
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="isComplete"></param>
        /// <returns></returns>
        public string GetOrderByAccountID(int accountID, bool isComplete)
        {
            var model = Factory.Get<IOrderModel>(SystemConst.IOC_Model.OrderModel);
            var list = model.GetByAccountID(accountID, isComplete).Select(a => new App_Order()
            {
                OrderID = a.ID,
                OrderNum = a.OrderNum,
                OrderStatus=model.GetOrderStatusName((EnumOrderStatus)a.status),
                OrderUserName=a.OrderUserInfo.Receiver,
                OrderUserPhone=a.OrderUserInfo.RPhone,
                OrderUserAddress=a.OrderUserInfo.Address,
                OrderDate = a.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"),
                BeginDate = a.BeginDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = a.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Remark = a.Remark,
                ProductName = a.OrderDetail.FirstOrDefault().ProductName,
                TotalPrice = a.Price,
                DeliveryType = model.GeDeliveryTypeName((EnumDeliveryType)a.DeliveryType)
            });
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public string AddOrder(App_OrderSubmit orderInfo)
        {
            var model = Factory.Get<IOrderModel>(SystemConst.IOC_Model.OrderModel);
            Order order = new Order();
            order.AccountMainID = orderInfo.AccountMainID;
            order.OrderUserID = orderInfo.OrderUserID;
            order.OrderUserType = orderInfo.OrderUserType;
            OrderUserInfo orderUserInfo = new OrderUserInfo();
            orderUserInfo.ID = orderInfo.OrderUserInfoID;
            orderUserInfo.ProvinceID = orderInfo.ProvinceID;
            orderUserInfo.CityID = orderInfo.CityID;
            orderUserInfo.DistrictID = orderInfo.DistrictID;
            orderUserInfo.Address = orderInfo.Address;
            orderUserInfo.Receiver = orderInfo.Receiver;
            orderUserInfo.RPhone = orderInfo.RPhone;
            orderUserInfo.TelePhone = orderInfo.TelePhone;
            var resul = model.AddOrder(order, orderUserInfo, orderInfo.ProductID, orderInfo.Count);
            return Newtonsoft.Json.JsonConvert.SerializeObject(resul);
        }
    }
}
