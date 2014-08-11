using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class _B_Complaint
    {
        /// <summary>
        /// 投诉ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 投诉是否匿名
        /// </summary>
        public bool IsAnonymous { get; set; }

        /// <summary>
        /// 投诉状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public string Score { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contetn { get; set; }

        /// <summary>
        /// 投诉日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 图片 多张图片用 | 分割
        /// </summary>
        public string  ImgPath { get; set; }

        /// <summary>
        /// 答复
        /// </summary>
        public string Reply { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public string UserInfo { get; set; }
    }
}
