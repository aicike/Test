using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class User_Group:IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }

        public int GroupID { get; set; }

        public virtual Group Group { get; set; }
    }
}
