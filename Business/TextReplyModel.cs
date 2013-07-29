using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Poco.Enum;

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
    }
}
