using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    [Serializable]
    public class Province : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "省份")]
        [Required(ErrorMessage = "请输入省份")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Name { get; set; }

        public virtual ICollection<AccountMain> AccountMains { get; set; }

        public virtual ICollection<City> Citys { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }

    [Serializable]
    public class City : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "城市")]
        [Required(ErrorMessage = "请输入城市")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Name { get; set; }

        public int ProvinceID { get; set; }

        public virtual Province Province { get; set; }

        [Display(Name = "邮政编码")]
        [Required(ErrorMessage = "请输入邮政编码")]
        [StringLength(10, ErrorMessage = "长度小于10")]
        public string ZipCode { get; set; }

        public virtual ICollection<District> Districts { get; set; }

        public virtual ICollection<AccountMain> AccountMains { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }

    [Serializable]
    public class District : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "地区")]
        [Required(ErrorMessage = "请输入地区")]
        [StringLength(50, ErrorMessage = "长度小于50")]
        public string Name { get; set; }

        public int CityID { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<AccountMain> AccountMains { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }


}
