﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class AppAdvertorial : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入标题")]
        public string Title { get; set; }

        [Display(Name = "置顶 1是 0 否")]
        public int stick { get;set;}

        [Display(Name = "排序")]
        public int Sort { get; set; }

        [Display(Name = "内容")]
        [Required(ErrorMessage = "请输入内容")]
        public string Content { get; set; }

        [Display(Name= "描述")]
        [Required(ErrorMessage = "请输入描述")]
        public string Depict { get; set; }

        [Display(Name = "发布日期")]
        public DateTime IssueDate { get; set; }


        [Display(Name = "App展示图片")]
        [Required(ErrorMessage = "请选择一张展示图片")]
        public string MainImagPath { get; set; }


        [Display(Name = "展示缩略图 中")]
        public string AppShowImagePath { get; set; }


        [Display(Name = "展示缩略图 小")]
        public string MinImagePath { get; set; }

    }
}