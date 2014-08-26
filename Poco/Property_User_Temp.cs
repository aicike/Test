using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Poco
{
    /// <summary>
    /// 业主信息(导入时需要用到的实体)
    /// </summary>
    public class Property_User_Temp
    {
        public string UserName { get; set; }

        public string Phone { get; set; }

        public string Comment { get; set; }

        public string Email { get; set; }

        public string HouseShortNum { get; set; }

        public int Property_HouseID { get; set; }
    }
}
