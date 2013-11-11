using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poco
{
    public class TaskAccount : IBaseEntity
    {
        public int ID { get; set; }

        public int SystemStatus { get; set; }

        public int AccountID { get; set; }

        public virtual Account Account { get; set; }

        public int TaskDetailID { get; set; }

        public virtual TaskDetail TaskDetail { get; set; }
    }
}
