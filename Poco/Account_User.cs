using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    /// <summary>
    /// Account管理的User数据
    /// </summary>
    [Serializable]
    public class Account_User : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountID { get; set; }

        public virtual Account Account { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }

        public int GroupID { get; set; }

        public virtual Group Group { get; set; }

    }
}
