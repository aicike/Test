using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class AppAdvertorialOperation : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AppAdvertorialID { get; set; }
        public virtual AppAdvertorial AppAdvertorial { get; set; }


        [Display(Name = "客户端类型")]
        public int EnumAdvertorialUType { get; set; }

        [Display(Name = "用户ID")]
        public int UserID { get; set; }

        [Display(Name = "转发总次数")]
        public int ForwardCnt { get; set; }

        [Display(Name = "转发微信次数")]
        public int ForwardWeiXinCnt { get; set; }

        [Display(Name = "转发微博次数")]
        public int ForwardWeiboCnt { get; set; }

        [Display(Name = "转发朋友圈次数")]
        public int ForwardFriendCnt { get; set; }

    }
}
