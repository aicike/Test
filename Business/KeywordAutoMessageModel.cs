using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Interface;
using Injection.Transaction;
using Injection;

namespace Business
{
    public class KeywordAutoMessageModel : BaseModel<KeywordAutoMessage>, IKeywordAutoMessageModel
    {
        [Transaction]
        public Result AddList(List<KeywordAutoMessage> list)
        {
            Result result = new Result();
            try
            {
                StringBuilder sqlBuilder = new StringBuilder("INSERT INTO dbo.KeywordAutoMessage (SystemStatus,EnumMessageTypeID,TextReply,MessageID,AutoMessage_KeywordID,[Order]) ");
                foreach (var item in list)
                {
                    sqlBuilder.Append(string.Format(" SELECT 0,{0},'{1}',{2},{3},{4} UNION ALL", item.EnumMessageTypeID, item.TextReply ?? "",
                        item.MessageID, item.AutoMessage_KeywordID, item.Order));
                }
                var sql = sqlBuilder.ToString();
                sql = sql.Remove(sql.Length - " UNION ALL".Length);
                CommonModel commonModel = Factory.Get(SystemConst.IOC_Model.CommonModel) as CommonModel;
                commonModel.SqlExecute(sql);
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
                string sql = "DELETE dbo.KeywordAutoMessage WHERE AutoMessage_KeywordID =" + autoMessage_KeywordID;
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
