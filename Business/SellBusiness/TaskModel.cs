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

        public new Result Delete(int id)
        {
            Result result = new Result();
            try
            {
                string sql_detail = "DELETE dbo.TaskDetail WHERE TaskID=" + id;
                base.SqlExecute(sql_detail);
                result = base.CompleteDelete(id);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        public IQueryable<Task> GetMyTasks(int accountID)
        {
            return List().Where(a => a.ReceiverAccountID == accountID);
        }
    }
}
