﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.Enum
{
    public enum EnumOrderStatus
    {
        /// <summary>
        /// 已完成
        /// </summary>
        Complete,        
        /// <summary>
        /// 进行中 
        /// </summary>
        Proceed,        
        /// <summary>
        /// 取消
        /// </summary>
        Cancel,        
        /// <summary>
        /// 等待付款
        /// </summary>
        WaitPayMent,        
        /// <summary>
        /// 已付款
        /// </summary>
        Payment,
        /// <summary>
        /// 已发货
        /// </summary>
        Shipped
    }
}