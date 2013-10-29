﻿using System;
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
    [Serializable]
    public class AccountMain : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "名称")]
        [Required(ErrorMessage = "请输入名称")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RemotePlus("CheckIsUniqueAccountMain", "Ajax", "", "Default", ErrorMessage = "名称已存在")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Name { get; set; }

        [Display(Name = "二级域名或目录名称")]
        [Required(ErrorMessage = "请输入")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "请输入有效字符(只能输入‘字母’或‘数字’)")]
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
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Phone { get; set; }

        [Display(Name = "Logo缩略图")]
        [Required(ErrorMessage = "请上传Logo缩略图")]
        [StringLength(500, ErrorMessage = "长度小于500缩略图")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string LogoImageThumbnailPath { get; set; }

        [Display(Name = "Logo")]
        [Required(ErrorMessage = "请上传Logo")]
        [StringLength(500, ErrorMessage = "长度小于500")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
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

        [Display(Name = "售楼地址")]
        [Required(ErrorMessage = "请输入售楼地址")]
        [StringLength(200, ErrorMessage = "长度小于200")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string SaleAddress { get; set; }

        [Display(Name = "售楼电话")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string SalePhone { get; set; }

        [Display(Name = "售楼地图定位地址")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string SaleMapAddress { get; set; }

        [Display(Name = "纬度")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Lng { get; set; }

        [Display(Name = "经度")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Lat { get; set; }

        [Display(Name = "IOS下载地址")]
        public string IOSDownloadPath { get; set; }

        [Display(Name = "Android下载地址")]
        public string AndroidDownloadPath { get; set; }

        [Display(Name = "Android版本")]
        [RegularExpression(@"^\d{1,3}.\d.\d$", ErrorMessage = "版本号格式：x.x.x")]
        public string AndroidVersion { get; set; }

        [Display(Name = "IOS版本")]
        [RegularExpression(@"^\d{1,3}.\d.\d$", ErrorMessage = "版本号格式：x.x.x")]
        public string IOSVersion { get; set; }

        /// <summary>
        /// 数据更新信息
        /// </summary>
        public int? AppUpdateID { get; set; }
        public virtual AppUpdate AppUpdate { get; set; }

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

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<AccountMainHouses> AccountMainHousess { get; set; }

        public virtual ICollection<PushMsg> PushMsgs { get; set; }

        public virtual ICollection<AutoMessage_User> AutoMessage_Users { get; set; }

        public virtual ICollection<AppAdvertorial> AppAdvertorial { get; set; }

        public virtual ICollection<AppWaitImg> AppWaitImg { get; set; }
    }
}