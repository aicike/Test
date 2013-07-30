using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interface;
using Poco;
using Injection;
using Controllers;

namespace Web.Controllers
{
    public class KeywordMessageController : ManageAccountController
    {
        public ActionResult Index(int? index)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            var list = autoMessage_KeywordModel.List(LoginAccount.CurrentAccountMainID).ToPagedList(index ?? 1, 15);
            return View(list);
        }

        [HttpPost]
        [ValidateInput(false)]
        public string Add(string ruleName, int ruleNo, string fullRuleNo, int? parentID, string keys, string messageTexts, string messageFileIDs, string messageImageTextIDs)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            AutoMessage_Keyword msg = new AutoMessage_Keyword();
            msg.RuleName = ruleName;
            msg.RuleNo = ruleNo;
            msg.FullRuleNo = fullRuleNo;
            msg.ParentAutoMessage_KeywordID = parentID;
            var result = autoMessage_KeywordModel.Add(msg, keys, messageTexts, messageFileIDs, messageImageTextIDs, LoginAccount.CurrentAccountMainID);
            if (result.HasError)
            {
                return AlertJS_NoTag(new Dialog(result.Error));
            }
            return "window.location.href='" + Url.Action("Index", "KeywordMessage", new { HostName = LoginAccount.HostName }) + "'";
        }

        [AllowCheckPermissions(false)]
        public string GetRuleNoString(string fullRuleNo)
        {
            string ruleNo = null;
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            if (string.IsNullOrEmpty(fullRuleNo) == false)
            {
                ruleNo = autoMessage_KeywordModel.GetFullRuleNo(LoginAccount.CurrentAccountMainID, fullRuleNo);
            }
            else
            {
                ruleNo = autoMessage_KeywordModel.GetRuleNo(LoginAccount.CurrentAccountMainID, null).ToString();
            }
            return ruleNo;
        }

        [AllowCheckPermissions(false)]
        public ActionResult Get(int id)
        {
            var autoMessage_KeywordModel = Factory.Get<IAutoMessage_KeywordModel>(SystemConst.IOC_Model.AutoMessage_KeywordModel);
            var entity = autoMessage_KeywordModel.GetByID_AccountMainID(id, LoginAccount.ID);
            var json = new
            {
                ID = entity.ID,
                RuleName = entity.RuleName,
                RuleNo = entity.RuleNo,
                FullRuleNo = entity.FullRuleNo,
                Keywords = entity.Keywords.Select(a => a.Token).ToList().ConvertToString(","),
                //KeywordAutoMessages = entity.KeywordAutoMessages.Select(a => new KeywordAutoMessage { ID = a.ID }).ToList().ObjectToJson("KeywordAutoMessages"),
                TextReplys = entity.TextReplys.Select(a => a.Content).ToList().ObjectToJson("TextReplys")
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}
