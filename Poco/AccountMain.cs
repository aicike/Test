using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace Poco
{
    /// <summary>
    /// 账号总表（房地产公司基本信息）
    /// </summary>
    public class AccountMain : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "名称")]
        [Required(ErrorMessage = "请输入名称")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RemotePlus("CheckIsUniqueAccountMain", "Ajax", "", "Default", ErrorMessage = "名称已存在")]
        public string Name { get; set; }

        [Display(Name = "二级域名或目录名称")]
        [Required(ErrorMessage = "请输入")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RegularExpression("^[a-zA-Z0-9_]+$", ErrorMessage = "请输入有效字符(只能输入‘字母’，‘数字’或‘_’)")]
        [RemotePlus("CheckIsUniqueAccountMain_HostName", "Ajax", "", "Default", ErrorMessage = "该名称已存在，请修改")]
        public string HostName { get; set; }

        [Display(Name = "省份")]
        [DropDownList(ErrorMessage = "请选择省份")]
        public int ProvinceID { get; set; }
        public virtual Province Province { get; set; }

        [Display(Name = "城市")]
        [DropDownList(ErrorMessage = "请选择城市")]
        public int CityID { get; set; }
        public virtual City City { get; set; }

        [Display(Name = "地区")]
        [DropDownList(ErrorMessage = "请选择地区")]
        public int DistrictID { get; set; }
        public virtual District District { get; set; }

        [Display(Name = "电话")]
        [Required(ErrorMessage = "请输入电话")]
        [StringLength(30, ErrorMessage = "长度小于30")]
        public string Phone { get; set; }

        [Display(Name = "Logo缩略图")]
        [Required(ErrorMessage = "请上传Logo缩略图")]
        [StringLength(500, ErrorMessage = "长度小于500缩略图")]
        public string LogoImageThumbnailPath { get; set; }

        [Display(Name = "Logo")]
        [Required(ErrorMessage = "请上传Logo")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        //[RegularExpression(".+(?i)(gif|jpg|png)", ErrorMessage = "文件格式为(.gif .jpg .jpeg .png .bmp)")]        
        public string LogoImagePath { get; set; }

        /// <summary>
        /// 账号类型，启用禁用
        /// </summary>
        [Display(Name = "状态")]
        public int AccountStatusID { get; set; }

        public virtual LookupOption AccountStatus { get; set; }

        /// <summary>
        /// 可上传文件限制
        /// </summary>
        [Display(Name = "素材限额")]
        [Range(0, 10, ErrorMessage = "目前系统限制最大附件容量为10GB")]
        public double FileLimit { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public int AccountMainExpandInfoID { get; set; }
        public virtual AccountMainExpandInfo AccountMainExpandInfo { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public int SystemUserID { get; set; }
        public virtual SystemUser SystemUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public virtual ICollection<LibraryText> LibraryTexts { get; set; }

        public virtual ICollection<LibraryVoice> LibraryVoices { get; set; }

        public virtual ICollection<LibraryVideo> LibraryVideos { get; set; }

        public virtual ICollection<LibraryImageText> LibraryImageTexts { get; set; }

        public virtual ICollection<LibraryImage> LibraryImages { get; set; }

        public virtual ICollection<AutoMessage_Add> AutoMessage_Adds { get; set; }

        public virtual ICollection<AutoMessage_Reply> AutoMessage_Replys { get; set; }

        public virtual ICollection<AutoMessage_Keyword> AutoMessage_Keywords { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<Account_AccountMain> Account_AccountMains { get; set; }

        public virtual ICollection<AccountMainHouseType> AccountMainHouseTypes { get; set; }

        public virtual ICollection<AccountMainHouseInfo> AccountMainHouseInfos { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<AccountMainHouses> AccountMainHousess { get; set; }
    }
}