using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class TaskRuleModel : BaseModel<TaskRule>, ITaskRuleModel
    {
        public IQueryable<TaskRule> GetByAccountMainID(int accountMainID)
        {
            return List().Where(a => a.AccountMainID == accountMainID);
        }
    }
}
