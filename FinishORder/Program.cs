using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FinishORder
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
            string sql = string.Format("update [Order] set [status]={0} where AccountMainID ={1} and Convert(varchar(20),EndDate,112) =Convert(varchar(20),getdate(),112) and [status]={2}", (int)Poco.Enum.EnumOrderStatus.Complete, AccountMainID, (int)Poco.Enum.EnumOrderStatus.Proceed);
            int a =SqlHelper.ExecuteNonQuery(sql);
        }
    }
}
