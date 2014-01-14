using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class ProductImg : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int ProductID { get; set; }
        public virtual Product Product { get; set; }


        [Display(Name = "图片地址")]
        public string PImgOriginal { get; set; }

        [Display(Name = "原始图片")]
        public string PImgMini { get; set; }





    }
}
