using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Poco.Enum;

namespace Business
{
    public class KeywordModel : BaseModel<Keyword>, IKeywordModel
    {
        [Transaction]
        public Result AddList(IList<Keyword> keywords)
        {
            Result result = new Result();
            try
            {
                foreach (var item in keywords)
                {
                    item.SystemStatus = (int)EnumSystemStatus.Active;
                    Context.Keyword.Add(item);
                }
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        [Transaction]
        public Result DeleteByAutoMessage_KeywordID(int autoMessage_KeywordID)
        {
            Result result = new Result();
            try
            {
                string sql = "DELETE dbo.Keyword WHERE AutoMessage_KeywordID =" + autoMessage_KeywordID;
                base.SqlExecute(sql);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }
    }
}
