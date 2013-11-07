using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;

namespace Business
{
    public class TaskModel : BaseModel<Task>, ITaskModel
    {
        public IQueryable<Task> GetByCreateAccountID(int createAccountID)
        {
            return List().Where(a => a.CreateAccountID == createAccountID);
        }
    }
}
