using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    /// <summary>
    /// 抽奖用户信息
    /// </summary>
    public class Lottery_User:IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 参加的大转盘ID
        /// </summary>
        public int? Lottery_dishID { get; set; }

        public virtual Lottery_dish Lottery_dish { get; set; }
        
        /// <summary>
        /// 参加的砸金蛋ID
        /// </summary>
        public int? Lottery_eggID { get; set; }

        public virtual Lottery_egg Lottery_egg { get; set; }
        
        /// <summary>
        /// 抽取奖品ID（砸金蛋，或者是大转盘奖品ID）
        /// </summary>
        public int Dish_Egg_detailID { get; set; }

        /// <summary>
        /// 用户ID，可以为空，空表示没用注册用户，而是登记了用户信息
        /// </summary>
        public int? UserID { get; set; }

        public virtual User User { get; set; }

        /// <summary>
        /// 活动用户信息ID（如果是参加完活动后，参与抽奖的登记用户，使用活动用户的信息）
        /// </summary>
        public int? ActivityInfoParticipatorID { get; set; }

        public virtual ActivityInfoParticipator ActivityInfoParticipator { get; set; }

        /// <summary>
        /// 调查用户信息ID（如果是参加完活动后，参与抽奖的登记用户，使用活动用户的信息）
        /// </summary>
        public int? SurveyAnswerUserID { get; set; }

        public virtual SurveyAnswerUser SurveyAnswerUser { get; set; }

        /// <summary>
        /// 抽奖时间
        /// </summary>
        public DateTime LotteryDate { get; set; }

        /// <summary>
        /// 领奖情况
        /// </summary>
        public int EnumLotteryStatus { get; set; }
    }
}
