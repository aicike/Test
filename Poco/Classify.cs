using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class Classify : IBaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        /// <summary>
        /// 所属客户
        /// </summary>
        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [Display(Name = "类别名称")]
        [Required(ErrorMessage = "请输入类别名称")]
        [StringLength(20, ErrorMessage = "长度小于20")]
        public string Name { get; set; }

        /// <summary>
        /// 类别描述
        /// </summary>
        [Display(Name = "类别描述")]
        [StringLength(20, ErrorMessage = "长度小于20")]
        public string Depict { get; set; }


        /// <summary>
        /// 类别图片
        /// </summary>
        [Display(Name = "类别图片")]
        public string ImgPath { get; set; }

        /// <summary>
        /// 上级ID 0为顶级分类
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 层级 0为第一级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 排序 升序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 从属关系 
        /// </summary>
        public string Subordinate { get; set; }


        public virtual ICollection<Product> Product { get; set; }
    }
}
