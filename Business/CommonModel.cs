using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Business
{
    public class CommonModel : EFModel
    {
        /// <summary>
        /// 返回false，说明已存在该值
        /// </summary>
        public bool CheckIsUnique(string tableName, string field, string value, int? id = null)
        {
            string sql = null;

            if (id.HasValue && id.Value > 0)
            {
                sql = string.Format("SELECT ID FROM {0} WHERE {1}='{2}' AND SystemStatus=0 AND ID<>{3}", tableName, field, value, id.Value);
            }
            else
            {
                sql = string.Format("SELECT ID FROM {0} WHERE {1}='{2}' AND SystemStatus=0 ", tableName, field, value);
            }
            var result = SqlQuery<int>(sql).ToList();
            if (result.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IQueryable<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return Context.Database.SqlQuery<T>(sql, parameters).AsQueryable();
        }

        public int SqlExecute(string sql, params object[] parameters)
        {
            return Context.Database.ExecuteSqlCommand(sql, parameters);
        }

    }
}
