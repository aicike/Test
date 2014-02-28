using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class _B_SurveyMain
    {

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 调查标题
        /// </summary>
        public string SurveyTitle { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 回答数
        /// </summary>
        public int Counts { get; set; }

        /// <summary>
        /// 状态 0：开启 1：停用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 调查类型 1：普通调查，2：打分调查
        /// </summary>
        public int EnumSurveyMainType { get; set; }

        /// <summary>
        /// 是否已生成用户端咨询 0否 1是
        /// </summary>
        public int ISGenerateUserAdvisory { get; set; }

        /// <summary>
        /// 是否已生成销售端咨询 0否 1是
        /// </summary>
        public int ISGenerateSaleAdvisory { get; set; }

        //显示图片
        public string AppShowImagePath { get; set; }


    }
}
