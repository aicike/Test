using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    public class UserTag : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        [Display(Name = "备注名称")]
        public string TagName { get; set; }


        public virtual ICollection<User> User { get; set; }
    }
}
