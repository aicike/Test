﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Poco.Enum;

namespace Interface
{
    public interface IOrderModel : IBaseModel<Order>
    {
        IQueryable<Order> GetByAccountMianID(int accountMainID);

        IQueryable<Order> GetByAccountID(int accountID);

        IQueryable<Order> GetByAccountID(int accountID, bool orderStatusComplete);

        IQueryable<Order> GetList(int accountMainID, int daybyday, string orderNum, string PhoneNum, string status);

        Result AddOrder(Order rorder, OrderUserInfo orderUserInfo, int productID, int count, int OrderMTypeID);

        string GetOrderStatusName(EnumOrderStatus orderStatus);

        string GeDeliveryTypeName(EnumDeliveryType deliveryType);

        Result SetOrderStatus(int id, int status);

        /// <summary>
        /// 微商城，获取待支付订单列表
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        IQueryable<Order> MicroSite_GetByUserID_WaitPayMent(int amid, int userID);

        /// <summary>
        /// 微商城，获取待收货订单列表
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="userID"></param>
        /// <returns></returns> 
        IQueryable<Order> MicroSite_GetByUserID_Proceed(int amid, int userID);

        /// <summary>
        /// 微商城，获取已结束订单列表
        /// </summary>
        /// <param name="amid"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        IQueryable<Order> MicroSite_GetByUserID_Complete(int amid, int userID);

        /// <summary>
        /// 微商城 提交订单
        /// </summary>
        /// <param name="HPIDS">产品ID（格式 9|10|1）</param>
        /// <param name="HPIDSandCnt">产品ID 与数量（格式 9,2|10,1|1,3）</param>
        /// <param name="HUserID">用户ID</param>
        /// <param name="AID">收货地址ID</param>
        /// <returns></returns>
        Result Micro_AddOrder(string HPIDS, string HPIDSandCnt, int HUserID, int AID, int AMID);



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
        IQueryable<Order> Micro_GetList(int accountMainID, int daybyday, string orderNum, string PhoneNum, string status, string UserName, string Pname);
    }
}
