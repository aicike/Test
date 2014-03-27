using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    /// <summary>
    /// 抽奖活动-砸金蛋
    /// </summary>
    public class Lottery_egg : IBaseEntity
    {

        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public virtual ICollection<Lottery_User> Lottery_Users { get; set; }

        public virtual ICollection<ActivityInfo> ActivityInfos { get; set; }
    }
}
