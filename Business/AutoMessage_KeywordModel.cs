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
        public Result Add(AutoMessage_Keyword entity, string keys, string messageTexts, string messageFileIDs, string messageImageTextIDs, int accountMainID)
        {
            Result result = new Result();
            //添加回复规则
            AutoMessage_Keyword msg = new AutoMessage_Keyword();
            msg.RuleName = entity.RuleName;
            msg.RuleNo = entity.RuleNo;
            msg.FullRuleNo = entity.FullRuleNo;
            msg.AccountMainID = accountMainID;
            msg.ParentAutoMessage_KeywordID = entity.ParentAutoMessage_KeywordID;
            msg.AccountMainHousesID = entity.AccountMainHousesID;
            msg.IsFistAutoMessage = entity.IsFistAutoMessage;
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
            return List().Where(a => a.AccountMainID == accoutMainID && a.ParentAutoMessage_KeywordID.HasValue == false);
        }

        public int GetRuleNo(int accountMainID, int? parentAutoMessage_KeywordID)
        {
            int ruleNo = 0;
            if (parentAutoMessage_KeywordID.HasValue == false)
            {
                ruleNo = List().Where(a => a.AccountMainID == accountMainID && a.ParentAutoMessage_KeywordID.HasValue == false).OrderByDescending(a => a.RuleNo).Select(a => a.RuleNo).FirstOrDefault();
                if (ruleNo == 0)
                {
                    ruleNo = 1;
                }
                else
                {
                    ruleNo += 1;
                }
            }
            else
            {
                ruleNo = List().Where(a => a.AccountMainID == accountMainID && a.ParentAutoMessage_KeywordID.Value == parentAutoMessage_KeywordID.Value).OrderByDescending(a => a.RuleNo).Select(a => a.RuleNo).FirstOrDefault();
                if (ruleNo == 0)
                {
                    ruleNo = 1;
                }
                else
                {
                    ruleNo += 1;
                }
            }
            return ruleNo;
        }


        public string GetFullRuleNo(int accountMainID, string fullRuleNo)
        {
            if (string.IsNullOrEmpty(fullRuleNo))
            {
                throw new ApplicationException(SystemConst.Notice.NotAuthorized);
            }
            var lastIDStr = fullRuleNo.Split('-').LastOrDefault();
            int lastID = 0;
            bool isOk = int.TryParse(lastIDStr, out lastID);
            if (isOk == false)
            {
                throw new ApplicationException(SystemConst.Notice.NotAuthorized);
            }
            int ruleNo = GetRuleNo(accountMainID, lastID);
            return string.Format("{0}-{1}", fullRuleNo, ruleNo);
        }


        public AutoMessage_Keyword GetByID_AccountMainID(int id, int accountMainID)
        {
            var entity = List().Where(a => a.ID == id && a.AccountMainID == accountMainID).FirstOrDefault();
            if (entity == null)
            {
                throw new ApplicationException(SystemConst.Notice.NotAuthorized);
            }
            return entity;
        }

        [Transaction]
        public Result Edit(int keyID, string ruleName, int projectID, string keys, string messageTexts, string messageFileIDs, string messageImageTextIDs, int accountMainID, bool isFistAutoMessage)
        {
            Result result = new Result();

            var autoMessage_Keyword = Get(keyID);
            autoMessage_Keyword.AccountMainHousesID = projectID;
            autoMessage_Keyword.RuleName = ruleName;
            autoMessage_Keyword.IsFistAutoMessage = isFistAutoMessage;
            //添加回复规则
            result = base.Edit(autoMessage_Keyword);
            if (result.HasError) return result;

            //添加关键字
            var keywordModel = Factory.Get<IKeywordModel>(SystemConst.IOC_Model.KeywordModel);
            var keyArray = keys.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<Keyword> keywords = new List<Keyword>();
            foreach (var item in keyArray)
            {
                Keyword key = new Keyword();
                key.Token = item;
                key.AutoMessage_KeywordID = keyID;
                keywords.Add(key);
            }
            result = keywordModel.DeleteByAutoMessage_KeywordID(keyID);
            if (result.HasError) return result;

            result = keywordModel.AddList(keywords);
            if (result.HasError) return result;

            //添加回复(文本)
            var textReplyModel = Factory.Get<ITextReplyModel>(SystemConst.IOC_Model.TextReplyModel);
            var msgArray = messageTexts.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            List<TextReply> textReply = new List<TextReply>();
            foreach (var item in msgArray)
            {
                TextReply text = new TextReply();
                text.Content = item;
                text.AutoMessage_KeywordID = keyID;
                textReply.Add(text);
            }
            result = textReplyModel.DeleteByAutoMessage_KeywordID(keyID);
            if (result.HasError) return result;

            result = textReplyModel.AddList(textReply);
            //添加回复（文件）

            //添加回复（图文）
            return result;
        }

        [Transaction]
        public Result Delete(int id, int accountMainID)
        {
            Result result = new Result();
            var entity = Get(id);
            if (entity == null || entity.AccountMainID != accountMainID)
            {
                result.Error = SystemConst.Notice.NotAuthorized;
            }
            try
            {
                string ids = GetIDString(entity.AutoMessage_KeywordsKeyword);
                if (ids.Length > 0)
                {
                    ids += id;
                }
                string deleteSQL = string.Format("DELETE dbo.AutoMessage_Keyword WHERE ID IN ({0})", ids);
                base.SqlExecute(deleteSQL);
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }

        private string GetIDString(ICollection<AutoMessage_Keyword> entitys)
        {
            if (entitys.Count == 0) { return ""; }
            StringBuilder sb = new StringBuilder();
            foreach (var item in entitys)
            {
                sb.AppendFormat("{0},", item.ID);
                sb.Append(GetIDString(item.AutoMessage_KeywordsKeyword));
            }
            return sb.ToString();
        }

        public List<AutoMessage_Keyword> GetFirstAutoMessage(int accountMainID)
        {
            return List().Where(a => a.AccountMainID == accountMainID).OrderBy(a => a.ID).ToList();
        }

    }
}
