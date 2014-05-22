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
        [StringLength(25, ErrorMessage = "长度小于25")]
        [Required(ErrorMessage = "请输入标题")]
        public string Title { get; set; }

        /// <summary>
        /// 标题图片
        /// </summary>
        [Display(Name = "标题图片")]
        [Required(ErrorMessage = "请输入标题图片")]
        public string TitleImage { get; set; }

        /// <summary>
        /// 标题展示图片 小
        /// </summary>
        [Display(Name = "标题展示图片 小")]
        public string TitleShowImage { get; set; }
        
        /// <summary>
        /// 单元
        /// </summary>
        [Display(Name = "单元")]
        [StringLength(10, ErrorMessage = "长度小于10")]
        [Required(ErrorMessage = "请输入单元")]
        public string Unit { get; set; }


         /// <summary>
        /// 楼层
        /// </summary>
        [Display(Name = "楼层")]
        [StringLength(10, ErrorMessage = "长度小于10")]
        [Required(ErrorMessage = "请输入楼层")]
        public string Floor { get; set; }
        
        /// <summary>
        /// 房号
        /// </summary>
        [Display(Name = "房号")]
        [StringLength(10, ErrorMessage = "长度小于10")]
        [Required(ErrorMessage = "请输入房号")]
        public string RoomNumber { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        [Display(Name = "面积")]
        [Required(ErrorMessage = "请输入面积")]
        public  double area { get; set; }

        /// <summary>
        /// 户型
        /// </summary>
        [Display(Name = "户型")]
        [StringLength(10, ErrorMessage = "长度小于10")]
        [Required(ErrorMessage = "请输入户型")]
        public string HouseType { get; set; }

        /// <summary>
        /// 装修
        /// </summary>
        [Display(Name = "装修")]
        [Required(ErrorMessage = "请输入装修")]
        public int  EnumDecoration { get; set; }

        /// <summary>
        /// 租赁方式 0合租 1整租
        /// </summary>
        [Display(Name = "租赁方式 0合租 1整租")]
        public int Lease { get; set; }
        


        /// <summary>
        /// 月租
        /// </summary>
        [Display(Name = "月租")]
        [Required(ErrorMessage = "请输入月租")]
        public decimal Price{ get; set; }

         /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        public string Description { get; set; }
        

        /// <summary>
        /// 房屋配套
        /// </summary>
        [Display(Name = "房屋配套")]
        [StringLength(200, ErrorMessage = "长度小于200")]
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
        [StringLength(10, ErrorMessage = "长度小于10")]
        [Required(ErrorMessage = "请输入业主姓名")]
        public string OwnerName { get; set; }

        /// <summary>
        /// 业主电话
        /// </summary>
        [Display(Name = "业主电话")]
        [Required(ErrorMessage = "请输入业主电话")]
        public int OwnerPhone{ get; set; }

        /// <summary>
        /// 状态 0 关闭 1发布
        /// </summary>
        [Display(Name = "状态 0 不发布 1发布")]
        public int Stauts { get; set; }

    }
}
