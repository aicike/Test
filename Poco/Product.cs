using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class Product : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        
        [Display(Name = "品名")]
        [Required(ErrorMessage = "请输入品名")]
        [StringLength(100, ErrorMessage = "长度小于100")]
        public string Name { get; set; }

        [Display(Name = "规格")]
        public string Specification { get; set; }

        [Display(Name = "销售价格")]
        [Required(ErrorMessage = "请输入销售价格")]
        public double Price { get; set; }

        [Display(Name = "运费")]
        [Required(ErrorMessage = "请输入运费")]
        public double Freight { get; set; }

        [Display(Name = "介绍")]
        public string Introduction { get; set; }

        [Display(Name = "操作日期")]
        public string LastSetDate { get; set; }

        [Display(Name = "产品状态")]
        public int Status { get; set; }

        //类别
        [Required(ErrorMessage = "请选择类别")]
        public int ClassifyID { get; set; }
        public virtual Classify Classify { get; set; }

        //扩展字段
        public string file1 { get; set; }
        public string file2 { get; set; }
        public string file3 { get; set; }
        public string file4 { get; set; }
        public string file5 { get; set; }
        public string file6 { get; set; }
        public string file7 { get; set; }



        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        public virtual ICollection<ProductImg> ProductImg { get; set; }
    }
}
