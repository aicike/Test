using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SetTaskStatus
{
    public class Program
    {
        static void Main(string[] args)
        {
            SetStatus();
        }

        public static void SetStatus()
        {
            string AccountMainID = ConfigurationManager.AppSettings["AccountMainID"].ToString();
            string sql2 = string.Format("update [task] set [EnumTaskStatus]={0} where AccountMainID ={1} and [EnumTaskStatus]={2} and  Convert(varchar(20),BeginDate,112) =Convert(varchar(20),getdate(),112)", (int)Poco.Enum.EnumTaskStatus.Process, AccountMainID, (int)Poco.Enum.EnumTaskStatus.None);
            int a = SqlHelper.ExecuteNonQuery(sql2);

            string sql = string.Format("update [task] set [EnumTaskStatus]={0} where AccountMainID ={1} and [EnumTaskStatus]={2} and EndDate < getdate()", (int)Poco.Enum.EnumTaskStatus.Finish, AccountMainID, (int)Poco.Enum.EnumTaskStatus.Process);
            int b = SqlHelper.ExecuteNonQuery(sql);
        }
    }
}
