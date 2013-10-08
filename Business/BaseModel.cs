using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
namespace Business
{
    public class BaseModel<T> : EFModel where T : class, IBaseEntity
    {
        public IQueryable<T> List()
        {
            return base.List<T>().OrderByDescending(a=>a.ID);
        }
        public IQueryable<T> GlobalList()
        {
            return base.GlobalList<T>().OrderByDescending(a => a.ID);
        }
        public Result Add(T entity)
        {
            return base.Add<T>(entity);
        }

        public Result Edit(T entity)
        {
            return base.Edit<T>(entity);
        }

        public Result Delete(int id)
        {
            return base.Delete<T>(id);
        }

        public Result CompleteDelete(int id)
        {
            return base.CompleteDelete<T>(id);
        }

        public T Get(int id)
        {
            return base.Get<T>(id);
        }

        public IQueryable<T> SqlQuery(string sql)
        {
            return Context.Database.SqlQuery<T>(sql).AsQueryable();
        }

        public int SqlExecute(string sql,params object[] parameters)
        {
            return Context.Database.ExecuteSqlCommand(sql, parameters);
        }
    }
}
