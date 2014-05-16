
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Poco.MerchantPoco;

namespace Poco.MerchantPoco
{
   /// <summary>
   /// 商户发布信息和小区映射表
   /// </summary>
   public class M_CommunityMapping : IBaseEntity
   {
       public int ID { get; set; }

       public int SystemStatus { get; set; }

       /// <summary>
       /// 小区ID
       /// </summary>
       public int AccountMainID { get; set; }

       public virtual AccountMain AccountMain { get; set; }

       /// <summary>
       /// 周边外卖
       /// </summary>
       public int? M_TakeOutID { get; set; }
       public virtual M_TakeOut M_TakeOut { get; set; }

       /// <summary>
       /// 搬家
       /// </summary>
       public int? M_MoveID { get; set; }
       public virtual M_Move M_Move { get; set; }

       /// <summary>
       /// 管道疏通
       /// </summary>
       public int? M_PipelineDredgeID { get; set; }
       public virtual M_PipelineDredge M_PipelineDredge { get; set; }

       /// <summary>
       /// 开锁换锁
       /// </summary>
       public int? M_UnlockID { get; set; }
       public virtual M_Unlock M_Unlock { get; set; }
   }
}
