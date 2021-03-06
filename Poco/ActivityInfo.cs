﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class ActivityInfo:IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 售楼部ID
        /// </summary>
        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 活动标题
        /// </summary>
        [Display(Name = "活动标题")]
        [Required(ErrorMessage = "请输入活动标题")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Title { get; set; }


        /// <summary>
        /// 活动备注或描述
        /// </summary>
        [Display(Name = "活动备注或描述")]
        [Required(ErrorMessage = "请输入活动备注或描述")]
        public string Remarks { get; set; }

        /// <summary>
        /// 活动内容
        /// </summary>
        [Display(Name = "活动内容")]
        [Required(ErrorMessage = "请输入活动活动内容")]
        public string Content { get; set; }


        [Display(Name = "App展示与分享图片")]
        public string MainImagPath { get; set; }


        [Display(Name = "展示缩略图 中")]
        public string AppShowImagePath { get; set; }


        [Display(Name = "展示缩略图 小")]
        public string MinImagePath { get; set; }



        /// <summary>
        /// 创建人
        /// </summary>
        [Display(Name = "创建人")]
        public int AccountID { get; set; }
        public virtual Account Account { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 报名结束日期
        /// </summary>
        [Display(Name = "报名结束日期")]
        [Required(ErrorMessage = "请选择报名结束日期")]
        public DateTime EnrollEndDate { get; set; }


        /// <summary>
        /// 活动开始日期
        /// </summary>
        [Display(Name = "活动开始日期")]
        [Required(ErrorMessage = "请选择活动开始日期")]
        public string ActivityStratDate { get; set; }

        /// <summary>
        /// 最大报名人数
        /// </summary>
        [Display(Name = "最大报名人数")]
        [Required(ErrorMessage = "请输入最大报名人数")]
        public int MaxCount { get; set; }


        /// <summary>
        /// 状态 0：开启 1：停用
        /// </summary>
        [Display(Name = "状态")]
        public int Status { get; set; }

        /// <summary>
        /// 是否已生成用户端咨询 0否 1是
        /// </summary>
         [Display(Name = "是否已生成用户端咨询")]
        public int ISGenerateUserAdvisory { get; set; }

         /// <summary>
         /// 是否已生成销售端咨询 0否 1是
         /// </summary>
         [Display(Name = "是否已生成销售端咨询")]
         public int ISGenerateSaleAdvisory { get; set; }



        public virtual ICollection<ActivityInfoParticipator> ActivityInfoParticipators { get; set; }

        
        public virtual ICollection<ActivityInfoSignIn> ActivityInfoSignIn { get; set; }

        public virtual ICollection<AppAdvertorialBrowse> AppAdvertorialBrowse { get; set; }
    }
}
