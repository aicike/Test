using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco.WebAPI_Poco
{
    /// <summary>
    /// 业主信息表
    /// </summary>
    public class App_PropertyUser
    {
        public int PropertyUserID { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string RoomNum { get; set; }

        public string BuildingNum { get; set; }

        public string CellNum { get; set; }

        public string Email { get; set; }
    }
}
