using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class RentalHouse : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }


        public int AccountMainID { get; set; }
        public virtual AccountMain AccountMain { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        [Required(ErrorMessage = "请输入标题")]
        public string Title { get; set; }

        /// <summary>
        /// 标题图片
        /// </summary>
        [Display(Name = "标题图片")]
        [Required(ErrorMessage = "请输入标题图片")]
        public string TitleImage { get; set; }


        /// <summary>
        /// 单元
        /// </summary>
        [Display(Name = "单元")]
        [Required(ErrorMessage = "请输入单元")]
        public string Unit { get; set; }


         /// <summary>
        /// 楼层
        /// </summary>
        [Display(Name = "楼层")]
        [Required(ErrorMessage = "请输入楼层")]
        public string Floor { get; set; }
        
        /// <summary>
        /// 房号
        /// </summary>
        [Display(Name = "房号")]
        [Required(ErrorMessage = "请输入房号")]
        public string RoomNumber { get; set; }

        /// <summary>
        /// 价格/月
        /// </summary>
        [Display(Name = "价格/月")]
        [Required(ErrorMessage = "请输入价格")]
        public decimal Price{ get; set; }

         /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        public string Description { get; set; }
        

        /// <summary>
        /// 房屋配套
        /// </summary>
        [Display(Name = "房屋配套")]
        [Required(ErrorMessage = "请输入房屋配套")]
        public string Matching { get; set; }


        /// <summary>
        /// 简介与图片
        /// </summary>
        [Display(Name = "简介与图片")]
        [Required(ErrorMessage = "请输入简介与图片")]
        public string Introduction { get; set; }
        
         /// <summary>
        /// 业主姓名
        /// </summary>
        [Display(Name = "业主姓名")]
        public string OwnerName { get; set; }

        /// <summary>
        /// 业主姓名
        /// </summary>
        [Display(Name = "业主姓名")]
        public string OwnerPhone{ get; set; }

    }
}
