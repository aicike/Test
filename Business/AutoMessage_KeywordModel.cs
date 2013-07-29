using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Poco;
using Injection;
using Interface;
using Injection.Transaction;

namespace Business
{
    public class AutoMessage_KeywordModel : BaseModel<AutoMessage_Keyword>, IAutoMessage_KeywordModel
    {
        [Transaction]
        public Result Add(string ruleName, string keys, string messageTexts, string messageFileIDs, string messageImageTextIDs, int accountMainID)
        {
            Result result = new Result();
            //添加回复规则
            AutoMessage_Keyword msg = new AutoMessage_Keyword();
            msg.RuleName = ruleName;
            msg.RuleNo = "1";
            msg.AccountMainID = accountMainID;
            result = base.Add(msg);
            if (result.HasError)
            {
                return result;
            }
            //添加关键字
            var keywordModel = Factory.Get<IKeywordModel>(SystemConst.IOC_Model.KeywordModel);
            var keyArray = keys.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<Keyword> keywords = new List<Keyword>();
            foreach (var item in keyArray)
            {
                Keyword key = new Keyword();
                key.Token = item;
                key.AutoMessage_KeywordID = msg.ID;
                keywords.Add(key);
            }
            result = keywordModel.AddList(keywords);
            if (result.HasError)
            {
                return result;
            }
            //添加回复(文本)
            var textReplyModel = Factory.Get<ITextReplyModel>(SystemConst.IOC_Model.TextReplyModel);
            var msgArray = messageTexts.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            List<TextReply> textReply = new List<TextReply>();
            foreach (var item in msgArray)
            {
                TextReply text = new TextReply();
                text.Content = item;
                text.AutoMessage_KeywordID = msg.ID;
                textReply.Add(text);
            }
            result = textReplyModel.AddList(textReply);
            //添加回复（文件）

            //添加回复（图文）
            return result;
        }

        public IQueryable<AutoMessage_Keyword> List(int accoutMainID)
        {
            return List().Where(a => a.AccountMainID == accoutMainID);
        }
    }
}
