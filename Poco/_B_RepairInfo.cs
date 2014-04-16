using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class _B_RepairInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 报修时间
        /// </summary>
        public string date { get; set; }

        /// <summary>
        /// 状态 
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 报修类型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 处理人姓名
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 处理人电话
        /// </summary>
        public string AccountPhone { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public string Score { get; set; }

        /// <summary>
        /// 操作 时间:内容|时间:内容
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// 图片路径 多张图片用|分割
        /// </summary>
        public string ImgPaths { get; set; }

    }
}
