﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.Enum
{
    /// <summary>
    /// 客户端用户类型
    /// </summary>
    public enum EnumClientUserType
    {
        /// <summary>
        /// 平台维护账号
        /// </summary>
        Platform=0,
        /// <summary>
        /// 业务账号
        /// </summary>
        Account=1,
        /// <summary>
        /// 普通用户
        /// </summary>
        User=2
    }
}
