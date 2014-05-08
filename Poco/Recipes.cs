using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 每日食谱
    /// </summary>
    public class Recipes : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入标题")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = "内容")]
        public string Content { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [Required(ErrorMessage = "请输入描述")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        public string Depict { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        [Display(Name = "发布日期")]
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// App展示图片
        /// </summary>
        [Display(Name = "App展示图片")]
        [Required(ErrorMessage = "请选择一张展示图片")]
        public string MainImagPath { get; set; }

        /// <summary>
        /// 展示缩略图 中
        /// </summary>
        [Display(Name = "展示缩略图 中")]
        public string AppShowImagePath { get; set; }

        /// <summary>
        /// 展示缩略图 小
        /// </summary>
        [Display(Name = "展示缩略图 小")]
        public string MinImagePath { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        [Display(Name = "浏览次数")]
        public int BrowseCnt { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        [Display(Name = "是否发布")]
        public bool IsRelease { get; set; }
    }
}
