using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;

namespace Interface
{
    public interface IBaseModel<T> where T : class,IBaseEntity
    {
        IQueryable<T> List();

        Result Add(T entity);

        Result Edit(T entity);

        Result Delete(int id);

        Result CompleteDelete(int id);

        T Get(int id);

        IQueryable<T> SqlQuery(string sql);
    }
}
