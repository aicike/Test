using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 客户端(APP)信息
    /// </summary>
    [Serializable]
    public class ClientInfo : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        #region 客户端信息

        /// <summary>
        /// 全球唯一标示
        /// </summary>
        [StringLength(16, ErrorMessage = "长度小于16")]
        public string IMEI { get; set; }

        /// <summary>
        /// 客户端系统类型
        /// </summary>
        public int? EnumClientSystemTypeID { get; set; }
        public virtual LookupOption EnumClientSystemType { get; set; }

        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime? SetupTiem { get; set; }

        /// <summary>
        /// 客户端账号类型
        /// </summary>
        public int? EnumClientUserTypeID { get; set; }
        public virtual LookupOption EnumClientUserType { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [Display(Name = "标签")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Tag { get; set; }

        [Display(Name = "客户端ID")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string ClientID { get; set; }

        #endregion


        #region 账号信息
        /// <summary>
        /// 账号ID
        /// </summary>
        public int? EntityID { get; set; }
        /// <summary>
        /// 账号对象
        /// </summary>
        public IBaseEntity Entity { get; set; }
        #endregion
    }
}
