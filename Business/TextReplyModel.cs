using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Poco.Enum;
using Injection.Transaction;

namespace Business
{
    public class TextReplyModel : BaseModel<TextReply>, ITextReplyModel
    {
        public Result AddList(IList<TextReply> textReplys)
        {
            Result result = new Result();
            try
            {
                foreach (var item in textReplys)
                {
                    item.SystemStatus = (int)EnumSystemStatus.Active;
                    Context.TextReply.Add(item);
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
                string sql = "DELETE dbo.TextReply WHERE AutoMessage_KeywordID =" + autoMessage_KeywordID;
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
